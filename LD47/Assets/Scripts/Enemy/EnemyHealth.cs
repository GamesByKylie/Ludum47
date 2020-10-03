using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Health
{
    public override void Die()
    {
        base.Die();
        SceneManager.LoadScene(2);
    }
}
