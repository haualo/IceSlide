using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    public Transform myPlayerTransformObj;
    private Rigidbody2D myRigidBody;
    public Animator animator;
    public AudioSource lose;
    public AudioSource jumpSound;

    public Joystick joystick;
    float moveHorizontal = 0f;

    
    private float moveSpeed = 0f;

    [SerializeField]
    private float speedHoriz;

    private bool facingRight;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;
    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    private bool isButtonPressed = false;

    public int maxHealth = 100;
    public static int currentHelath;
    public HealthBarController healthBar;

    private bool outRight = false;
    private bool outLeft = false;

    public Gradient gradient;
    SpriteRenderer myRender;
    // Start is called before the first frame update
    void Start()
    {
        currentHelath = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myRender = GetComponent<SpriteRenderer>();
        myRender.material.color = gradient.Evaluate(1f);
    }

    public void TakeDamage(int damage)
    {
        //currentHelath -= damage;
        currentHelath = currentHelath - damage;
        healthBar.setHealth(currentHelath);
        
    }

    public void buttonPressed(bool change)
    {
        isButtonPressed = change;
        animator.SetBool("isJumping", true);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();

        //change color
        if(currentHelath == maxHealth)
        {
            myRender.material.color = gradient.Evaluate(1f);
        }
        else if(currentHelath < 67 && currentHelath > 34) 
        {
            myRender.material.color = gradient.Evaluate(0.5f);
        }
        else if( currentHelath < 34 && currentHelath > 1)
        {
            myRender.material.color = gradient.Evaluate(0.3f);
        }
        else if( currentHelath <= 1)
        {

            myRender.material.color = gradient.Evaluate(0);
        }


        //gameOver
        if (currentHelath <= 0)
        {
            DieMenu.GameIsOver = true;
            //hit.Stop();
        }


        //takeDamage
        if (enemyMove.isHit || enemymove2.isHitEnemySlow)
        {
            TakeDamage(10);

            enemymove2.isHitEnemySlow = false;
            enemyMove.isHit = false;
        }


        //move left or right
        if (joystick.Horizontal >= 0.2f)
        {
            moveHorizontal++;
            
        }
        else if (joystick.Horizontal <= -0.2f)
        {
            moveHorizontal--;

        }
        else   //stop moving if joystick in middle
        { 
            moveHorizontal = 0;
            /*
            if (moveHorizontal > 0)
            {
                moveHorizontal--;
            }else if(moveHorizontal < 0)
            {
                moveHorizontal++;
            }
            else
            {
                moveHorizontal = 0;
            }*/
        }

        //change to other side of screen


        if (transform.position.x > 10.5f)
        {
                outRight = true;
                myRigidBody.AddForce(new Vector2(200f, 0));
        }
        else if (transform.position.x < -10.5f)
        {
               outLeft = true;
               myRigidBody.AddForce(new Vector2(-200f, 0));
        }

       
        if( outRight || outLeft)
        {
            outLeft = false;
            outRight = false;
            transform.position = new Vector2(transform.position.x * -1, transform.position.y);


        }


        if (isButtonPressed)
        {
            jump = true;
            isButtonPressed = false;

        }
        else
        {
            jump = false;
            animator.SetBool("isJumping", false);
        }

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));


        Flip(moveHorizontal);
    }




    private void FixedUpdate()
    {
        HandelMovment(moveHorizontal);
        if(transform.position.y < -5)
        {
            DieMenu.GameIsOver = true;
            lose.Play();
        }
    }

    private void HandelMovment(float horizMove)
    {
        myRigidBody.velocity = new Vector2(horizMove * speedHoriz, myRigidBody.velocity.y);

        if (isGrounded && jump)
        {
            jumpSound.Play();
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void Flip(float horiz)
    {

        if (horiz > 0 && !facingRight || horiz < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    }

    private bool IsGrounded()
    {
        if(myRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i =0; i< colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


}
