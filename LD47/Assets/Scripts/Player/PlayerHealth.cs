using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Image damageFlash;
    public float flashFadeSpeed;
    public Color damageFlashColor = Color.red;

    public event Action OnPlayerDeath;

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

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        damageFlash.color = damageFlashColor;
        while (damageFlash.color.a >= 0)
        {
            damageFlash.color = Color.Lerp(damageFlash.color, Color.clear, flashFadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
