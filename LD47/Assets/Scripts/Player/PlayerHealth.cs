using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    public override void Die()
    {
        Debug.Log("Player is dead, respawning...");
        //Reset position
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        currentHealth = maxHealth;
        UpdateHealthbar();

        //Reset maze timer

        //Reset minotaur

        //Reset crown and sword
        
        //Reset any thread progress past the last checkpoint

        //Reset conversation progress

    }
}
