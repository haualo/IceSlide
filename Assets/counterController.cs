using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class counterController : MonoBehaviour
{

    public GameObject controlUI;
    public GameObject counterUI;
    public Text counterLabel;

    private int counter = 3;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        counterLabel.text = counter.ToString();



    }
    private void FixedUpdate()
    {



    }
}
