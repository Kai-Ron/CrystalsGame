using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPowered : MonoBehaviour
{
    public Energy source;
    public Light2D light2D;
    
    // Start is called before the first frame update
    void Start()
    {
        light2D = gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(source)
        {
            if(source.power <= 0.0f)
            {
                source = null;
            }
            else
            {
                light2D.intensity = 1.0f;
            }
        }
        else
        {
            light2D.intensity = 0.0f;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(source && collider.gameObject.GetComponent<Energy>())
        {
            if(source.power > 0.0f && collider.gameObject.GetComponent<Energy>().source)
            {
                if(collider.gameObject.GetComponent<Energy>().source.power < source.power)
                {
                    collider.gameObject.GetComponent<Energy>().source = source;
                }
            }
            else if(source.power > 0.0f)
            {
                collider.gameObject.GetComponent<Energy>().source = source;
            }
            else if(collider.gameObject.GetComponent<Energy>().source == source)
            {
                collider.gameObject.GetComponent<Energy>().source = null;
                source = null;
            }
        }
        else if(!source && collider.gameObject.GetComponent<Energy>())
        {
            if(collider.gameObject.GetComponent<Energy>().interactable == false)
            {
                collider.gameObject.GetComponent<Energy>().source = null;
                collider.gameObject.GetComponent<Energy>().power = 0.0f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Energy>())
        {
            if(collider.gameObject.GetComponent<Energy>().source == source)
            {
                collider.gameObject.GetComponent<Energy>().source = null;
            }
        }

        if(collider.gameObject.GetComponent<LightEnergy>())
        {
            if(collider.gameObject.GetComponentInParent<Energy>() == source)
            {
                source = null;
            }
            Debug.Log(collider.name);
        }
    }
}
