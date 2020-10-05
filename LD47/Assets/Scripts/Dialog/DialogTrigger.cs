﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogController dc;
    public PlayerHealth ph;
    public string startNode;
    public GameObject controlsText;

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
            controlsText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            controlsText.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && !dialogCompleted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            dc.StartDialog(startNode);
            dialogCompleted = true;
        }
    }

    private void DialogTrigger_OnPlayerDeath()
    {
        dialogCompleted = false;
    }
}
