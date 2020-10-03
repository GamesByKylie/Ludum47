using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public Item goldenYarn;
    public Item sword;
    public Item crown;

    public GameObject wornCrown;

    private void Start()
    {
        wornCrown.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Item i = other.GetComponent<Item>();
            if (i.name == goldenYarn.name)
            {
                Debug.Log("You just picked up the golden yarn");
            }
            else if (i.name == sword.name)
            {
                Debug.Log("You just picked up the sword!");
                GetComponent<PlayerAttack>().SwitchWeapon(GetComponent<PlayerAttack>().weapon);
            }
            else if (i.name == crown.name)
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
}
