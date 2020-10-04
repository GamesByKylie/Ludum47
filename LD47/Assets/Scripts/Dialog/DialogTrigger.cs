using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogController dc;
    public PlayerHealth ph;

    private bool playerInRange = false;
    private bool dialogCompleted = false;

    private void Start()
    {
        ph.OnPlayerDeath += DialogTrigger_OnPlayerDeath;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && !dialogCompleted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            dc.StartDialog();
            dialogCompleted = true;
        }
    }

    private void DialogTrigger_OnPlayerDeath()
    {
        dialogCompleted = false;
    }
}
