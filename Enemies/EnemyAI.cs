﻿using Matcha.Game.Colors;   // testing only
using UnityEngine;
using System.Collections;

public class EnemyAI : CacheBehaviour {

    public float attackInterval = 1f;
    public float chanceOfAttack = 40f;
    public float attackWhenInRange = 20f;

    private ProjectileManager projectile;
    private Weapon weapon;
    private Transform target;

    public bool test;
    public GameObject[] targets;    // testing only

	void Start()
    {
        projectile = GetComponent<ProjectileManager>();
        weapon = GetComponentInChildren<Weapon>();
        target = GameObject.Find(PLAYER).transform;
	}

    void OnBecameVisible()
    {
        InvokeRepeating("LookAtTarget", 1f, .3f);

        if (test) {
            StartCoroutine(LobCompTest());
        } else {
            InvokeRepeating("AttackRandomly", 2f, attackInterval);
        }
    }

    void OnBecameInvisible()
    {
        CancelInvoke("LookAtTarget");
        if (!test)
            CancelInvoke("AttackRandomly");
    }

    void LookAtTarget()
    {
        int direction = (target.position.x > transform.position.x) ? RIGHT : LEFT;
        transform.localScale = new Vector3((float)direction, transform.localScale.y, transform.localScale.z);
    }

    void AttackRandomly()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= attackWhenInRange)
        {
            if (Random.Range(1, 101) <= chanceOfAttack)
                projectile.FireAtTarget(weapon, target);
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 vel = GetForceFrom(transform.position,target.position);
        float angle = Mathf.Atan2(vel.y, vel.x)* Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        float power = 1;
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y))*power;
    }








    // TARGET TESTING SUITE
    // ####################

    IEnumerator LobCompTest()
    {
        int i = 0;
        int j = i - 1;

        while (true)
        {
            if (i >= targets.Length)
                i = 0;
            if (j >= targets.Length)
                j = 0;
            if (j < 0)
                j = 10;

            targets[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", MColor.orange);
            targets[j].GetComponent<SpriteRenderer>().material.SetColor("_Color", MColor.white);
            projectile.FireAtTarget(weapon, targets[i].transform);
            i++;
            j++;
            yield return new WaitForSeconds(2);
        }
    }

}