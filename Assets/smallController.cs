using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallController : MonoBehaviour
{
    public Transform body;
    public Animator animator;
    public AudioSource bodySound;

    private Rigidbody2D bodyRigidBody;

    [SerializeField]
    GameObject bodyObj;

    public static bool hitSmallBody = false;

    void Start()
    {
        bodyRigidBody = GetComponent<Rigidbody2D>();
    }


    public void AlertObservers(string message)
    {

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("HITHIT!!");
            hitSmallBody = true;
        }
    }
}
