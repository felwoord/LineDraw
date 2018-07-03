﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : MonoBehaviour
{
    private float adTimer;

    private string iosGameID = "2656056";
    private string androidGameID = "2656055";
    private string gameID = null;

    private int type;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID
        gameID = androidGameID;
#elif UNITY_IOS
        gameID = iosGameID;
#endif

        Advertisement.Initialize(gameID);

    }

    void Start()
    {
        adTimer = 0;
    }

    void Update()
    {
        adTimer += Time.deltaTime;
    }
    public void ShowInterstitial()
    {
        if (adTimer > 180)
        {
            Advertisement.Show();
            adTimer = 0;
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
