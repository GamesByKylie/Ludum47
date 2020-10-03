using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public event Action OnPlayerEnterBossRoom;
    public Transform playerEntrance;
    public Transform minotaurSpawn;

    private void Start()
    {
        GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gc.sb = this;
        gc.minotaurSpawnPoint = minotaurSpawn;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = playerEntrance.position;
            other.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            StartCoroutine(StartBattle());
        }
    }

    private IEnumerator StartBattle()
    {
        yield return null;
        OnPlayerEnterBossRoom?.Invoke();
    }
}
