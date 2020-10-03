using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public Slider healthBar;

    protected float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        UpdateHealthbar();
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        //Prevent it from going below 0 for graphics reasons
        currentHealth = Mathf.Max(currentHealth, 0f);
        Debug.Log($"{gameObject.name} health at {currentHealth}");
        UpdateHealthbar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void UpdateHealthbar()
    {
        healthBar.value = currentHealth;
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} died");
    }
}
