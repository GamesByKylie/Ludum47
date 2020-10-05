using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    public Weapon swingAttack;
    public Weapon stunAttack;
    public Weapon jumpAttack;
    public float jumpTriggerRange;

    [HideInInspector]public int phase = 1;

    private float stunTimer = 0.0f;
    private float jumpTimer = 0.0f;
    
    private void Update()
    {
        if (timing)
        {
            timer += Time.fixedDeltaTime;
            stunTimer += Time.fixedDeltaTime;
            jumpTimer += Time.fixedDeltaTime;
        }
        switch (phase)
        {
            case (1):
                //Phase 1 - using primaryAttack only
                SwapIfNeeded(primaryAttack);
                CheckAttack(ref timer, "Attack", primaryAttack.range);
                break;
            case (2):
                //Phase 2 - using swingAttack if the player is close and stunAttack if not
                if (InRange(swingAttack))
                {
                    SwapIfNeeded(swingAttack);
                    CheckAttack(ref timer, "Attack", swingAttack.range);
                }
                else if (InRange(stunAttack))
                {
                    SwapIfNeeded(stunAttack);
                    CheckAttack(ref stunTimer, "Stun", stunAttack.range);
                }

                break;
            case (3):
                //Phase 3 - using swingAttack if the player is close, stunAttack if medium, and jumpAttack if far
                if (InRange(swingAttack))
                {
                    SwapIfNeeded(swingAttack);
                    CheckAttack(ref timer, "Attack", swingAttack.range);
                }
                else if (InRange(stunAttack))
                {
                    SwapIfNeeded(stunAttack);
                    CheckAttack(ref stunTimer, "Stun", stunAttack.range);
                }
                else if (Vector3.Distance(transform.position, target.transform.position) <= jumpTriggerRange)
                {
                    Debug.Log("In range for jump");
                    SwapIfNeeded(jumpAttack);
                    CheckAttack(ref jumpTimer, "Jump", jumpTriggerRange);
                }

                break;
        }

    }

    private void CheckAttack(ref float timer, string animTrigger, float range)
    {
        if (timer >= currentWeapon.cooldown && InRange(range))
        {
            Debug.Log($"Setting {animTrigger}");
            anim.SetTrigger(animTrigger);
            timer = 0.0f;
            PauseTiming();
        }
        else if (timer < currentWeapon.cooldown)
        {
            Debug.Log($"Still waiting on {currentWeapon.weaponName} cooldown");
        }
    }

    private void SwapIfNeeded(Weapon w)
    {
        if (!currentWeapon.Equals(w))
        {
            currentWeapon = w;
        }
    }

    public void StunAttack()
    {
        Debug.Log("Stunning roar");
        stunTimer = 0.0f;

    }

    public void JumpAttack()
    {
        Debug.Log("Jumping pound");
        jumpTimer = 0.0f;

    }
}
