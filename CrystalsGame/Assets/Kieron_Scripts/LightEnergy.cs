using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightEnergy : MonoBehaviour
{
    public float raycastDistance = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<LightPowered>())
        {
            //Physics2D.queriesStartInColliders = false;

            LayerMask mask = LayerMask.GetMask("TransparentFX");

            //Debug.DrawRay(transform.position, (collider.transform.position - transform.position), Color.red);
            
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, (collider.transform.position - transform.position), raycastDistance, mask);

            if(raycast.collider == collider)
            {
                if(collider.gameObject.GetComponent<LightPowered>().source != gameObject.GetComponentInParent<Energy>() && collider.gameObject.GetComponent<LightPowered>().source != null)
                {
                    if(collider.gameObject.GetComponent<LightPowered>().source.power < gameObject.GetComponentInParent<Energy>().power)
                    {
                        collider.gameObject.GetComponent<LightPowered>().source = gameObject.GetComponentInParent<Energy>();
                    }
                }
                else
                {
                    collider.gameObject.GetComponent<LightPowered>().source = gameObject.GetComponentInParent<Energy>();
                }

                //Debug.Log(raycast.collider.name);
            }
            else if(collider.gameObject.GetComponent<LightPowered>().source == gameObject.GetComponentInParent<Energy>())
            {
                collider.gameObject.GetComponent<LightPowered>().source = null;
            }
            //Debug.Log("Detected!");
        }
    }

    void TriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<LightPowered>())
        {
            if(collider.gameObject.GetComponent<LightPowered>().source == gameObject.GetComponentInParent<Energy>())
            {
                collider.gameObject.GetComponent<LightPowered>().source = null;
            }

            //Debug.Log("Detected!");
        }

        Debug.Log(collider.name);
    }
}
