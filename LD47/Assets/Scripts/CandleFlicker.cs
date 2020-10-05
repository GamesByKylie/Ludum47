using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFlicker : MonoBehaviour
{
    public float flickerLimit;
    public float flickerIntensity;

    private Light l;
    private float initialIntensity;
    private bool flickering = true;

    private void Start()
    {
        l = GetComponent<Light>();
        initialIntensity = l.intensity;
    }

    private void Update()
    {
        if (flickering)
        {
            l.intensity += Random.Range(-flickerIntensity, flickerIntensity);
            l.intensity = Mathf.Clamp(l.intensity, 0, flickerLimit);
        }
    }

    public void Extinguish()
    {
        if (l == null)
        {
            l = GetComponent<Light>();
        }
        l.intensity = 0;
        flickering = false;
    }

    public void Light()
    {
        l.intensity = initialIntensity;
        flickering = true;
    }

}
