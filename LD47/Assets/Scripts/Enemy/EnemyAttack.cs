using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    public float range;
    public float cooldown;

    private float timer;

    private void Update()
    {
        timer += Time.fixedDeltaTime;
        Debug.Log(timer);

        if (timer >= cooldown && Vector3.Distance(target.transform.position, transform.position) <= range)
        {
            anim.SetTrigger("Attack");
            timer = 0.0f;
        }
    }
}
