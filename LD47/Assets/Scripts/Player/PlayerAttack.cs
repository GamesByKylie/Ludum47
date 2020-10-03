using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("Attack");
        }
    }
}
