using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightCrystal : MonoBehaviour
{
    public float power = 0.0f;
    public bool powered = false;
    public Energy energy;
    public GameObject lightSource;
    public CircleCollider2D cc;

    public float intensity;
    public float maxThreshold = 10.0f, minThreshold = 0.1f;
    public int multiplier = 10;
    public Light2D light2D;

    // Start is called before the first frame update
    void Start()
    {
        energy = gameObject.GetComponent<Energy>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    
    void FixedUpdate()
    {
        power = energy.power;

        if(power > 0)
        {
            if(!powered)
            {
                lightSource.SetActive(true);
                powered = true;
            }
            AdjustLight();
            //power -= 0.02f;
        }
        else if(power <= 0)
        {
            if(powered)
            {
                lightSource.SetActive(false);
                powered = false;
            }
        }
    }

    public void AdjustLight()
    {
        if((power) > maxThreshold)
        {
            intensity = maxThreshold;
        }
        else if((power) < minThreshold)
        {
            intensity = minThreshold;
        }
        else
        {
            intensity = power;
        }

        light2D.pointLightInnerRadius = intensity/2;
        light2D.pointLightOuterRadius = intensity;
        light2D.intensity = intensity/multiplier;
        cc.radius = intensity;
        lightSource.GetComponent<LightEnergy>().raycastDistance = intensity;
    }
}
