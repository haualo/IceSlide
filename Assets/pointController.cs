using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointController : MonoBehaviour
{
    public static int scorePoint;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (DieMenu.ContinueIsPressed)
        {
            scorePoint = PlayerPrefs.GetInt("SCOREIN");
            DieMenu.ContinueIsPressed = false;
        }
        else
        {
            scorePoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + scorePoint.ToString();
        PlayerPrefs.SetInt("SCOREIN", scorePoint);
    }
}
