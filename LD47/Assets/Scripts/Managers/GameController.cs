using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerHealth ph;
    public StartMaze sm;
    public StartBoss sb;
    public float timeLimit = 2f;
    public Slider timeIndicator;
    public GameObject minotaur;
    public Transform minotaurSpawnPoint;

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

    private bool sbInitialized = false;
    private GameObject minotaurObj;

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

        if (minotaurObj != null)
        {
            Destroy(minotaurObj);
        }
    }

    private void Controller_OnMazeStart()
    {
        timeIndicator.gameObject.SetActive(true);
        timeIndicator.maxValue = timeLimit;
        timeIndicator.value = timeLimit;
        timer = timeLimit;
        timing = true;
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
            }
            else
            {
                timeIndicator.value = timer;
            }
        }
    }

}
