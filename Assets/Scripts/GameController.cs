using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    private GameObject linePrefab;
    private int currentLine;
    public GameObject[] linesPrefabs;
    private Line activeLine;
    private Rigidbody2D playerRB;
    private GameObject player;
    private GameObject cam;
    public bool playerAlive;

    private float maxSpeed;
    private Vector2 upForce;
    private float camSpeed, totalCamSpeed;
    private bool doOnce25, doOnce50, doOnce75, doOnce100, doOnce125, doOnce150, doOnce175, doOnce200, doOnce300, doOnce350;
    private bool doOnce400, doOnce450, doOnce500, doOnce550, doOnce600, doOnce650, doOnce700, doOnce750, doOnce800, doOnce850, doOnce900, doOnce950, doOnce1000, doOnce1100;
    private bool doOnce1200, doOnce1300, doOnce2000;

    private Text currentHeightTxt, topHeightTxt, currentHeightTxtEndRun, topHeightTxtEndRun;
    private float currentHeight, topHeight;

    private float[] setsPos = new float[4]; 
    private float lastSetHeight, lastSetPosition;
    private float powerUpsCDCounter;
    private bool powerUpsCD;
    private float sideBarrierSpawnCounter;

    public GameObject mainCanvas, endRunCanvas, pauseCanvas, continueCanvas;

    private int coinsCount, totalCoins, coinsCountAnimation;
    private Text coinTxt, coinEndRunTxt;

    private int currentSkin;

    private AdController adCont;

    private int diamondQtd;
    private bool continueAvlb;
    private Text diamondQtdTxt;
    private float countdown;
    private Text countdownText;
    private bool continueCanvasOpen;
    private bool endAnimation;
    private bool doubleCoinAvlb;
    private bool doubleCoinAnimat;
    private int doubleCoinQtd;
    private RectTransform heightsRT, coinsRT, watchAdRT;
    private float heightsPosY, coinsPosX, watchAdPosX;
    public GameObject doubleCoinGO;

    private bool paused;

    public AudioSource[] effectAS;
    private float effectVolume;
    private bool drawAudioCD;
    private float drawAudioCounterCD;
    private float drawCounter;

    public GameObject ballBonusCoin;

    void Start()
    {

        adCont = GameObject.Find("AdControl").GetComponent<AdController>();
        adCont.ShowInterstitial();

        currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
        player = Instantiate(Resources.Load("Balls/Skin" + currentSkin) as GameObject);
        player.name = "Player";
        player.transform.position = new Vector2(0, 0);

        
        GameObjectFind();
        GetPlayerPrefs();
        Inicialization();
        CameraResizer();
        CallCheckAd();
    }

    void Update()
    {
        if (!paused)
        {
            LineDraw();
        }
        if (playerAlive)
        {
            CameraPlayerMov();

            Score();

            SpawnObjects();
        }
        Progress();

        if (doubleCoinAnimat)
        {
            DoubleCoinAnimation();
        }


        if (drawAudioCD)
            drawAudioCounterCD += Time.deltaTime;

        if (drawAudioCounterCD >= 0.5f)
        {
            drawAudioCD = false;
            drawAudioCounterCD = 0;
        }

        if (continueCanvasOpen)
        {
            if(countdown > 0)
            {
                countdown -= Time.deltaTime;
                countdownText.text = countdown.ToString("0");
            }
            else
            {
                continueCanvasOpen = false;
                mainCanvas.SetActive(false);
                EndGame();
            }
           
        }

        if (endAnimation)
        {
            EndGameAnimation();
        }


    }
    private void CameraResizer()
    {
        float width = Display.main.systemWidth;
        float height = Display.main.systemHeight;
        float ratio = width / height;
        if(ratio <= 0.5f)
        {
            cam.GetComponent<Camera>().orthographicSize = 6;
        }
    }
    private void GameObjectFind()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera");
        currentHeightTxt = GameObject.Find("CurrentHeight").GetComponent<Text>();
        topHeightTxt = GameObject.Find("TopHeight").GetComponent<Text>();
        coinTxt = GameObject.Find("CoinCount").GetComponent<Text>();
    }
    private void GetPlayerPrefs()
    {
        topHeight = PlayerPrefs.GetFloat("TopHeight", 0);
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        diamondQtd = PlayerPrefs.GetInt("DiamondQtd", 0);
        currentLine = PlayerPrefs.GetInt("CurrentLine", 0);
        effectVolume = PlayerPrefs.GetFloat("EffectVolume", 1);
    }
    private void Inicialization()
    {
        maxSpeed = 2f;
        upForce = new Vector2(0, 10);
        camSpeed = 0.4f;

        currentHeight = 0;
        topHeightTxt.text = topHeight.ToString("0");
        coinsCount = 0;
        coinTxt.text = coinsCount.ToString();
        sideBarrierSpawnCounter = 0;
        lastSetHeight = 0;
        powerUpsCDCounter = 0;
        powerUpsCD = false;
        lastSetPosition = 10;

        foreach(AudioSource audioS in effectAS)
        {
            if (effectVolume == 0)
            {
                audioS.enabled = false;
            }
        }

        playerAlive = true;

        doOnce25 = false;
        doOnce50 = false;
        doOnce75 = false;
        doOnce100 = false;
        doOnce125 = false;
        doOnce150 = false;
        doOnce175 = false;
        doOnce200 = false;
        doOnce300 = false;
        doOnce350 = false;



        doubleCoinAvlb = true;
        doubleCoinAnimat = false;
        doubleCoinQtd = 0;

        if (diamondQtd >= 3)
        {
            continueAvlb = true;
        }
        else
        {
            continueAvlb = false;
        }
        paused = false;
        endAnimation = false;
        linePrefab = linesPrefabs[currentLine];

    }
    private void Score()
    {
        if (player.transform.position.y > currentHeight)
        {
            currentHeight = player.transform.position.y;
            currentHeightTxt.text = currentHeight.ToString("0");
        }
        if (currentHeight > topHeight)
        {
            topHeight = currentHeight;
            topHeightTxt.text = topHeight.ToString("0");
        }
    }
    private void SpawnObjects()
    {
        if (player.transform.position.y > setsPos[0])
        {
            for (int i = 0; i < 3; i++)
            {
                setsPos[i] = setsPos[i + 1];
            }
            if (lastSetPosition < player.transform.position.y)
            {
                lastSetPosition = player.transform.position.y;
            }
            int setRand = 0;
            if (powerUpsCD || player.transform.position.y < 50)
            {
                setRand = Random.Range(1, 20);
                powerUpsCDCounter += Time.deltaTime;
                if (powerUpsCDCounter > 10)
                {
                    powerUpsCD = false;
                }
            }
            else
            {
                setRand = Random.Range(1, 24);
            }
            switch (setRand)
            {
                case 1:
                    GameObject set1 = Instantiate(Resources.Load("Sets/Set1") as GameObject);
                    set1.transform.position = new Vector3(Random.Range(-1.75f, 1.75f), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set1.transform.Find("Height").localPosition.y;
                    lastSetPosition = set1.transform.position.y;
                    Transform set1Transf = set1.GetComponent<Transform>();
                    foreach (Transform child in set1Transf) if (child.CompareTag("IceBlock"))
                        {
                            child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                        }
                    break;
                case 2:
                    GameObject set2 = Instantiate(Resources.Load("Sets/Set2") as GameObject);
                    set2.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set2.transform.Find("Height").localPosition.y;
                    lastSetPosition = set2.transform.position.y;
                    Transform set2Transf = set2.GetComponent<Transform>();
                    foreach (Transform child in set2Transf) if (child.CompareTag("IceBlock"))
                        {
                            child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                        }
                    break;
                case 3:
                    GameObject set3 = Instantiate(Resources.Load("Sets/Set3") as GameObject);
                    set3.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set3.transform.Find("Height").localPosition.y;
                    lastSetPosition = set3.transform.position.y;
                    Transform set3Transf = set3.GetComponent<Transform>();
                    foreach (Transform child in set3Transf) if (child.CompareTag("IceBlock"))
                        {
                            child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                        }
                    break;
                case 4:
                    GameObject set4 = Instantiate(Resources.Load("Sets/Set4") as GameObject);
                    set4.transform.position = new Vector3(Random.Range(-1f, 1f), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set4.transform.Find("Height").localPosition.y;
                    lastSetPosition = set4.transform.position.y;
                    Transform set4Transf = set4.GetComponent<Transform>();
                    foreach (Transform child in set4Transf) if (child.CompareTag("IceBlock"))
                        {
                            child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                        }
                    int aux4 = Random.Range(0, 2);
                    if (aux4 == 0)
                    {
                        set4.transform.Rotate(Vector3.up * 180);
                    }
                    break;
                case 5:
                    GameObject set5 = Instantiate(Resources.Load("Sets/Set5") as GameObject);
                    set5.transform.position = new Vector3(Random.Range(-1f, 1f), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set5.transform.Find("Height").localPosition.y;
                    lastSetPosition = set5.transform.position.y;
                    Transform set5Transf = set5.GetComponent<Transform>();
                    foreach (Transform child in set5Transf) if (child.CompareTag("IceBlock"))
                        {
                            child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                        }
                    int aux5 = Random.Range(0, 2);
                    if (aux5 == 0)
                    {
                        set5.transform.Rotate(Vector3.up * 180);
                    }
                    break;
                case 6:
                    GameObject set6 = Instantiate(Resources.Load("Sets/Set6") as GameObject);
                    set6.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set6.transform.Find("Height").localPosition.y;
                    lastSetPosition = set6.transform.position.y;
                    break;
                case 7:
                    GameObject set7 = Instantiate(Resources.Load("Sets/Set7") as GameObject);
                    set7.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set7.transform.Find("Height").localPosition.y;
                    lastSetPosition = set7.transform.position.y;
                    int aux7 = Random.Range(0, 2);
                    if (aux7 == 0)
                    {
                        set7.transform.Rotate(Vector3.up * 180);
                    }
                    break;
                case 8:
                    GameObject set8 = Instantiate(Resources.Load("Sets/Set8") as GameObject);
                    set8.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set8.transform.Find("Height").localPosition.y;
                    lastSetPosition = set8.transform.position.y;
                    int aux8 = Random.Range(0, 2);
                    if (aux8 == 0)
                    {
                        set8.transform.Rotate(Vector3.up * 180);
                    }
                    break;
                case 9:
                    GameObject set9 = Instantiate(Resources.Load("Sets/Set9") as GameObject);
                    set9.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set9.transform.Find("Height").localPosition.y;
                    lastSetPosition = set9.transform.position.y;
                    Transform set9Transf = set9.GetComponent<Transform>();
                    foreach (Transform child in set9Transf) if (child.CompareTag("SideBarrier"))
                        {
                            int aux9 = Random.Range(0, 2);
                            if (aux9 == 0)                                                                                      //teste com angulo aleatorio
                            {                                                                                                   //se quiser, mudar para if(set9.transform.pos.x < 0)...
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(15, 75));
                            }
                            else
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-15, -75));
                            }
                        }
                    break;
                case 10:
                    GameObject set10 = Instantiate(Resources.Load("Sets/Set10") as GameObject);
                    set10.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set10.transform.Find("Height").localPosition.y;
                    lastSetPosition = set10.transform.position.y;
                    Transform set10Transf = set10.GetComponent<Transform>();
                    foreach (Transform child in set10Transf) if (child.CompareTag("SideBarrier"))
                        {
                            int aux10 = Random.Range(0, 2);
                            if (aux10 == 0)                                                                                      //teste com angulo aleatorio
                            {                                                                                                   //se quiser, mudar para if(set10.transform.pos.x < 0)...
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(15, 75));
                            }
                            else
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-15, -75));
                            }
                        }
                    break;
                case 11:
                    GameObject set11 = Instantiate(Resources.Load("Sets/Set11") as GameObject);
                    set11.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set11.transform.Find("Height").localPosition.y;
                    lastSetPosition = set11.transform.position.y;
                    Transform set11Transf = set11.GetComponent<Transform>();
                    foreach (Transform child in set11Transf) if (child.CompareTag("HighBounceBarrier"))
                        {
                            int aux11 = Random.Range(0, 2);
                            if (aux11 == 0)                                                                                      //teste com angulo aleatorio
                            {                                                                                                   //se quiser, mudar para if(set11.transform.pos.x < 0)...
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(15, 75));
                            }
                            else
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-15, -75));
                            }
                        }
                    break;
                case 12:
                    GameObject set12 = Instantiate(Resources.Load("Sets/Set12") as GameObject);
                    set12.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 6, 0);
                    lastSetHeight = set12.transform.Find("Height").localPosition.y;
                    lastSetPosition = set12.transform.position.y;
                    break;
                case 13:
                    GameObject set13 = Instantiate(Resources.Load("Sets/Set13") as GameObject);
                    set13.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set13.transform.Find("Height").localPosition.y;
                    lastSetPosition = set13.transform.position.y;
                    int aux13 = Random.Range(0, 2);
                    if (aux13 == 0)
                    {
                        set13.transform.Rotate(Vector3.up * 180);
                    }
                    break;
                case 14:
                    GameObject set14 = Instantiate(Resources.Load("Sets/Set14") as GameObject);
                    set14.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set14.transform.Find("Height").localPosition.y;
                    lastSetPosition = set14.transform.position.y;
                    int aux14 = Random.Range(0, 2);
                    if (aux14 == 0)
                    {
                        set14.transform.Rotate(Vector3.up * 180);
                    }
                    break;
                case 15:
                    GameObject set15 = Instantiate(Resources.Load("Sets/Set15") as GameObject);
                    set15.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set15.transform.Find("Height").localPosition.y;
                    lastSetPosition = set15.transform.position.y;
                    int aux15 = Random.Range(0, 2);
                    if (aux15 == 0)
                    {
                        set15.transform.Rotate(Vector3.up * 180);
                    }
                    break;
                case 16:
                    GameObject set16 = Instantiate(Resources.Load("Sets/Set16") as GameObject);
                    set16.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set16.transform.Find("Height").localPosition.y;
                    lastSetPosition = set16.transform.position.y;
                    int aux16 = Random.Range(0, 2);
                    if (aux16 == 0)
                    {
                        set16.transform.Rotate(Vector3.up * 180);
                    }
                    break;
                case 17:
                    GameObject set17 = Instantiate(Resources.Load("Sets/Set17") as GameObject);
                    set17.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set17.transform.Find("Height").localPosition.y;
                    lastSetPosition = set17.transform.position.y;
                    int aux17 = Random.Range(0, 2);
                    if (aux17 == 0)
                    {
                        set17.transform.Rotate(Vector3.up * 180);
                    }
                    break;
                case 18:
                    GameObject set18 = Instantiate(Resources.Load("Sets/Set18") as GameObject);
                    set18.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set18.transform.Find("Height").localPosition.y;
                    lastSetPosition = set18.transform.position.y;
                    Transform set18Transf = set18.GetComponent<Transform>();
                    foreach (Transform child in set18Transf) if (child.CompareTag("IceBlock"))
                        {
                            child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                        }
                    break;
                case 19:
                    GameObject set19 = Instantiate(Resources.Load("Sets/Set19") as GameObject);
                    set19.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set19.transform.Find("Height").localPosition.y;
                    lastSetPosition = set19.transform.position.y;
                    Transform set19Transf = set19.GetComponent<Transform>();
                    foreach (Transform child in set19Transf) if (child.CompareTag("IceBlock"))
                        {
                            child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                        }
                    break;
                case 20:
                    GameObject set20 = Instantiate(Resources.Load("Sets/Set20") as GameObject);
                    set20.transform.position = new Vector3(Random.Range(-2, 2), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set20.transform.Find("Height").localPosition.y;
                    lastSetPosition = set20.transform.position.y;
                    powerUpsCD = true;
                    break;
                case 21:
                    GameObject set21 = Instantiate(Resources.Load("Sets/Set21") as GameObject);
                    set21.transform.position = new Vector3(Random.Range(-2, 2), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set21.transform.Find("Height").localPosition.y;
                    lastSetPosition = set21.transform.position.y;
                    powerUpsCD = true;
                    break;
                case 22:
                    GameObject set22 = Instantiate(Resources.Load("Sets/Set22") as GameObject);
                    set22.transform.position = new Vector3(Random.Range(-2, 2), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set22.transform.Find("Height").localPosition.y;
                    lastSetPosition = set22.transform.position.y;
                    powerUpsCD = true;
                    break;
                case 23:
                    GameObject set23 = Instantiate(Resources.Load("Sets/Set23") as GameObject);
                    set23.transform.position = new Vector3(Random.Range(-2, 2), lastSetPosition + lastSetHeight + 4, 0);
                    lastSetHeight = set23.transform.Find("Height").localPosition.y;
                    lastSetPosition = set23.transform.position.y;
                    powerUpsCD = true;
                    break;
                default:
                    break;
            }

            setsPos[3] = lastSetPosition;
        }

        sideBarrierSpawnCounter += Time.deltaTime;
        if (sideBarrierSpawnCounter > 1)
        {
            float rand = Random.Range(0, 10);
            if (rand > 2)
            {
                GameObject sideBarrier = Instantiate(Resources.Load("SideBarrier") as GameObject);
                int side = Random.Range(0, 2);
                if (side == 0)
                {
                    sideBarrier.transform.position = new Vector3(-2.6f, player.transform.position.y + 12, 0);
                }
                else
                {
                    sideBarrier.transform.position = new Vector3(2.65f, player.transform.position.y + 12, 0);
                }
            }
            sideBarrierSpawnCounter = 0;
        }
    }
    private void DoubleCoinAnimation()
    {
        if (coinsCountAnimation < doubleCoinQtd)
        {
            coinsCountAnimation++;
            coinEndRunTxt.text = coinsCountAnimation.ToString();
        }
    }
    private void Progress()
    {
       if (!doOnce25 && currentHeight > 25)
        {
            maxSpeed = 2.20f;
            upForce = new Vector2(0, 15);
            camSpeed = 0.5f;
            doOnce25 = true;
        }

        if (!doOnce50 && currentHeight > 50)
        {
            maxSpeed = 2.40f;
            upForce = new Vector2(0, 15);
            camSpeed = 0.5f;
            doOnce50 = true;
        }

        if (!doOnce75 && currentHeight > 75)
        {
            maxSpeed = 2.60f;
            upForce = new Vector2(0, 17);
            camSpeed = 0.55f;
            doOnce75 = true;
        }

        if (!doOnce100 && currentHeight > 100)
        {
            maxSpeed = 2.80f;
            upForce = new Vector2(0, 20);
            camSpeed = 0.6f;
            doOnce100 = true;
        }

        if (!doOnce125 && currentHeight > 125)
        {
            maxSpeed = 3.0F;
            upForce = new Vector2(0, 22);
            camSpeed = 0.65f;
            doOnce125 = true;
        }

        if (!doOnce150 && currentHeight > 150)
        {
            maxSpeed = 3.2f;
            upForce = new Vector2(0, 25);
            camSpeed = 0.7f;
            doOnce150 = true;
        }

       if (!doOnce175 && currentHeight > 175)
        {
            maxSpeed = 3.4f;
            upForce = new Vector2(0, 27);
            camSpeed = 0.75f;
            doOnce175 = true;
        }

        if (!doOnce200 && currentHeight > 200)
        {
            maxSpeed = 3.6f;
            upForce = new Vector2(0, 30);
            camSpeed = 0.60f;
            doOnce200 = true;
        }

        if (!doOnce300 && currentHeight > 300)
        {
            maxSpeed = 3.8f;
            upForce = new Vector2(0, 30);
            camSpeed = 0.9f;
            doOnce300 = true;
        }

        if (!doOnce350 && currentHeight > 350)
        {
            maxSpeed = 3.8f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce350 = true;
        }
        if (!doOnce400 && currentHeight > 400)
        {
            maxSpeed = 4.0f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce400 = true;
        }
        if (!doOnce450 && currentHeight > 450)
        {
            maxSpeed = 4.2f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce450 = true;
        }
        if (!doOnce500 && currentHeight > 500)
        {
            maxSpeed = 4.4f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce500 = true;
        }
        if (!doOnce550 && currentHeight > 550)
        {
            maxSpeed = 4.6f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce550 = true;
        }
        if (!doOnce600 && currentHeight > 600)
        {
            maxSpeed = 4.8f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce600 = true;
        }
        if (!doOnce650 && currentHeight > 650)
        {
            maxSpeed = 5.0f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce650 = true;
        }
        if (!doOnce700 && currentHeight > 700)
        {
            maxSpeed = 5.2f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce700 = true;
        }
        if (!doOnce750 && currentHeight > 750)
        {
            maxSpeed = 5.4f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce750 = true;
        }
        if (!doOnce800 && currentHeight > 800)
        {
            maxSpeed = 5.6f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce800 = true;
        }
        if (!doOnce850 && currentHeight > 850)
        {
            maxSpeed = 5.8f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce850 = true;
        }
        if (!doOnce900 && currentHeight > 900)
        {
            maxSpeed = 6.0f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce900 = true;
        }
        if (!doOnce950 && currentHeight > 950)
        {
            maxSpeed = 6.2f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce950 = true;
        }
        if (!doOnce1000 && currentHeight > 1000)
        {
            maxSpeed = 6.4f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce1000 = true;
        }
        if (!doOnce1100 && currentHeight > 1100)
        {
            maxSpeed = 6.6f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce1100 = true;
        }
        if (!doOnce1200 && currentHeight > 1200)
        {
            maxSpeed = 6.8f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce1200 = true;
        }
        if (!doOnce1300 && currentHeight > 1300)
        {
            maxSpeed = 7.0f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce1300 = true;
        }




        if (!doOnce2000 && currentHeight > 2000)             ////////////////Hardcore
        {
            maxSpeed = 8.0f;
            upForce = new Vector2(0, 35);
            camSpeed = 0.95f;
            doOnce2000 = true;
        }



        totalCamSpeed = camSpeed * maxSpeed;
        if(totalCamSpeed > 4)
        {
            totalCamSpeed = 4;
        }

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
    private void CameraPlayerMov()
    {
        if (playerRB.velocity.y <= maxSpeed)
        {
            playerRB.AddForce(upForce, ForceMode2D.Force);
        }

        if (cam.transform.position.y >= player.transform.position.y + 1.5f)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + totalCamSpeed * Time.deltaTime, cam.transform.position.z);
        }
        else
        {
            cam.transform.position = new Vector3(cam.transform.position.x, player.transform.position.y + 1.5f, cam.transform.position.z);
        }
    }
    public void ContinueCanvas()
    {
        Time.timeScale = 1;
        playerAlive = false;

        PlayAudio(3);

        continueCanvasOpen = true;

        if (continueAvlb)
        {
            mainCanvas.SetActive(false);
            continueCanvas.SetActive(true);
            countdownText = GameObject.Find("Countdown").GetComponent<Text>();
            countdown = 5;
            diamondQtdTxt = GameObject.Find("DiamondQtd").GetComponent<Text>();
            diamondQtdTxt.text = diamondQtd.ToString();
            countdownText.text = countdown.ToString();
        }
        else
        {
            countdown = 0;
        }


        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Set"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("SideBarrier"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Line"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("ExtraCoins"))
        {
            Destroy(gameObj);
        }

        int deathCounter = PlayerPrefs.GetInt("DeathCounter", 0);
        PlayerPrefs.SetInt("DeathCounter", deathCounter + 1);

    }
    public void EndGame()
    {
        continueCanvas.SetActive(false);
        endRunCanvas.SetActive(true);

        currentHeightTxtEndRun = GameObject.Find("CurrentHeightEndRun").GetComponent<Text>();
        currentHeightTxtEndRun.text = currentHeight.ToString("0");
        float totalDistance = PlayerPrefs.GetFloat("TotalDistance", 0);
        PlayerPrefs.SetFloat("TotalDistance", totalDistance + currentHeight);
        topHeightTxtEndRun = GameObject.Find("TopHeightEndRun").GetComponent<Text>();
        topHeightTxtEndRun.text = topHeight.ToString("0");
        PlayerPrefs.SetFloat("TopHeight", topHeight);
        coinEndRunTxt = GameObject.Find("CoinCountEndRun").GetComponent<Text>();
        if(currentSkin == 16)
        {
            ballBonusCoin.SetActive(true);
            coinsCount = Mathf.CeilToInt(coinsCount * 1.1f);
        }
        coinEndRunTxt.text = coinsCount.ToString();
        if (doubleCoinAvlb)
        {
            doubleCoinGO.SetActive(true);
        }
        else
        {
            doubleCoinGO.SetActive(false);
        }
        heightsRT = GameObject.Find("Heights").GetComponent<RectTransform>();
        heightsPosY = heightsRT.localPosition.y;
        heightsRT.localPosition = new Vector2(heightsRT.localPosition.x, heightsRT.localPosition.y + 500);
        coinsRT = GameObject.Find("Coins").GetComponent<RectTransform>();
        coinsPosX = heightsRT.localPosition.x;
        coinsRT.localPosition = new Vector2(coinsRT.localPosition.x - 500, coinsRT.localPosition.y);
        watchAdRT = GameObject.Find("WatchAdButton").GetComponent<RectTransform>();
        watchAdPosX = watchAdRT.localPosition.x;
        watchAdRT.localPosition = new Vector2(watchAdRT.localPosition.x + 500, watchAdRT.localPosition.y);

        endAnimation = true;

    }
    private void EndGameAnimation()
    {
        heightsRT.localPosition = Vector2.Lerp(heightsRT.localPosition, new Vector2(heightsRT.localPosition.x, heightsPosY), 0.15f);
        coinsRT.localPosition = Vector2.Lerp(coinsRT.localPosition, new Vector2(coinsPosX, coinsRT.localPosition.y), 0.10f);
        watchAdRT.localPosition = Vector2.Lerp(watchAdRT.localPosition, new Vector2(watchAdPosX, watchAdRT.localPosition.y), 0.05f);
    }
    public void WatchAd()
    {
        adCont.ShowRewardedVideo(1);
    }
    public void AdCompleted()
    {
        adCont.ResetTimer();
        int adsWatched = PlayerPrefs.GetInt("AdsWatched", 0);
        adsWatched += 1;
        PlayerPrefs.SetInt("AdsWatched", adsWatched);
        PlayerPrefs.Save();
        doubleCoinAnimat = true;
        doubleCoinQtd = coinsCount * 2;
        coinsCountAnimation = coinsCount;
        coinsCount = doubleCoinQtd;
        doubleCoinGO.SetActive(false);
        doubleCoinAvlb = false;
    }
    public void EndRunButton(int aux)
    {
        if (aux <= 1)                                       //EndGame Buttons
        {
            totalCoins += coinsCount;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            if (aux == 1)
            {
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                SceneManager.LoadScene("MenuScene");
            }
        }                                                   //Pause Buttons
        else
        {
            if (aux == 3)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("MenuScene");
            }
        }
    }
    public void AddCoin()
    {
        coinsCount++;
        coinTxt.text = coinsCount.ToString();
        PlayAudio(0);
    }
    public void ContinueButton()
    {
        continueAvlb = false;
        diamondQtd -= 3;
        int diamondsUsed = PlayerPrefs.GetInt("DiamondsUsed", 0);
        PlayerPrefs.SetInt("DiamondsUsed", diamondsUsed + 3);
        PlayerPrefs.SetInt("DiamondQtd", diamondQtd);
        int continueCounter = PlayerPrefs.GetInt("ContinueCounter", 0);
        PlayerPrefs.SetInt("ContinueCounter", continueCounter + 1);
        player = Instantiate(Resources.Load("Balls/Skin" + currentSkin) as GameObject);
        player.transform.position = new Vector3(0, currentHeight, 0);
        player.name = "Player";
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAlive = true;
        lastSetPosition = player.transform.position.y + 5;
        doubleCoinAvlb = true;
        mainCanvas.SetActive(true);
        continueCanvas.SetActive(false);
        continueCanvasOpen = false;
        cam.transform.position = new Vector3(0, player.transform.position.y, cam.transform.position.z);

    }
    public void PauseButton()
    {
        if (!paused)
        {
            mainCanvas.SetActive(false);
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            mainCanvas.SetActive(true);
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
    }
    public void PlayAudio(int aux)
    {
        if (effectVolume != 0)
        {
            effectAS[aux].Play();
            if (aux == 2)
            {
                drawAudioCD = true;
            }
        }
    }
    public void CallCheckAd()
    {
        if (!adCont.CheckAvlbRewarded())
        {
            doubleCoinGO.GetComponent<Button>().interactable = false;
        }
        else
        {
            doubleCoinGO.GetComponent<Button>().interactable = true;
        }
    }
}
