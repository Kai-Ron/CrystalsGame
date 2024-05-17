using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float move;
    public float jump;
    //public GameObject target;
    private bool flip = false;
    Animator animator;
    private Vector2 inputValue;
    float horizInput;
    float vertInput;
    //bool isWalking;

    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    public bool isGrounded;

    [SerializeField] private GameObject grabbedObject;

    // Start is called before the first frame update
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        horizInput = rb.velocity.x;
        vertInput = rb.velocity.y;
        animator.SetFloat("hInput", horizInput);
        animator.SetFloat("vInput", vertInput);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if(horizInput > 0.2f || horizInput < -0.2f)
            {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
        else if(horizInput < 0.2f && horizInput > -0.2f)
        {
            animator.SetBool("Walking", false);
        }

        if(isGrounded == true)
        {
            animator.SetBool("Grounded", true);
        }
        else if(isGrounded == false)
        {
            animator.SetBool("Grounded", false);
        }

        if (move < 0 && flip != true)
        {
            playerFlip();
        }
        else if(move > 0 && flip == true)
        {
            playerFlip();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded == true)
            {
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.C) && grabbedObject == null)
        {
            Physics2D.queriesStartInColliders = true;
            
            LayerMask mask = LayerMask.GetMask("Crystals");

            RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance, mask);

            Debug.DrawRay(rayPoint.position, transform.right * rayDistance, Color.red);

            if(hitInfo.collider != null && hitInfo.collider.gameObject.tag == "Object")
            {
                grabbedObject = hitInfo.collider.gameObject;
                animator.SetBool("Holding", true);
                if (hitInfo.collider.gameObject.GetComponent<Energy>())
                {
                    grabbedObject.GetComponent<Energy>().grabbed = true;
                    Debug.Log(grabbedObject);
                }

                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
                Debug.Log("C Pick Up");
            }
            else if(hitInfo.collider == null)
            {
                Debug.Log("Collider Null");
            }
            else
            {
                Debug.Log(hitInfo.collider.gameObject);
            }
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            Physics2D.queriesStartInColliders = true;
            
            LayerMask mask = LayerMask.GetMask("Crystals");

            RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance, mask);

            Debug.DrawRay(rayPoint.position, transform.right * rayDistance, Color.red);

            if(hitInfo.collider != null && hitInfo.collider.gameObject.tag == "Object")
            {
                if(hitInfo.collider.gameObject.GetComponent<Energy>())
                {
                    grabbedObject.GetComponent<Energy>().grabbed = false;
                    Debug.Log(grabbedObject);
                }
                animator.SetBool("Holding", false);
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;

            Debug.Log("C Put Down");
            }
            else if(hitInfo.collider == null)
            {
                Debug.Log("Collider Null");
            }
            else
            {
                Debug.Log(hitInfo.collider.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            isGrounded = true;
        }

        if(collision.gameObject.GetComponent<KineticCrystal>())
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            isGrounded = false;
        }

        if(collision.gameObject.GetComponent<KineticCrystal>())
        {
            transform.SetParent(null);
        }
    }

    void playerFlip()
    {
        flip = !flip;
        transform.Rotate(0f, 180f, 0f);
    }


}
