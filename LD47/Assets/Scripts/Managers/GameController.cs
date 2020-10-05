﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerHealth ph;
    public StartMaze sm;
    public StartBoss sb;
    public DialogController dc;
    public float timeLimit = 2f;
    public Slider timeIndicator;
    public CandleFlicker timeCandle;
    public GameObject minotaur;
    public Transform minotaurSpawnPoint;
    public GameObject instructions;
    public float instructionsDuration;

    [Header("Items")]
    public Item crown;
    public Transform crownSpawn;
    public Item goldenThreadPickup;
    public Transform threadSpawn;
    public Item sword;
    public Transform swordSpawn;
    public GoldenThread thread;

    private GameObject crownObj;
    private GameObject threadPickupObj;
    private GameObject swordObj;

    private float timer;
    private bool timing = false;

    private bool sbInitialized = false;
    private GameObject minotaurObj;
    private int deathNum = 0;

    public bool ShowTimer
    {
        get; set;
    }

    public bool EarnedCrown
    {
        get; set;
    }

    public bool EarnedThread
    {
        get; set;
    }

    public bool EarnedSword
    {
        get; set;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sm.OnMazeStart += Controller_OnMazeStart;
        ph.OnPlayerDeath += Controller_OnPlayerDeath;
        timeCandle.Extinguish();
        StartCoroutine(HideInstructions());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!dc.dialogRunning && Input.GetKeyDown(KeyCode.Mouse0) && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public GameObject SpawnItem(Item i, Transform spawn)
    {
        Item spawned = Instantiate(i);
        spawned.transform.position = spawn.position;
        spawned.transform.rotation = spawn.rotation;
        spawned.gc = this;
        return spawned.gameObject;
    }

    private void Controller_OnPlayerDeath()
    {
        if (crownObj != null)
        {
            Destroy(crownObj);
        }
        if (threadPickupObj != null)
        {
            Destroy(threadPickupObj);
        }
        if (swordObj != null)
        {
            Destroy(swordObj);
        }

        EarnedCrown = false;
        EarnedSword = false;
        EarnedThread = false;

        timing = false;
        timer = timeLimit;
        ShowTimer = false;
        timeIndicator.gameObject.SetActive(false);
        timeCandle.Extinguish();

        if (minotaurObj != null)
        {
            Destroy(minotaurObj);
        }

        if (deathNum == 0)
        {
            dc.StartDialog("AthenaStart");
        }

        deathNum++;
    }

    private void Controller_OnMazeStart()
    {
        if (ShowTimer)
        {
            timeIndicator.gameObject.SetActive(true);
            timeIndicator.maxValue = timeLimit;
            timeIndicator.value = timeLimit;
            timeCandle.Light();
        }
        else
        {
            timeCandle.Extinguish();
        }

        timer = timeLimit;
        timing = true;

        if (EarnedCrown)
        {
            Debug.Log("Spawning crown");
            crownObj = SpawnItem(crown, crownSpawn);
        }
        
        if (EarnedSword)
        {
            Debug.Log("Spawning sword");
            swordObj = SpawnItem(sword, swordSpawn);
        }
    }

    private void Controller_OnPlayerEnterBossRoom()
    {
        timing = false;
        timeIndicator.gameObject.SetActive(false);
        minotaurObj = Instantiate(minotaur, minotaurSpawnPoint.position, minotaurSpawnPoint.rotation);
        minotaurObj.GetComponent<EnemyMovement>().target = ph.transform;
        minotaurObj.GetComponent<EnemyAttack>().target = ph;

        ph.GetComponent<PlayerAttack>().target = minotaurObj.GetComponent<EnemyHealth>();
    }

    private void FixedUpdate()
    {
        if (!sbInitialized && sb != null)
        {
            sb.OnPlayerEnterBossRoom += Controller_OnPlayerEnterBossRoom;
            sbInitialized = true;
        }

        if (timing)
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                ph.TakeDamage(2);
                timeCandle.Extinguish();
            }
            else
            {
                timeIndicator.value = timer;
            }
        }
    }

    public void TrySpawnThread()
    {
        Debug.Log("Trying to spawn thread object");
        if (EarnedThread)
        {
            Debug.Log("Spawning thread object");
            threadPickupObj = SpawnItem(goldenThreadPickup, threadSpawn);
        }
    }

    private IEnumerator HideInstructions()
    {
        yield return new WaitForSeconds(instructionsDuration);
        instructions.SetActive(false);
    }

}
