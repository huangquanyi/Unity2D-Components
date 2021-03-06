using DG.Tweening;
using Matcha.Dreadful;
using Matcha.Unity;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class DisplayScoreFX : BaseBehaviour
{
	private Text textComponent;
	private CanvasScaler canvasScaler;
	private RectTransform rectTrans;
	private Sequence fadeIn;
	private Sequence fadeOutInstant;
	private Sequence fadeOutHUD;
	private Sequence displayScoreFX;
	private float canvasScale;

	void Awake()
	{
		rectTrans = GetComponent<RectTransform>();
		Assert.IsNotNull(rectTrans);

		textComponent = GetComponent<Text>();
		Assert.IsNotNull(textComponent);
	}

	void Start()
	{
		canvasScaler = gameObject.GetComponentInParent<CanvasScaler>();
		Assert.IsNotNull(canvasScaler);

		canvasScale = Camera.main.GetComponent<PixelArtCamera>().CanvasScale;
		Assert.AreNotApproximatelyEqual(0.0f, canvasScale);

		PositionHUDElements();

		// cache & pause tween sequences.
		(fadeOutInstant = MFX.Fade(textComponent, 0, 0, 0)).Pause();
		(fadeIn         = MFX.Fade(textComponent, 1, HUD_FADE_IN_AFTER, HUD_INITIAL_FADE_LENGTH)).Pause();
		(fadeOutHUD     = MFX.Fade(textComponent, 0, HUD_FADE_OUT_AFTER, HUD_INITIAL_FADE_LENGTH)).Pause();
		(displayScoreFX = MFX.DisplayScoreFX(gameObject)).Pause();
	}

	void OnInitScore(int initScore)
	{
		textComponent.text = initScore.ToString();
		FadeInScore();
	}

	void PositionHUDElements()
	{
		canvasScaler.scaleFactor = canvasScale;
		M.PositionInHUD(rectTrans, textComponent, SCORE_ALIGNMENT, SCORE_X_POS, SCORE_Y_POS);
	}

	void FadeInScore()
	{
		fadeOutInstant.Restart();
		fadeIn.Restart();
	}

	void OnChangeScore(int newScore)
	{
		textComponent.text = newScore.ToString();
		displayScoreFX.Restart();
	}

	void OnFadeHud(bool status)
	{
		fadeOutHUD.Restart();
	}

	void OnEnable()
	{
		EventKit.Subscribe<int>("init score", OnInitScore);
		EventKit.Subscribe<int>("change score", OnChangeScore);
		EventKit.Subscribe<bool>("fade hud", OnFadeHud);
		EventKit.Subscribe("reposition hud elements", PositionHUDElements);
	}

	void OnDestroy()
	{
		EventKit.Unsubscribe<int>("init score", OnInitScore);
		EventKit.Unsubscribe<int>("change score", OnChangeScore);
		EventKit.Unsubscribe<bool>("fade hud", OnFadeHud);
		EventKit.Unsubscribe("reposition hud elements", PositionHUDElements);
	}
}
