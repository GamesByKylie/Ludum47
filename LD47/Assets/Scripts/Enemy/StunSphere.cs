using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunSphere : MonoBehaviour
{
    public float speed;

    public void StartExpansion(float size)
    {
        StartCoroutine(Expand(size));
    }

    private IEnumerator Expand(float size)
    {
        while (transform.localScale.x < size)
        {
            yield return null;
            transform.localScale += Vector3.one * speed * Time.deltaTime;
        }

        Destroy(gameObject);
    }
}
