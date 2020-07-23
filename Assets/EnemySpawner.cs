using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemySpawner;

    private int myTimer = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myTimer++;

    }

    private void FixedUpdate()
    {
        if (myTimer > 500)
        {
            EnemySpawning();
            myTimer = 0;
        }
    }

    private void EnemySpawning()
    {
        bool enemySpawn = false;

        while (!enemySpawn)
        {
            Vector3 enemyPos = new Vector3(Random.Range(-8f, 8f), 10f, 0f);

            if ((enemyPos - transform.position).magnitude < 3)
            {
                continue;
            }
            else
            {
                Instantiate(enemySpawner, enemyPos, Quaternion.identity);
                enemySpawn = true;
            }
        }

    }
}
