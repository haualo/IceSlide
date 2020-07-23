using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public Transform enemySlow;
    
    public AudioSource hit;

    [SerializeField]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    //public static bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        /*
                 if (isHit)
                {
                    movement = new Vector2(0, 0);
                }
                 */


    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
