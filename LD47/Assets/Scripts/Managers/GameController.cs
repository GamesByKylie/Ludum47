using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerHealth ph;
    public StartMaze sm;
    public float timeLimit = 2f;
    public Slider timeIndicator;

    [Header("Items")]
    public Item crown;
    public Item goldenThreadPickup;
    public Item sword;
    public GoldenThread thread;

    private GameObject crownObj;
    private GameObject threadPickupObj;
    private GameObject swordObj;

    private float timer;
    private bool timing = false;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        crownObj = SpawnItem(crown);
        threadPickupObj = SpawnItem(goldenThreadPickup);
        swordObj = SpawnItem(sword);

        sm.OnMazeStart += Controller_OnMazeStart;
        ph.OnPlayerDeath += Controller_OnPlayerDeath;
    }

    public GameObject SpawnItem(Item i)
    {
        Item spawned = Instantiate(i);
        spawned.transform.position = new Vector3(Random.Range(-5f, -2f), 0f, Random.Range(-5f, -2f));
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

        crownObj = SpawnItem(crown);
        threadPickupObj = SpawnItem(goldenThreadPickup);
        swordObj = SpawnItem(sword);

        timing = false;
        timer = timeLimit;

        timeIndicator.gameObject.SetActive(false);
    }

    private void Controller_OnMazeStart()
    {
        timeIndicator.gameObject.SetActive(true);
        timeIndicator.maxValue = timeLimit;
        timeIndicator.value = timeLimit;
        timer = timeLimit;
        timing = true;
    }

    private void FixedUpdate()
    {
        if (timing)
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                ph.TakeDamage(2);
            }
            else
            {
                timeIndicator.value = timer;
            }
        }
    }

}
