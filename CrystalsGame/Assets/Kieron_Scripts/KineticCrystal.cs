using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticCrystal : MonoBehaviour
{
    public float power = 0.0f;
    public bool powered = false;
    public Energy energy;
    public Rigidbody2D rb;
    public Vector2 direction;
    //RigidbodyConstraints2D constraints;
    
    // Start is called before the first frame update
    void Start()
    {
        energy = gameObject.GetComponent<Energy>();
        rb = gameObject.GetComponent<Rigidbody2D>();
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
                rb.velocity = new Vector2(0.0f, 0.0f);
                rb.gravityScale = 0.0f;
                //rb.bodyType = RigidbodyType2D.Kinematic;
                //rb.constraints = RigidbodyConstraints2D.FreezeAll;
                powered = true;
            }
            //rb.AddForce(direction);
            rb.MovePosition(rb.position + (direction * Time.fixedDeltaTime));
            //power -= 0.02f;
        }
        else if(power <= 0)
        {
            if(powered)
            {
                //rb.constraints = RigidbodyConstraints2D.None;
                //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = new Vector2(0.0f, 0.0f);
                rb.gravityScale = 1.0f;
                powered = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Energy>())
        {
            if(collider.gameObject.GetComponent<Energy>().power > 0.0f)
            {
                if(collider.gameObject.GetComponent<Energy>().source != energy)
                {
                    bool move = true;

                    if(powered && collider.gameObject.GetComponent<Energy>().interactable)
                    {
                        move = false;
                    }

                    if(move)
                    {
                        //direction = collider.gameObject.energy.direction;
                        float xDir = transform.position.x - collider.transform.position.x;
                        float yDir = transform.position.y - collider.transform.position.y;
                        float threshold = 0.25f;
                        float speed = 1.0f;

                        if(xDir >= threshold)
                        {
                            xDir = speed;
                        }
                        else if(xDir <= -threshold)
                        {
                            xDir = -speed;
                        }
                        else
                        {
                            xDir = 0.0f;
                        }

                        if(yDir >= threshold)
                        {
                            yDir = speed;
                        }
                        else if(yDir <= -threshold)
                        {
                            yDir = -speed;
                        }
                        else
                        {
                            yDir = 0.0f;
                        }

                        direction = new Vector2(xDir, yDir);

                        Debug.Log(direction);
                    }
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Energy>())
        {
            if(collision.gameObject.GetComponent<Energy>().power > 0.0f)
            {
                if(collision.gameObject.GetComponent<Energy>().source != energy)
                {
                    bool move = true;

                    if(powered & collision.gameObject.GetComponent<Energy>().interactable)
                    {
                        move = false;
                    }

                    if(move)
                    {
                        //direction = collision.gameObject.energy.direction;
                        float xDir = transform.position.x - collision.transform.position.x;
                        float yDir = transform.position.y - collision.transform.position.y;
                        float threshold = 0.5f;
                        float speed = 1.0f;

                        if(xDir >= threshold)
                        {
                            xDir = speed;
                        }
                        else if(xDir <= -threshold)
                        {
                            xDir = -speed;
                        }
                        else
                        {
                            xDir = 0.0f;
                        }

                        if(yDir >= threshold)
                        {
                            yDir = speed;
                        }
                        else if(yDir <= -threshold)
                        {
                            yDir = -speed;
                        }
                        else
                        {
                            yDir = 0.0f;
                        }

                        direction = new Vector2(xDir, yDir);

                        Debug.Log(direction);
                    }
                }
            }
        }
    }
}
