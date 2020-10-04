using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{

    private void Start()
    {
        weapon.gameObject.SetActive(false);
    }

    private void Update()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= currentWeapon.cooldown && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentWeapon.Equals(unarmed))
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
