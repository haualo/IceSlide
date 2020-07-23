using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject aboutMenuUI;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AboutButton()
    {
        mainMenuUI.SetActive(false);
        aboutMenuUI.SetActive(true);
        
    }
    public void BackButtonInAboutMenu()
    {
        mainMenuUI.SetActive(true);
        aboutMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
