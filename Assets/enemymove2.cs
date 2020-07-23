using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove2 : MonoBehaviour
{
    public Transform player;
    public Transform enemySlow;
    public Animator animator;
    public AudioSource hit;

    [SerializeField]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;


    public static bool isHitEnemySlow;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemySlow.transform.position = new Vector3(Random.Range(-8f, -0.5f), 6f, 0f);
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;


        if (isHitEnemySlow)
        {
            hit.Play();
            movement = new Vector2(0, 0);
            if (playerController.currentHelath > 0)
            {
                enemySlow.transform.position = new Vector3(Random.Range(-8f, 8f), 6f, 0f);
            }

        }

        if (playerController.currentHelath <= 0)
        {
            hit.Stop();
        }

        moveCharacter(movement);

    }



    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isHitEnemySlow = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isHitEnemySlow = true;
        }
    }
}
