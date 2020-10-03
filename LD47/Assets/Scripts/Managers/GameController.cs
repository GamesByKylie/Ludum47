using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerHealth ph;

    [Header("Items")]
    public Item crown;
    public Item goldenThreadPickup;
    public Item sword;
    public GoldenThread thread;

    private GameObject crownObj;
    private GameObject threadPickupObj;
    private GameObject swordObj;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        crownObj = SpawnItem(crown);
        threadPickupObj = SpawnItem(goldenThreadPickup);
        swordObj = SpawnItem(sword);

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
    }
}
