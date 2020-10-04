using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    public float range;
    
    private void Update()
    {
        if (timing)
        {
            timer += Time.fixedDeltaTime;
        }
        
        if (timer >= currentWeapon.cooldown && Vector3.Distance(target.transform.position, transform.position) <= range)
        {
            anim.SetTrigger("Attack");
            timer = 0.0f;
            PauseTiming();
        }
    }

    public void StunAttack()
    {
        Debug.Log("Stunning roar");
    }

    public void JumpAttack()
    {
        Debug.Log("Jumping pound");
    }

}
