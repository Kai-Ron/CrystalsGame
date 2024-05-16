using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Script : MonoBehaviour
{
    public bool opened;
    public Energy source;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(source)
        {
            if(source.power <= 0.0f && opened == true)
            {
                opened = false;
                animator.SetBool("Opened", opened);
            }
            else if(source.power > 0.0f && opened == false)
            {
                opened = true;
                animator.SetBool("Opened", opened);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Energy>())
        {
            if(!source)
            {
                source = collider.gameObject.GetComponent<Energy>();
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
            }
        }
    }
}
