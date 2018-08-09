using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public GameObject mainCanvas, shopCanvas, settingsCanvas, cashShopCanvas, helpCanvas, achvCanvas, coinDisplayCanvas;
    private bool draw;
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
    private GameObject removeAdsGO;
    private int removeAds;
    public GameObject receivedUI;
    public Text receivedQtd;

    public GameObject restorePurchaseButton, deleteGameConf;

    private int currentHint;
    public GameObject[] hint;
    private int hintseen;
    public GameObject[] hintNot = new GameObject[2];

    public GameObject skinScroll, lineScroll;

    private AudioSource effectAS;
    public Slider effectSlider;
    private float effectVolume;
    public AudioClip buttonPressedAudio, storeScrolledAudio, drawAudio;
    private bool drawAudioCD;
    private float drawAudioCounterCD, storeSkinScrollCounter, lastSkinScrollCounter, storeLineScrollCounter, lastLineScrollCounter;
    private float drawCounter;
    public Scrollbar skinScrollBar, lineScrollBar;


    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("AdCont").Length == 0)
        {
            GameObject adContGO = Instantiate(Resources.Load("AdControl") as GameObject);
            adContGO.name = "AdControl";
        }
    }
    void Start()
    {
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
    void Update()
    {
        if (draw)
        {
            LineDraw();
        }

        if (drawAudioCD)
            drawAudioCounterCD += Time.deltaTime;

        if (drawAudioCounterCD >= 0.5f)
        {
            drawAudioCD = false;
            drawAudioCounterCD = 0;
        }
    }
    private void GameObjectFind()
    {
        totalCoinsTxt = GameObject.Find("TotalCoins").GetComponent<Text>();
        diamondQtdTxt = GameObject.Find("DiamondQtd").GetComponent<Text>();
        adCont = GameObject.Find("AdControl").GetComponent<AdController>();
        removeAdsGO = GameObject.Find("RemoveAds");
        effectAS = GameObject.Find("EffectSource").GetComponent<AudioSource>();
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
                if (currentSkin == i)
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
        removeAds = PlayerPrefs.GetInt("RemoveAds", 0);
        effectVolume = PlayerPrefs.GetFloat("EffectVolume", 1);
        hintseen = PlayerPrefs.GetInt("HintSeen", 0);

    }
    private void Inicialization()
    {
        totalCoinsTxt.text = totalCoins.ToString();
        diamondQtdTxt.text = diamondQtd.ToString();
        linePrefab = linesPrefabs[currentLine];
        storeSkinScrollCounter = 0;
        lastSkinScrollCounter = 0;
        storeLineScrollCounter = 0;
        lastLineScrollCounter = 0;
        drawAudioCounterCD = 0;
        drawAudioCD = false;
        drawCounter = 0;
        if (removeAds == 1)
        {
            removeAdsGO.SetActive(false);
        }
        draw = true;
        currentHint = 0;
        adCont.RequestBanner();
        if (hintseen == 1)
        {
            hintNot[0].SetActive(false);
            hintNot[1].SetActive(false);
        }
#if UNITY_IOS
        restorePurchaseButton.SetActive(true);
#else
        restorePurchaseButton.SetActive(false);
#endif
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
            drawCounter = 0;
        }
        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);

            drawCounter += Time.deltaTime;
            if (!drawAudioCD && drawCounter > 0.075f)
            {
                PlayAudio(2);
            }
        }
    }
    public void ChangeCanvas(int aux)
    {
        switch (aux)
        {
            case 1:                            //main -> shop
                mainCanvas.SetActive(false);
                shopCanvas.SetActive(true);
                draw = false;
                break;
            case 2:                            //main -> settings
                mainCanvas.SetActive(false);
                settingsCanvas.SetActive(true);
                draw = false;
                break;
            case 3:                            //shop -> main
                shopCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                draw = true;
                break;
            case 4:                            //settings -> main
                settingsCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                draw = true;
                break;
            case 5:                            //open cash shop
                cashShopCanvas.SetActive(true);
                draw = false;
                break;
            case 6:                            //close cash shop
                cashShopCanvas.SetActive(false);
                if (mainCanvas.activeSelf)
                {
                    draw = true;
                }
                break;
            case 7:                            //settings -> help
                settingsCanvas.SetActive(false);
                helpCanvas.SetActive(true);
                if (hintseen == 0)
                {
                    hintseen = 1;
                    hintNot[0].SetActive(false);
                    hintNot[1].SetActive(false);
                    PlayerPrefs.SetInt("HintSeen", 1);
                }
                break;
            case 8:                            //help -> settings
                helpCanvas.SetActive(false);
                settingsCanvas.SetActive(true);
                break;
            case 9:                            //main -> achievements
                mainCanvas.SetActive(false);
                achvCanvas.SetActive(true);
                gameObject.GetComponent<AchiviementsController>().Start();
                draw = false;
                break;
            case 10:                           //achievements -> main
                achvCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                draw = true;
                break;
            default:
                break;
        }
    }
    public void PlayButton()
    {
        adCont.bannerView.Hide();
        SceneManager.LoadScene("GameScene");
    }
    public void BuySelectSkin(int skinNum)
    {
        if (skins[skinNum] == 0) //se nao tiver a skin
        {
            if (totalCoins >= skinPrice[skinNum])
            {
                totalCoins -= skinPrice[skinNum];
                int coinsUsed = PlayerPrefs.GetInt("CoinsUsed", 0);
                PlayerPrefs.SetInt("CoinsUsed", coinsUsed + skinPrice[skinNum]);
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
                int coinsUsed = PlayerPrefs.GetInt("CoinsUsed", 0);
                PlayerPrefs.SetInt("CoinsUsed", coinsUsed + linePrice[lineNum]);
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
        diamondQtd += 2;
        diamondQtdTxt.text = diamondQtd.ToString();
        PlayerPrefs.SetInt("DiamondQtd", diamondQtd);
        OpenReceivedUI(2);
    }
    public void BuyDiamond(int qtd)
    {
        diamondQtd += qtd;
        diamondQtdTxt.text = diamondQtd.ToString();
        PlayerPrefs.SetInt("DiamondQtd", diamondQtd);
        OpenReceivedUI(qtd);
    }
    public void BuyRemoveAds()
    {
        adCont.RemoveAdsBought();
        SceneManager.LoadScene("MenuScene");
    }
    public void VolumeControl()
    {
        effectVolume = effectSlider.value;
        effectAS.volume = effectVolume;
        PlayerPrefs.SetFloat("EffectVolume", effectVolume);
    }
    public void DeleteGameData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("MenuScene");
    }
    public void OpenCloseDeleteGameConf(int aux)
    {
        if (aux == 0)
        {
            deleteGameConf.SetActive(true);
        }
        if (aux == 1)
        {
            deleteGameConf.SetActive(false);
        }
        if (aux == 2)
        {
            deleteGameConf.SetActive(false);
            DeleteGameData();
        }
    }
    public void ChangeHint(int aux)
    {
        hint[currentHint].SetActive(false);
        currentHint += aux;
        if (currentHint < 0)
            currentHint = 10;

        if (currentHint > 10)
            currentHint = 0;

        hint[currentHint].SetActive(true);
    }
    public void ChangeScrollView(int aux)
    {
        switch (aux)
        {
            case 0:
                skinScroll.SetActive(true);
                lineScroll.SetActive(false);
                break;
            case 1:
                skinScroll.SetActive(false);
                lineScroll.SetActive(true);
                break;
            default:
                break;
        }
        
    }
    public void OpenReceivedUI(int qtd)
    {
        receivedUI.SetActive(true);
        receivedQtd.text = qtd.ToString();
        coinDisplayCanvas.GetComponent<Canvas>().sortingOrder = 5;
    }
    public void CloseReceivedUI()
    {
        receivedUI.SetActive(false);
        coinDisplayCanvas.GetComponent<Canvas>().sortingOrder = 0;
    }
    public void PlayAudio(int aux)
    {
        switch (aux)
        {
            case 0:
                effectAS.clip = buttonPressedAudio;
                effectAS.Play();
                break;
            case 1:
                effectAS.clip = storeScrolledAudio;
                effectAS.Play();
                break;
            case 2:
                effectAS.clip = drawAudio;
                effectAS.Play();
                drawAudioCD = true;
                break;
            default:
                break;
        }
    }
    public void StoreScroll (float aux)
    {
        if (aux == 0)
        {
            storeSkinScrollCounter += Mathf.Abs(lastSkinScrollCounter - skinScrollBar.value);
            lastSkinScrollCounter = skinScrollBar.value;
            if (storeSkinScrollCounter > 0.15f)
            {
                PlayAudio(1);
                storeSkinScrollCounter = 0;
            }
        }
        if (aux == 1)
        {
            storeLineScrollCounter += Mathf.Abs(lastLineScrollCounter - lineScrollBar.value);
            lastLineScrollCounter = lineScrollBar.value;
            if (storeLineScrollCounter > 0.15f)
            {
                PlayAudio(1);
                storeLineScrollCounter = 0;
            }
        }
    }
}
