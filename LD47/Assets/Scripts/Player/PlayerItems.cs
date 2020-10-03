using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public Item goldenYarn;
    public Item sword;
    public Item crown;

    public GameObject wornCrown;

    private PlayerHealth ph;

    private void Start()
    {
        ph = GetComponent<PlayerHealth>();
        wornCrown.SetActive(false);

        ph.OnPlayerDeath += Items_OnPlayerDeath;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Item i = other.GetComponent<Item>();
            if (i.itemName == goldenYarn.itemName)
            {
                Debug.Log("You just picked up the golden yarn");
                i.gc.thread.gameObject.SetActive(true);
            }
            else if (i.itemName == sword.itemName)
            {
                Debug.Log("You just picked up the sword!");
                GetComponent<PlayerAttack>().SwitchWeapon(GetComponent<PlayerAttack>().weapon);
            }
            else if (i.itemName == crown.itemName)
            {
                Debug.Log("You just picked up the crown!");
                wornCrown.SetActive(true);
            }
            else
            {
                Debug.Log("Unknown item");
            }

            Destroy(other.gameObject);
        }
    }

    private void Items_OnPlayerDeath()
    {
        wornCrown.SetActive(false);
        GetComponent<PlayerAttack>().SwitchWeapon(GetComponent<PlayerAttack>().unarmed);
    }
}
