using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public GameObject mainCanvas, shopCanvas, settingsCanvas;
    private Text totalCoinsTxt;
    private int totalCoins;

    public GameObject[] skinsGO;
    private int[] skins;
    private int[] price;
    private int skinsCount;
    private int currentSkin;

    public GameObject linePrefab;
    private Line activeLine;

    private AdController adCont;

    private void Awake()
    {
      if(GameObject.FindGameObjectsWithTag("AdCont").Length == 0)
        {
            GameObject adContGO = Instantiate(Resources.Load("AdControl") as GameObject);
            adContGO.name = "AdControl";
        }
    }
    void Start() {
        skinsCount = skinsGO.Length;
        skins = new int[skinsCount];
        price = new int[skinsCount];

        price[0] = 100;
        price[1] = 200;
        price[2] = 300;
        price[3] = 999999999;
        price[4] = 999999999;

        PlayerPrefs.SetInt("Skin0", 1);

        GameObjectFind();
        GetPlayerPrefs();
        Inicialization();
    }

    void Update() {
        LineDraw();
    }

    private void GameObjectFind()
    {
        totalCoinsTxt = GameObject.Find("TotalCoins").GetComponent<Text>();
    }
    private void GetPlayerPrefs()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
        for (int i = 0; i < skinsCount; i++)
        {
            skins[i] = PlayerPrefs.GetInt("Skin" + i, 0);
            if (skins[i] == 0)
            {
                skinsGO[i].transform.Find("Price").GetComponent<Text>().text = price[i].ToString();
            }
            else
            {
                if(currentSkin == i)
                {
                    skinsGO[i].transform.Find("Price").GetComponent<Text>().text = "Selected";
                }
                else
                {
                    skinsGO[i].transform.Find("Price").GetComponent<Text>().text = "Select";
                }

                skinsGO[i].transform.Find("Price").transform.Find("Image").GetComponent<Image>().enabled = false;
            }
        }
    }
    private void Inicialization()
    {
        totalCoinsTxt.text = totalCoins.ToString();
    }
    private void LineDraw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject lineGO = Instantiate(linePrefab);
            activeLine = lineGO.GetComponent<Line>();

        }
        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }
        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
    }
    public void ChangeCanvas(int aux)
    {
        switch (aux)
        {
            case 1:                            //main -> shop
                mainCanvas.SetActive(false);
                shopCanvas.SetActive(true);
                break;
            case 2:                            //main -> settings
                mainCanvas.SetActive(false);
                settingsCanvas.SetActive(true);
                break;
            case 3:                            //shop -> main
                shopCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                break;
            case 4:                            //settings -> main
                settingsCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void BuySelectSkin(int skinNum)
    {
        if (skins[skinNum] == 0) //se nao tiver a skin
        {
            if (totalCoins >= price[skinNum])
            {
                totalCoins -= price[skinNum];
                totalCoinsTxt.text = totalCoins.ToString();
                PlayerPrefs.SetInt("TotalCoins", totalCoins);
                skins[skinNum] = 1;
                PlayerPrefs.SetInt("Skin" + skinNum, 1);
                skinsGO[skinNum].transform.Find("Price").transform.Find("Image").GetComponent<Image>().enabled = false;
                skinsGO[currentSkin].transform.Find("Price").GetComponent<Text>().text = "Select";
                currentSkin = skinNum;
                skinsGO[currentSkin].transform.Find("Price").GetComponent<Text>().text = "Selected";

                PlayerPrefs.SetInt("CurrentSkin", currentSkin);
                PlayerPrefs.Save();
            }
        }
        else
        {
            skinsGO[currentSkin].transform.Find("Price").GetComponent<Text>().text = "Select";
            currentSkin = skinNum;
            skinsGO[currentSkin].transform.Find("Price").GetComponent<Text>().text = "Selected";

            PlayerPrefs.SetInt("CurrentSkin", currentSkin);
            PlayerPrefs.Save();
        }
    }
    public void CleanScreen()
    {
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Line"))
        {
            Destroy(gameObj);
        }
    }
    public void WatchAd()
    {
        adCont.ShowRewardedVideo(0);
    }
    public void AdCompleted()
    {
        //Adicionar Reward pelo Ad
    }
}
