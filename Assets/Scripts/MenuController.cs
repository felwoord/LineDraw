using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public GameObject mainCanvas, shopCanvas, cashShopCanvas, helpCanvas, achvCanvas, coinDisplayCanvas;
    private float settingsBarPosX;
    private RectTransform settingsBar;
    private bool openSettings;
    private bool draw;
    private Text totalCoinsTxt;
    private int totalCoins;

    public GameObject wDrawing;
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
    public GameObject receivedSkinUI;

    public GameObject restorePurchaseButton/*,deleteGameConf*/;

    private int currentHint;
    public GameObject[] hint;
    private int hintseen;
    public GameObject[] hintNot = new GameObject[2];

    public GameObject skinScroll, lineScroll;

    private AudioSource effectAS;
    public Image volumeImg;
    public Sprite volumeOn, volumeOff;
    public Slider effectSlider;
    private float effectVolume;
    public AudioClip buttonPressedAudio, storeScrolledAudio, drawAudio;
    private bool drawAudioCD;
    private float drawAudioCounterCD, storeSkinScrollCounter, lastSkinScrollCounter, storeLineScrollCounter, lastLineScrollCounter;
    private float drawCounter;
    public Scrollbar skinScrollBar, lineScrollBar;

    private ParticleSystem buyEffect;

    public Transform skinContent;
    public ScrollRect skinScrollRect;
    private float delayCenterSkin;                  //Counter to see how long since the ScrollRect stopped 
    private bool enableDelayCenterSkin;             //Bool to start delayCenterSkin to increment 
    private int totalSkinQtt;                       //Total ball skins amount
    private float shorterDist;                      //Shorter distance between current ScrollRect Pos and one of Centered Pos
    private bool enableSkinCentering;               //Enable Ball Skin Centering
    int shorterDistIndex;                           //Index of the shorter distance
    private bool doOnceCalculateSkinPos;            //Call CalculatePositionSkinScrollRect once
    private float centeredPos;                      //Centered postion of the contents

    public Transform lineContent;
    public ScrollRect lineScrollRect;
    private float delayCenterLine;
    private bool enableDelayCenterLine;
    private int totalLineQtt;
    private float shorterDistLine;
    private bool enableLineCentering;
    int shorterDistIndexLine;
    private bool doOnceCalculateLinePos;
    private float centeredPosLine;

    public Button watchADButton;
    public RectTransform settingsBarRectTrans, pivotSettingsRectTrans;



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
        OpenCloseSettingsBar();

        if (enableDelayCenterSkin && Input.touchCount == 0)
        {
            delayCenterSkin += Time.deltaTime;
            if (delayCenterSkin > 0.025f && doOnceCalculateSkinPos)
            {
                doOnceCalculateSkinPos = false;
                CalculatePositionSkinScrollRect();
                delayCenterSkin = 0;
            }
            if (enableSkinCentering)
            {
                SkinCentering();
            }
        }

        if (enableDelayCenterLine && Input.touchCount == 0)
        {
            delayCenterLine += Time.deltaTime;
            if (delayCenterLine > 0.025f && doOnceCalculateLinePos)
            {
                doOnceCalculateLinePos = false;
                CalculatePositionLineScrollRect();
                delayCenterLine = 0;
            }
            if (enableLineCentering)
            {
                LineCentering();
            }
        }
    }
    private void GameObjectFind()
    {
        totalCoinsTxt = GameObject.Find("TotalCoins").GetComponent<Text>();
        diamondQtdTxt = GameObject.Find("DiamondQtd").GetComponent<Text>();
        adCont = GameObject.Find("AdControl").GetComponent<AdController>();
        removeAdsGO = GameObject.Find("RemoveAds");
        effectAS = GameObject.Find("EffectSource").GetComponent<AudioSource>();
        settingsBar = GameObject.Find("SettingsBar").GetComponent<RectTransform>();
        buyEffect = GameObject.Find("BuyEffect").GetComponent<ParticleSystem>();
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
        settingsBarPosX = settingsBar.anchoredPosition.x;
        openSettings = false;
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
        settingsBarRectTrans.sizeDelta = new Vector2(80, 280);
        settingsBarRectTrans.anchoredPosition = new Vector2(settingsBarRectTrans.anchoredPosition.x, -60f);
        //pivotSettingsRectTrans.sizeDelta = new Vector2(80, 280);
        pivotSettingsRectTrans.anchoredPosition = new Vector2(pivotSettingsRectTrans.anchoredPosition.x, -60);
        
#endif

        if (effectVolume == 1)
        {
            effectAS.volume = 1;
            volumeImg.sprite = volumeOn;
        }
        else
        {
            effectAS.volume = 0;
            volumeImg.sprite = volumeOff;
        }

        totalSkinQtt = 0;
        foreach (Transform child in skinContent) if (child.CompareTag("BallSkin"))
            {
                totalSkinQtt++;
            }

        totalLineQtt = 0;
        foreach (Transform child in lineContent) if (child.CompareTag("LineSkin"))
            {
                totalLineQtt++;
            }

        CallCheckAd();
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
    private void OpenCloseSettingsBar()
    {
        if (openSettings)
            settingsBar.anchoredPosition = Vector2.Lerp(settingsBar.anchoredPosition, new Vector2(0, settingsBar.anchoredPosition.y), 0.5f);
        else
            settingsBar.anchoredPosition = Vector2.Lerp(settingsBar.anchoredPosition, new Vector2(settingsBarPosX, settingsBar.anchoredPosition.y), 0.5f);

    }
    public void ChangeCanvas(int aux)
    {
        switch (aux)
        {
            case 1:                            //main -> shop
                mainCanvas.SetActive(false);
                shopCanvas.SetActive(true);
                draw = false;
                wDrawing.SetActive(false);
                break;
            case 2:                            //open settings bar
                openSettings = !openSettings;
                break;
            case 3:                            //shop -> main
                shopCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                wDrawing.SetActive(true);
                draw = true;
                break;
            case 4:
                break;
            case 5:                            //open cash shop
                cashShopCanvas.SetActive(true);
                draw = false;
                wDrawing.SetActive(false);
                adCont.bannerView.Hide();
                break;
            case 6:                            //close cash shop
                cashShopCanvas.SetActive(false);
                adCont.bannerView.Show();
                if (mainCanvas.activeSelf)
                {
                    wDrawing.SetActive(true);
                    draw = true;
                }
                break;
            case 7:                            //main -> help
                mainCanvas.SetActive(false);
                helpCanvas.SetActive(true);
                wDrawing.SetActive(false);
                draw = false;
                if (hintseen == 0)
                {
                    hintseen = 1;
                    hintNot[0].SetActive(false);
                    hintNot[1].SetActive(false);
                    PlayerPrefs.SetInt("HintSeen", 1);
                }
                break;
            case 8:                            //help -> main
                helpCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                wDrawing.SetActive(true);
                draw = true;
                break;
            case 9:                            //main -> achievements
                mainCanvas.SetActive(false);
                achvCanvas.SetActive(true);
                wDrawing.SetActive(false);
                gameObject.GetComponent<AchiviementsController>().Start();
                draw = false;
                break;
            case 10:                           //achievements -> main
                achvCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                wDrawing.SetActive(true);
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
            if (skinNum == 16 || skinNum == 19 || skinNum == 20 || skinNum == 21)
            {
                if (diamondQtd >= skinPrice[skinNum])
                {
                    diamondQtd -= skinPrice[skinNum];
                    int diamondsUsed = PlayerPrefs.GetInt("DiamondsUsed", 0);
                    PlayerPrefs.SetInt("DiamondsUsed", diamondsUsed + skinPrice[skinNum]);
                    diamondQtdTxt.text = diamondQtd.ToString();
                    PlayerPrefs.SetInt("DiamondQtd", diamondQtd);
                    skins[skinNum] = 1;
                    PlayerPrefs.SetInt("Skin" + skinNum, 1);
                    skinsGO[skinNum].transform.Find("Price").transform.Find("Image").GetComponent<Image>().enabled = false;
                    skinsGO[currentSkin].transform.Find("Price").GetComponent<Text>().text = "Select";
                    currentSkin = skinNum;
                    skinsGO[currentSkin].transform.Find("Price").GetComponent<Text>().text = "Selected";
                    buyEffect.Play();
                    PlayerPrefs.SetInt("CurrentSkin", currentSkin);
                    PlayerPrefs.Save();
                }
            }
            else
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
                    buyEffect.Play();
                    PlayerPrefs.SetInt("CurrentSkin", currentSkin);
                    PlayerPrefs.Save();
                }
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
            if (lineNum >= 8)
            {
                if (diamondQtd >= linePrice[lineNum])
                {
                    diamondQtd -= linePrice[lineNum];
                    int diamondsUsed = PlayerPrefs.GetInt("DiamondsUsed", 0);
                    PlayerPrefs.SetInt("DiamondsUsed", diamondsUsed + linePrice[lineNum]);
                    diamondQtdTxt.text = diamondQtd.ToString();
                    PlayerPrefs.SetInt("DiamondQtd", diamondQtd);
                    lines[lineNum] = 1;
                    PlayerPrefs.SetInt("Line" + lineNum, 1);
                    linesGO[lineNum].transform.Find("Price").transform.Find("Image").GetComponent<Image>().enabled = false;
                    linesGO[currentLine].transform.Find("Price").GetComponent<Text>().text = "Select";
                    currentLine = lineNum;
                    linesGO[currentLine].transform.Find("Price").GetComponent<Text>().text = "Selected";
                    linePrefab = linesPrefabs[currentLine];
                    buyEffect.Play();
                    PlayerPrefs.SetInt("CurrentLine", currentLine);
                    PlayerPrefs.Save();
                }
            }
            else
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
                    buyEffect.Play();
                    PlayerPrefs.SetInt("CurrentLine", currentLine);
                    PlayerPrefs.Save();
                }
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
        adCont.ResetTimer();
        int adsWatched = PlayerPrefs.GetInt("AdsWatched", 0);
        adsWatched += 1;
        PlayerPrefs.SetInt("AdsWatched", adsWatched);
        diamondQtd += 2;
        diamondQtdTxt.text = diamondQtd.ToString();
        PlayerPrefs.SetInt("DiamondQtd", diamondQtd);
        PlayerPrefs.Save();
        OpenReceivedUI(2, 0);
        CallCheckAd();
    }
    public void BuyDiamond(int qtd)
    {
        diamondQtd += qtd;
        diamondQtdTxt.text = diamondQtd.ToString();
        PlayerPrefs.SetInt("DiamondQtd", diamondQtd);
        OpenReceivedUI(qtd, 0);
    }
    public void BuyRemoveAds()
    {
        adCont.RemoveAdsBought();
        SceneManager.LoadScene("MenuScene");
    }
    public void VolumeControl()
    {
        if (effectAS.volume == 1)
        {
            effectAS.volume = 0;
            volumeImg.sprite = volumeOff;
        }
        else
        {
            effectAS.volume = 1;
            volumeImg.sprite = volumeOn;
        }
        effectVolume = effectAS.volume;
        PlayerPrefs.SetFloat("EffectVolume", effectVolume);
    }
    public void ChangeHint(int aux)
    {
        hint[currentHint].SetActive(false);
        currentHint += aux;
        if (currentHint < 0)
            currentHint = 4;

        if (currentHint > 4)
            currentHint = 0;

        if (currentHint == 4)
        {
            PlayerPrefs.SetInt("NerdBoy", 1);
        }

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
    public void OpenReceivedUI(int qtd, int aux)
    {
        if (aux == 0)
        {
            receivedUI.SetActive(true);
            receivedQtd.text = qtd.ToString();
            coinDisplayCanvas.GetComponent<Canvas>().sortingOrder = 5;
        }
        else
        {
            ChangeCanvas(10);
            skins[21] = 1;
            PlayerPrefs.SetInt("Skin21", 1);
            PlayerPrefs.Save();
            receivedSkinUI.SetActive(true);
            coinDisplayCanvas.GetComponent<Canvas>().sortingOrder = 5;
        }
    }
    public void CloseReceivedUI(int aux)
    {
        if (aux == 0)
        {
            receivedUI.SetActive(false);
            coinDisplayCanvas.GetComponent<Canvas>().sortingOrder = 0;
        }
        else
        {
            receivedSkinUI.SetActive(false);
            coinDisplayCanvas.GetComponent<Canvas>().sortingOrder = 0;
            SceneManager.LoadScene("MenuScene");
        }
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
    public void StoreScroll(float aux)
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
    public void ResetCounterScrollRect(int aux)     //Called when store RectScroll stops moving
    {
        if (aux == 0)                                //Ball Skins
        {
            delayCenterSkin = 0;
            enableDelayCenterSkin = true;
            doOnceCalculateSkinPos = true;
        }

        if (aux == 1)                                //Line Skins
        {
            delayCenterLine = 0;
            enableDelayCenterLine = true;
            doOnceCalculateLinePos = true;
        }
    }
    private void CalculatePositionSkinScrollRect()  //Calculate what position RectScroll should be
    {
        shorterDist = 9999;
        float currentPos = skinScrollRect.horizontalNormalizedPosition;
        for (int i = 0; i < totalSkinQtt; i++)
        {
            //Formula => CenteredPosition = [1 / (NºTotalSkins - 1) ] * Index
            float b = totalSkinQtt - 1;
            float a = 1 / b;
            float c = a * i;
            centeredPos = c;
            float dist = Mathf.Abs(currentPos - centeredPos);
            if (dist < shorterDist)
            {
                shorterDist = dist;
                shorterDistIndex = i;
            }

        }
        enableSkinCentering = true;
    }
    private void SkinCentering()
    {
        float b = totalSkinQtt - 1;
        float a = 1 / b;
        float c = a * shorterDistIndex;
        centeredPos = c;
        float currentPos = skinScrollRect.horizontalNormalizedPosition;
        currentPos = Mathf.Lerp(currentPos, centeredPos, 0.2f);
        skinScrollRect.horizontalNormalizedPosition = currentPos;
        if (Mathf.Abs(currentPos - centeredPos) < 0.01f)
        {
            enableDelayCenterSkin = false;
            enableSkinCentering = false;
        }
    }
    private void CalculatePositionLineScrollRect()
    {
        shorterDistLine = 9999;
        float currentPosLine = lineScrollRect.horizontalNormalizedPosition;
        for (int i = 0; i < totalLineQtt; i++)
        {
            float b = totalLineQtt - 1;
            float a = 1 / b;
            float c = a * i;
            centeredPosLine = c;
            float distLine = Mathf.Abs(currentPosLine - centeredPosLine);
            if (distLine < shorterDistLine)
            {
                shorterDistLine = distLine;
                shorterDistIndexLine = i;
            }

        }
        enableLineCentering = true;
    }
    private void LineCentering()
    {
        float b = totalLineQtt - 1;
        float a = 1 / b;
        float c = a * shorterDistIndexLine;
        centeredPosLine = c;
        float currentPosLine = lineScrollRect.horizontalNormalizedPosition;
        currentPosLine = Mathf.Lerp(currentPosLine, centeredPosLine, 0.2f);
        lineScrollRect.horizontalNormalizedPosition = currentPosLine;
        if (Mathf.Abs(currentPosLine - centeredPosLine) < 0.01f)
        {
            enableDelayCenterLine = false;
            enableLineCentering = false;
        }
    }
    public void CallCheckAd()
    {
        if (!adCont.CheckAvlbRewarded())
        {
            watchADButton.enabled = false;
        }
        else
        {
            watchADButton.enabled = true;
        }
    }

    public void DeleteGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MenuScene");
    }
}
