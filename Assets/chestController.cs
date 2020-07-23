using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestController : pointController
{
    public Transform chest;
    public Animator animator;
    public AudioSource chestSound;

    private Rigidbody2D chestRigidBody;

    [SerializeField]
    GameObject chestObj;

    private bool playerHit = false;
    private bool chestAnimeEnd = false;
    private bool getScore = false;
    private int myTimer = 0;


    void Start()
    {
        chestRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myTimer++;


        if (transform.position.y < -5)
        {
            chest.transform.position = new Vector3(Random.Range(-8f, 8f), 10f, 0f);
        }

        if (transform.position.y > 2f)
        {
            animator.SetBool("Open", false);
        }

        if (chestAnimeEnd)
        {
            chest.transform.position = new Vector3(Random.Range(-8f, 8f), 10f, 0f);
            chestRigidBody.velocity = new Vector2(0,0);
            chestAnimeEnd = false;
            getScore = true;
            chestRigidBody.gravityScale = 0;
        }

        if (getScore)
        {
            scorePoint += 10;
            getScore = false;
            chestSound.Play();
        }

        if (playerHit)
        {
            animator.SetBool("Open", true);
            playerHit = false;


        }


    }


    
    private void FixedUpdate()
    {
        if (!playerHit)
        {
            if (myTimer > 1500)
            {
                chestRigidBody.gravityScale = 1;
                myTimer = 0;
            }
        }

    }

    public void AlertObservers(string message)
    {
        if (message.Equals("ChestAnimationEnded"))
        {
            chestAnimeEnd = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerHit = true;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            playerHit = true;

        }
    }
}
