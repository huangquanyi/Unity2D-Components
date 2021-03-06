using System;
using UnityEngine;

static class ExtensionMethods
{
	// gameobject extensions.
    static public void SendEvent(this GameObject go, string eventType)
    {
        go.SendMessage(eventType);
    }

	static public void SendEvent<T>(this GameObject go, string eventType, T arg1)
	{
		go.SendMessage(eventType, arg1);
	}

    static public void SendEventUp(this GameObject go, string eventType)
    {
        go.SendMessageUpwards(eventType);
    }

	static public void SendEventUp<T>(this GameObject go, string eventType, T arg1)
	{
		go.SendMessageUpwards(eventType, arg1);
	}

    static public void SendEventDown(this GameObject go, string eventType)
    {
        go.BroadcastMessage(eventType);
    }

	static public void SendEventDown<T>(this GameObject go, string eventType, T arg1)
	{
		go.BroadcastMessage(eventType, arg1);
	}

	// transform extensions.
	public static void SetPositionX(this Transform transform, float x)
	{
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}

	public static void SetPositionY(this Transform transform, float y)
	{
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}

	public static void SetPositionZ(this Transform transform, float z)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}

	public static void SetPositionXY(this Transform transform, float x, float y)
	{
		transform.position = new Vector3(x, y, transform.position.z);
	}

	public static void SetPosition(this Transform transform, float x, float y, float z)
	{
		transform.position = new Vector3(x, y, z);
	}

	public static void SetLocalPositionX(this Transform transform, float x)
	{
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
	}

	public static void SetLocalPositionY(this Transform transform, float y)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
	}

	public static void SetLocalPositionZ(this Transform transform, float z)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
	}

	public static void SetLocalPositionXY(this Transform transform, float x, float y)
	{
		transform.localPosition = new Vector3(x, y, transform.localPosition.z);
	}

	public static void SetLocalPosition(this Transform transform, float x, float y, float z)
	{
		transform.localPosition = new Vector3(x, y, z);
	}

	public static void SetAbsLocalPositionX(this Transform transform, float x)
	{
		if (transform.lossyScale.x > 0f)
		{
			transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
		}
		else
		{
			transform.localPosition = new Vector3(-x, transform.localPosition.y, transform.localPosition.z);
		}
	}

	public static void SetLocalScaleX(this Transform transform, float x)
	{
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}

	public static void SetLocalScale(this Transform transform, float x, float y, float z)
	{
		transform.localScale = new Vector3(x, y, z);
	}

	public static void SetAbsLocalScaleX(this Transform transform, float x)
	{
		if (transform.lossyScale.x > 0f)
		{
			transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
		}
		else
		{
			transform.localScale = new Vector3(-x, transform.localScale.y, transform.localScale.z);
		}
	}

	// float & double extensions — allow comparison of two floating point numbers within a given margin of error.
	public static bool FloatEquals(this float num1, float num2, float threshold = .0001f)
	{
		return Math.Abs(num1 - num2) < threshold;
	}

	public static bool DoubleEquals(this double num1, double num2, double threshold = .0001f)
	{
		return Math.Abs(num1 - num2) < threshold;
	}

	// rigidbody2D extensions.
	public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
	{
		Vector3 dir = (body.transform.position - explosionPosition);
		float wearoff = 1 - (dir.magnitude / explosionRadius);
		body.AddForce(dir.normalized * explosionForce * wearoff);
	}

	public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
	{
		Vector3 dir = (body.transform.position - explosionPosition);
		float wearoff = 1 - (dir.magnitude / explosionRadius);
		Vector3 baseForce = dir.normalized * explosionForce * wearoff;
		body.AddForce(baseForce);

		float upliftWearoff = 1 - upliftModifier / explosionRadius;
		Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
		body.AddForce(upliftForce);
	}
}
