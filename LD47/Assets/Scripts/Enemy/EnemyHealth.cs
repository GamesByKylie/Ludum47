using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Health
{
    [Range(0f, 1f)]
    public float phase2TriggerPercent = 0.7f;
    [Range(0f, 1f)]
    public float phase3TriggerPercent = 0.3f;

    private EnemyAttack ea;
    private EnemyMovement em;
    private Animator anim;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        UpdateHealthbar();

        ea = GetComponent<EnemyAttack>();
        em = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        switch (ea.phase)
        {
            case (1):
                if (currentHealth / maxHealth <= phase2TriggerPercent)
                {
                    Debug.Log("Triggering phase 2");
                    anim.SetTrigger("Phase2");
                    ea.phase = 2;
                    em.speed = em.phase2speed;
                }
                break;
            case (2):
                if (currentHealth / maxHealth <= phase3TriggerPercent)
                {
                    Debug.Log("Triggering phase 3");
                    ea.phase = 3;
                    em.speed = em.phase3speed;
                }
                break;
        }
    }

    public override void Die()
    {
        base.Die();
        anim.SetTrigger("Die");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(2);
    }
}
