using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinController : pointController
{

    public Transform coin;
    public Rigidbody2D coinRB;

    public AudioSource coin1;
    public AudioSource coin2;

    [SerializeField]
    private Transform[] hitPoints;

    [SerializeField]
    private float hitRadius;

    [SerializeField]
    private LayerMask whatIsTarget;

    [SerializeField]
    private GameObject coinS;


    private bool isHit;

    int myVal = 0;
    //score

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(coin.transform.position.y <= -1.5f)
        {
            coinRB.gravityScale = 0;
            coinRB.velocity = new Vector2(0, 0);
        }
        else
        {
            coinRB.gravityScale = 1;
        }


        if (isHit)
        {

                switch (myVal)
            {
                case 0: coin1.Play(); coin2.Stop(); myVal = 1; break;
                case 1: coin2.Play(); coin1.Stop(); myVal = 0; break;

            }
        }
    }


    private void FixedUpdate()
    {


        isHit = IsHit();
        if (isHit)
        {
            scorePoint += 1;
            coin.transform.position = new Vector3(Random.Range(-8f, 8f), 6f, 0f);


        }
        
        if(transform.position.y < -5)
        {
            coin.transform.position = new Vector3(Random.Range(-8f, 8f), 6f, 0f);
        }
    }



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

    private void SpawnCoins()
    {
        bool coinSpawned = false;

        while (!coinSpawned)
        {
            Vector3 coinPosition = new Vector3(Random.Range(-8f, 8f), 6f, 0f);

            if((coinPosition - transform.position).magnitude < 3)
            {
                continue;
            }
            else
            {
                Instantiate(coinS, coinPosition, Quaternion.identity);
                coinSpawned = true;
            }
        }

    }


}
