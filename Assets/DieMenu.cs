using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;
using System;


public class DieMenu : MonoBehaviour
{
    public static bool GameIsOver = false;
    public static bool ContinueIsPressed = false;
    public AudioSource music;

    public GameObject gameOverUI;
    public GameObject controlUI;
    public Text highScoreText;
    public Text currentScoreText;

    private int currentScore;
    private int highScore;
    private bool adRewardClosed = false;
    
    //reward ad
    private RewardedAd rewardedAd;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        music.Play();
        this.rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");


        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }

    private void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        ContinueIsPressed = true;
        Time.timeScale = 1f;
        GameIsOver = false;
        coinController.scorePoint = currentScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        controlUI.SetActive(true);
    }

    void Update()
    {
        currentScore = coinController.scorePoint;



        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScore = currentScore;
        }
        currentScoreText.text = currentScore.ToString();
        highScoreText.text = "Best: " + highScore.ToString();

        if (GameIsOver)
        {
            GameOver();
        }
    }



    void GameOver()
    {
        gameOverUI.SetActive(true);
        controlUI.SetActive(false);
        Time.timeScale = 0f;
        music.Stop();
    }

    public void MainMenu()
    {

        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        controlUI.SetActive(true);
        GameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void ContinueGame()
    {
        UserChoseToWatchAd();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
