using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public event Action OnPlayerEnterBossRoom;
    public Transform playerEntrance;
    public Transform minotaurSpawn;

    private bool started = false;

    private void Start()
    {
        GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gc.sb = this;
        gc.minotaurSpawnPoint = minotaurSpawn;
        gc.ph.OnPlayerDeath += Boss_OnPlayerDeath;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!started && other.CompareTag("Player"))
        {
            other.transform.position = playerEntrance.position;
            other.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            started = true;
            StartCoroutine(StartBattle());
        }
    }

    private IEnumerator StartBattle()
    {
        yield return null;
        OnPlayerEnterBossRoom?.Invoke();
        yield return null;
    }

    private void Boss_OnPlayerDeath()
    {
        started = false;
    }
}
