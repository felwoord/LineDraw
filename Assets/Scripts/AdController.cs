using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using System;

public class AdController : MonoBehaviour
{
    private float adTimer;

    private string iosGameIDUnityAds = "2656056";
    private string androidGameIDUnityAds = "2656055";
    private string gameID = null;
    //private string androidGameIDAdmob = "ca-app-pub-8754874070275415~2313930081"; //real
    //private string iosGameIDAdmob = "ca-app-pub-8754874070275415~8698950004";     //real
    private string androidGameIDAdmob = "ca-app-pub-3940256099942544~3347511713";   //teste
    private string iosGameIDAdmob = "ca-app-pub-3940256099942544~1458002511";       //teste
    private string appID = null;
    private int removeAds;
    private int type;

    public BannerView bannerView;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID
        gameID = androidGameIDUnityAds;
        appID = androidGameIDAdmob;
#elif UNITY_IOS
        gameID = iosGameIDUnityAds;
        appID = iosGameIDAdmob;
#endif

        Advertisement.Initialize(gameID);
        MobileAds.Initialize(appID);
    }

    void Start()
    {
        adTimer = 0;
        removeAds = PlayerPrefs.GetInt("RemoveAds", 0);
    }

    void Update()
    {
        if (removeAds == 0)
        {
            adTimer += Time.deltaTime;
        }
    }
    public void RemoveAdsBought()
    {
        removeAds = 1;
        PlayerPrefs.SetInt("RemoveAds", removeAds);
    }
    public void RequestBanner()
    {
        if (removeAds == 0)
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
             string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
             string adUnitId = "unexpected_platform";
#endif

            // Create a banner at the bottom of the screen.
            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            bannerView.LoadAd(request);

            // Show Banner
            bannerView.Show();
        }
    }
    public void ShowInterstitial()
    {
        if (adTimer > 120)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
                adTimer = 0;
            }
            else
            {
#if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

                // Initialize an InterstitialAd.
                InterstitialAd interstitial = new InterstitialAd(adUnitId);
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the interstitial with the request.
                interstitial.LoadAd(request);
                // If successfully Loaded, show Interstitial
                if (interstitial.IsLoaded())
                {
                    interstitial.Show();
                }
            }
        }
    }
    public void ShowRewardedVideo(int aux)
    {
        type = aux;

        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("rewardedVideo", options);
    }

    void HandleShowResult(ShowResult result)
    {

        if (result == ShowResult.Finished)
        {
            RewardType();
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }

    public void RewardType()
    {
        if (type == 0)
        {
            GameObject.Find("Main Camera").GetComponent<MenuController>().AdCompleted();
        }
        else if (type == 1)
        {
            GameObject.Find("Main Camera").GetComponent<GameController>().AdCompleted();
        }
    }
}
