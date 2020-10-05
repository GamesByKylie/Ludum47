using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    public Weapon sword;

    private void Start()
    {
        sword.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (timing)
        {
            timer += Time.fixedDeltaTime;
        }
        
        if (timer >= currentWeapon.cooldown && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentWeapon.Equals(primaryAttack))
            {
                anim.SetTrigger("Attack");
            }
            else
            {
                anim.SetTrigger("Sword");
            }

            timer = 0.0f;
        }


    }
}
