using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    private void Update()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= currentWeapon.cooldown && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Player attacking at " + timer);
            anim.SetTrigger("Attack");
            timer = 0.0f;
        }
    }
}
