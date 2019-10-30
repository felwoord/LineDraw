using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdController : MonoBehaviour
{
    private float adTimer;


    private string iosGameIDUnityAds = "2656056";       
    private string androidGameIDUnityAds = "2656055";   
    private string gameID = null;
    private string appID = null;
    private int removeAds;
    private int type;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID
        gameID = androidGameIDUnityAds;
#elif UNITY_IOS
        gameID = iosGameIDUnityAds;
#endif

        Advertisement.Initialize(gameID, false);
    }

    void Start()
    {
        ResetTimer();
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
    public void ShowInterstitial()
    {
        if (adTimer > 180)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
                ResetTimer();
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

    public void ResetTimer()
    {
        adTimer = 0;
    }

    public bool CheckAvlbRewarded()
    {
        return Advertisement.IsReady();
    }
}
