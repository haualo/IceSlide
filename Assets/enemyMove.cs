using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyMove : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public Animator animator;
    public AudioSource hit;

    [SerializeField]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField]
    private Transform[] hitPoints;

    [SerializeField]
    private float hitRadius;

    [SerializeField]
    private LayerMask whatIsTarget;

    public static bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemy.transform.position = new Vector3(Random.Range(0.5f, 8f), 6f, 0f);
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90f;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        //isHit = IsHit();
        
        if (isHit)
        {
            //DieMenu.GameIsOver = true;
            hit.Play();
            movement = new Vector2(0, 0);
            if(playerController.currentHelath > 0)
            {
                enemy.transform.position = new Vector3(Random.Range(-8f, 8f), 6f, 0f);
            }

        }

        if(playerController.currentHelath <= 0)
        {
            hit.Stop();
        }

        moveCharacter(movement);

    }



    private void FixedUpdate()
    {
        //isHit = IsHit();
        moveCharacter(movement);
        if (isHit)
        {

        }


    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isHit = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            isHit = true;
        }
    }

    /*
    private bool IsHit()
    {
        foreach (Transform point in hitPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, hitRadius, whatIsTarget);

            for (int i = 0; i < colliders.Length; i++)
            {

                if (colliders[i].gameObject != gameObject)
                {
                    return true;
                }

            }
        }
        return false;
    }
    */
}
