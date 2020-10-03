using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(15);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        Debug.Log($"Player health {currentHealth}");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Debug.Log("Player is dead, respawning...");
        //Reset position
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        //Reset maze timer

        //Reset minotaur

        //Reset crown and sword
        
        //Reset any thread progress past the last checkpoint

        //Reset conversation progress

    }
}
