using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public event Action OnPlayerDeath;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
    }

    private void OnEnable()
    {
        healthBar.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        healthBar.gameObject.SetActive(false);
    }

    public override void Die()
    {
        OnPlayerDeath?.Invoke();
        Debug.Log("Player is dead, respawning...");
        //Reset position
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        currentHealth = maxHealth;
        UpdateHealthbar();
    }
}
