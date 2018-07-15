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
    private bool playerAlive;

    private float maxSpeed;
    private Vector2 upForce;
    private float camSpeed;
    private bool doOnce50, doOnce100;

    private Text currentHeightTxt, topHeightTxt, currentHeightTxtEndRun, topHeightTxtEndRun;
    private float currentHeight, topHeight;

    private float setSpawnCounter, missesCounter, lastSetHeight, lastSetPosition;
    private float sideBarrierSpawnCounter;

    public GameObject mainCanvas, endRunCanvas, pauseCanvas;

    private int coinsCount, totalCoins, coinsCountAnimation;
    private Text coinTxt, coinEndRunTxt;

    private int currentSkin;

    private AdController adCont;

    private int diamondQtd;
    private bool continueAvlb;
    private Text diamondQtdTxt;
    private GameObject continueButtonGO;

    private bool doubleCoinAvlb;
    private bool doubleCoinAnimat;
    private int doubleCoinQtd;
    public GameObject doubleCoinGO;

    private bool paused;

    void Start()
    {
        adCont = GameObject.Find("AdControl").GetComponent<AdController>();
        adCont.ShowInterstitial();

        currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
        player = Instantiate(Resources.Load("Skin" + currentSkin) as GameObject);
        player.name = "Player";
        player.transform.position = new Vector2(0, 0);

        GameObjectFind();
        GetPlayerPrefs();
        Inicialization();
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
    }
    private void Inicialization()
    {
        maxSpeed = 3.5f;
        upForce = new Vector2(0, 10);
        camSpeed = 0.6f;

        currentHeight = 0;
        topHeightTxt.text = topHeight.ToString("0");
        coinsCount = 0;
        coinTxt.text = coinsCount.ToString();
        setSpawnCounter = 0;
        sideBarrierSpawnCounter = 0;
        missesCounter = 0;
        lastSetHeight = 0;
        lastSetPosition = 10;

        playerAlive = true;

        doOnce50 = false;
        doOnce100 = false;

        doubleCoinAvlb = true;
        doubleCoinAnimat = false;
        doubleCoinQtd = 0;

        if (diamondQtd > 0)
        {
            continueAvlb = true;
        }
        else
        {
            continueAvlb = false;
        }
        paused = false;
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
        setSpawnCounter += Time.deltaTime;
        if (setSpawnCounter > 1f)
        {
            float rand = Random.Range(0, 10);
            if (missesCounter >= 2)
            {
                rand = 10;
                missesCounter = 0;
            }
            if (rand > 1)
            {
                if (lastSetPosition < player.transform.position.y)
                {
                    lastSetPosition = player.transform.position.y;
                }
                int setRand = Random.Range(1, 6);
                switch (setRand)
                {
                    case 1:
                        GameObject set1 = Instantiate(Resources.Load("Set1") as GameObject);
                        set1.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 3, 0);
                        lastSetHeight = set1.transform.Find("Height").localPosition.y;
                        lastSetPosition = set1.transform.position.y;
                        Transform set1Transf = set1.GetComponent<Transform>();
                        foreach (Transform child in set1Transf) if (child.CompareTag("IceBlock"))
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                            }
                        break;
                    case 2:
                        GameObject set2 = Instantiate(Resources.Load("Set2") as GameObject);
                        set2.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 3, 0);
                        lastSetHeight = set2.transform.Find("Height").localPosition.y;
                        lastSetPosition = set2.transform.position.y;
                        Transform set2Transf = set2.GetComponent<Transform>();
                        foreach (Transform child in set2Transf) if (child.CompareTag("IceBlock"))
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                            }
                        break;
                    case 3:
                        GameObject set3 = Instantiate(Resources.Load("Set3") as GameObject);
                        set3.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 3, 0);
                        lastSetHeight = set3.transform.Find("Height").localPosition.y;
                        lastSetPosition = set3.transform.position.y;
                        Transform set3Transf = set3.GetComponent<Transform>();
                        foreach (Transform child in set3Transf) if (child.CompareTag("IceBlock"))
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                            }
                        break;
                    case 4:
                        GameObject set4 = Instantiate(Resources.Load("Set4") as GameObject);
                        set4.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 3, 0);
                        lastSetHeight = set4.transform.Find("Height").localPosition.y;
                        lastSetPosition = set4.transform.position.y;
                        Transform set4Transf = set4.GetComponent<Transform>();
                        foreach (Transform child in set4Transf) if (child.CompareTag("IceBlock"))
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                            }
                        break;
                    case 5:
                        GameObject set5 = Instantiate(Resources.Load("Set5") as GameObject);
                        set5.transform.position = new Vector3(0, lastSetPosition + lastSetHeight + 3, 0);
                        lastSetHeight = set5.transform.Find("Height").localPosition.y;
                        lastSetPosition = set5.transform.position.y;
                        Transform set5Transf = set5.GetComponent<Transform>();
                        foreach (Transform child in set5Transf) if (child.CompareTag("IceBlock"))
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                            }
                        break;
                    default:
                        break;
                }
                missesCounter = 0;
            }
            else
            {
                missesCounter++;
            }
            setSpawnCounter = 0;
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
        maxSpeed = 2 + Mathf.Sqrt(currentHeight) / 10.0f;
        upForce = new Vector2(0, 10 + Mathf.Sqrt(currentHeight) / 10.0f);
        camSpeed = 0.6f + Mathf.Sqrt(currentHeight) / 100;

        if (!doOnce50 && currentHeight > 500)
        {
            maxSpeed += 2;
            upForce = new Vector2(0, upForce.y + 2);
            camSpeed += 0.1f;
            doOnce50 = true;
        }

        if (!doOnce100 && currentHeight > 100)
        {
            maxSpeed += 3;
            upForce = new Vector2(0, upForce.y + 3);
            camSpeed += 0.3f;
            doOnce100 = true;
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
        }
        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
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
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + maxSpeed * camSpeed * Time.deltaTime, cam.transform.position.z);
        }
        else
        {
            cam.transform.position = new Vector3(cam.transform.position.x, player.transform.position.y + 1.5f, cam.transform.position.z);
        }
    }
    public void EndGame()
    {
        Time.timeScale = 1;
        playerAlive = false;
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("IceBlock"))
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
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Set"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("HighBounceBarrier"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Magnet"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Shield"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Invincible"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("SlowTime"))
        {
            Destroy(gameObj);
        }
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("SuperCoin"))
        {
            Destroy(gameObj);
        }

        mainCanvas.SetActive(false);
        endRunCanvas.SetActive(true);
        currentHeightTxtEndRun = GameObject.Find("CurrentHeightEndRun").GetComponent<Text>();
        currentHeightTxtEndRun.text = currentHeight.ToString("0");
        topHeightTxtEndRun = GameObject.Find("TopHeightEndRun").GetComponent<Text>();
        topHeightTxtEndRun.text = topHeight.ToString("0");
        PlayerPrefs.SetFloat("TopHeight", topHeight);
        coinEndRunTxt = GameObject.Find("CoinCountEndRun").GetComponent<Text>();
        coinEndRunTxt.text = coinsCount.ToString();
        if (doubleCoinAvlb)
        {
            doubleCoinGO.SetActive(true);
        }
        else
        {
            doubleCoinGO.SetActive(false);
        }
        diamondQtdTxt = GameObject.Find("DiamondQtd").GetComponent<Text>();
        diamondQtdTxt.text = diamondQtd.ToString();
        continueButtonGO = GameObject.Find("ContinueButton");
        if (!continueAvlb)
        {
            continueButtonGO.SetActive(false);
        }


    }
    public void WatchAd()
    {
        adCont.ShowRewardedVideo(1);
    }
    public void AdCompleted()
    {
        doubleCoinAnimat = true;
        doubleCoinQtd = coinsCount * 2;
        coinsCountAnimation = coinsCount;
        coinsCount = doubleCoinQtd;
        doubleCoinGO.SetActive(false);
        doubleCoinAvlb = false;
    }
    public void EndRunButton(int aux)
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
    }
    public void AddCoin()
    {
        coinsCount++;
        coinTxt.text = coinsCount.ToString();
    }
    public void ContinueButton()
    {
        continueAvlb = false;
        diamondQtd--;
        PlayerPrefs.SetInt("DiamondQtd", diamondQtd);
        player = Instantiate(Resources.Load("Skin" + currentSkin) as GameObject);
        player.transform.position = new Vector3(0, currentHeight, 0);
        player.name = "Player";
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAlive = true;
        doubleCoinAvlb = true;
        mainCanvas.SetActive(true);
        endRunCanvas.SetActive(false);
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
}
