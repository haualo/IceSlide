using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player;
    public Transform Enemy;
    public Animator animator;

    [SerializeField]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField]
    private Transform[] hitPoints;

    [SerializeField]
    private Transform reSpawnPoint;


    [SerializeField]
    private float hitRadius;

    [SerializeField]
    private LayerMask whatIsTarget;

    private bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        isHit = IsHit();

        if (isHit)
        {
            movement = new Vector2(0, 0);

            animator.SetBool("isHit", true);

            for (int i =0; i< 10000; i++) { Debug.Log("i" + " = " + i); }
            Enemy.transform.position = new Vector3(Random.Range(-8f, 8f), 6f, 0f);

            Debug.Log(" Enemy.transform.position" + "  " + Enemy.transform.position);

        }
        else
        {
            animator.SetBool("isHit", false);
        }
        

    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private bool IsHit()
    {
        foreach(Transform point in hitPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, hitRadius, whatIsTarget);

            for(int i= 0; i < colliders.Length; i++)
            {

                if(colliders[i].gameObject != gameObject)
                {
                    return true;
                }

            }
        }
        return false;
    }
}
