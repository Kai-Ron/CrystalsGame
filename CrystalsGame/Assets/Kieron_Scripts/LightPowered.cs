using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPowered : MonoBehaviour
{
    public Energy source;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
