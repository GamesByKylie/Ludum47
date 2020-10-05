using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBullet : MonoBehaviour
{
    public float damage;
    public float speed;

    private Rigidbody rb;

    private bool triggered = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward.normalized * speed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player hit the thing");
                other.GetComponent<PlayerHealth>().TakeDamage(damage);
                Destroy(gameObject);
                triggered = true;
            }
            else if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
                triggered = true;
            }
        }
    }
}
