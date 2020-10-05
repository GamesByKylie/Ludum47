using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    public Weapon swingAttack;
    public Weapon stunAttack;
    public Weapon jumpAttack;

    [Header("Special Attacks")]
    public float stunDuration;
    public float jumpTriggerRange;
    public float jumpDistance;
    public GameObject jumpGroundBullet;
    public Transform[] bulletSpawns;


    [HideInInspector]public int phase = 1;

    private float stunTimer = 0.0f;
    private float jumpTimer = 0.0f;

    private PlayerMovement pm;
    private EnemyMovement em;
    private bool setPM = false;

    private void Start()
    {
        if (pm == null)
        {
            pm = target.GetComponent<PlayerMovement>();
            setPM = true;
        }

        em = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (!setPM && pm == null)
        {
            pm = target.GetComponent<PlayerMovement>();
            setPM = true;
        }

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
            anim.SetTrigger(animTrigger);
            timer = 0.0f;
            PauseTiming();
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
        stunTimer = 0.0f;

        if (InRange(stunAttack))
        {
            StartCoroutine(pm.FreezeForTime(stunDuration));
        }
    }

    public void MoveJump()
    {
        jumpTimer = 0.0f;

        em.JumpForward(jumpDistance);
    }

    public void JumpAttack()
    {
        //Spawn ground bullet things
        if (InRange(jumpAttack))
        {
            target.TakeDamage(jumpAttack.damage);
        }

        foreach (Transform t in bulletSpawns)
        {
            Instantiate(jumpGroundBullet, t.position, t.rotation);
        }
    }
}
