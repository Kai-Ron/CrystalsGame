using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Energy : MonoBehaviour
{
    public float power = 0.0f;
    public float intensity;
    public float maxThreshold = 10.0f, minThreshold = 1.0f;
    public int multiplier = 10;
    public Energy source;
    public Light2D lightSource;
    public bool interactable;
    
    // Start is called before the first frame update
    void Start()
    {
        lightSource = gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(source)
        {
            power = source.power;
        }
        else if(!source && power > 0.0f)
        {
            power -= 0.02f;
        }
        else if(!source && power < 0.0f)
        {
            power = 0.0f;
        }

        if(lightSource)
        {
            if((power/multiplier) > maxThreshold)
            {
                intensity = maxThreshold;
            }
            else if((power/multiplier) < minThreshold)
            {
                intensity = minThreshold;
            }
            else
            {
                intensity = power/multiplier;
            }

            lightSource.intensity = intensity;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Energy>())
        {
            if(source)
            {
                if(source.power <= 0.0f)
                {
                    source = null;
                    //power = 0;
                }
                else if(power < collider.gameObject.GetComponent<Energy>().power && collider.gameObject.GetComponent<Energy>().source != gameObject.GetComponent<Energy>())
                {
                    source = collider.gameObject.GetComponent<Energy>();
                    power = source.power;
                }
            }
            else if(power < collider.gameObject.GetComponent<Energy>().power && collider.gameObject.GetComponent<Energy>().source != gameObject.GetComponent<Energy>())
            {
                source = collider.gameObject.GetComponent<Energy>();
                power = source.power;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Energy>())
        {
            if(collider.gameObject.GetComponent<Energy>() == source)
            {
                source = null;
                //power = 0.0f;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Energy>())
        {
            if(source)
            {
                if(source.power <= 0.0f)
                {
                    source = null;
                    //power = 0;
                }
                else if(power < collision.gameObject.GetComponent<Energy>().power && collision.gameObject.GetComponent<Energy>().source != gameObject.GetComponent<Energy>())
                {
                    source = collision.gameObject.GetComponent<Energy>();
                    power = source.power;
                }
            }
            else if(power < collision.gameObject.GetComponent<Energy>().power && collision.gameObject.GetComponent<Energy>().source != gameObject.GetComponent<Energy>())
            {
                source = collision.gameObject.GetComponent<Energy>();
                power = source.power;
            }
            //Debug.Log(gameObject.name + " collisions working");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Energy>())
        {
            if(collision.gameObject.GetComponent<Energy>() == source)
            {
                source = null;
                //power = 0.0f;
            }
        }
    }
}
