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
    public int[] skinPrice;
    private int skinsCount;
    private int currentSkin;
    public GameObject[] linesGO;
    private int[] lines;
    public int[] linePrice;
    public GameObject[] linesPrefabs;
    private int linesCount;
    private int currentLine;


    private GameObject linePrefab;
    private Line activeLine;

    private AdController adCont;

    private int diamondQtd;
    private Text diamondQtdTxt;

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
        linesCount = linesGO.Length;
        lines = new int[linesCount];

        PlayerPrefs.SetInt("Skin0", 1);
        PlayerPrefs.SetInt("Line0", 1);

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
        diamondQtdTxt = GameObject.Find("DiamondQtd").GetComponent<Text>();
        adCont = GameObject.Find("AdControl").GetComponent<AdController>();
    }
    private void GetPlayerPrefs()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
        currentLine = PlayerPrefs.GetInt("CurrentLine", 0);
        for (int i = 0; i < skinsCount; i++)
        {
            skins[i] = PlayerPrefs.GetInt("Skin" + i, 0);
            if (skins[i] == 0)
            {
                skinsGO[i].transform.Find("Price").GetComponent<Text>().text = skinPrice[i].ToString();
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
        currentLine = PlayerPrefs.GetInt("CurrentLine", 0);
        for (int i = 0; i < linesCount; i++)
        {
            lines[i] = PlayerPrefs.GetInt("Line" + i, 0);
            if (lines[i] == 0)
            {
                linesGO[i].transform.Find("Price").GetComponent<Text>().text = linePrice[i].ToString();
            }
            else
            {
                if (currentLine == i)
                {
                    linesGO[i].transform.Find("Price").GetComponent<Text>().text = "Selected";
                }
                else
                {
                    linesGO[i].transform.Find("Price").GetComponent<Text>().text = "Select";
                }

                linesGO[i].transform.Find("Price").transform.Find("Image").GetComponent<Image>().enabled = false;
            }
        }
        diamondQtd = PlayerPrefs.GetInt("DiamondQtd", 0);
    }
    private void Inicialization()
    {
        totalCoinsTxt.text = totalCoins.ToString();
        diamondQtdTxt.text = diamondQtd.ToString();
        linePrefab = linesPrefabs[currentLine];

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
            if (totalCoins >= skinPrice[skinNum])
            {
                totalCoins -= skinPrice[skinNum];
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
    public void BuySelectLine(int lineNum)
    {
        if (lines[lineNum] == 0) //se nao tiver a Line
        {
            if (totalCoins >= linePrice[lineNum])
            {
                totalCoins -= linePrice[lineNum];
                totalCoinsTxt.text = totalCoins.ToString();
                PlayerPrefs.SetInt("TotalCoins", totalCoins);
                lines[lineNum] = 1;
                PlayerPrefs.SetInt("Line" + lineNum, 1);
                linesGO[lineNum].transform.Find("Price").transform.Find("Image").GetComponent<Image>().enabled = false;
                linesGO[currentLine].transform.Find("Price").GetComponent<Text>().text = "Select";
                currentLine = lineNum;
                linesGO[currentLine].transform.Find("Price").GetComponent<Text>().text = "Selected";
                linePrefab = linesPrefabs[currentLine];
                PlayerPrefs.SetInt("CurrentLine", currentLine);
                PlayerPrefs.Save();
            }
        }
        else
        {
            linesGO[currentLine].transform.Find("Price").GetComponent<Text>().text = "Select";
            currentLine = lineNum;
            linesGO[currentLine].transform.Find("Price").GetComponent<Text>().text = "Selected";
            linePrefab = linesPrefabs[currentLine];

            PlayerPrefs.SetInt("CurrentLine", currentLine);
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
        diamondQtd++;
        diamondQtdTxt.text = diamondQtd.ToString();
        PlayerPrefs.SetInt("DiamondQtd", diamondQtd);
    }
}
