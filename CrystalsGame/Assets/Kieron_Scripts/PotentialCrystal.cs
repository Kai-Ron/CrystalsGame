using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialCrystal : MonoBehaviour
{
    public float power = 0.0f;
    public bool powered = false;
    public Energy energy;
    public Rigidbody2D rb;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        energy = gameObject.GetComponent<Energy>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(energy.grabbed)
        {
            rb = player.GetComponent<Rigidbody2D>();
            //Debug.Log(rb.gameObject);
        }
        else
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        if(energy.source)
        {
            energy.source = null;
        }
    }

    void FixedUpdate()
    {
        if(rb.velocity.magnitude > 0.2f /*&& power < rb.velocity.magnitude*/)
        {
            power += 0.02f;
        }
        else
        {
            if(power <= 0)
            {
                power = 0.0f;
            }
            else
            {
                power -= 0.02f;
            }
        }

        energy.power = power;
        
        if(power > 0 && !powered)
        {
            powered = true;
        }
        else if(power <= 0 && powered)
        {
            energy.power = 0;
            powered = false;
        }
    }
}
