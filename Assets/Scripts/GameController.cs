using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject linePrefab;

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

    private float setSpawnCounter, missesCounter;
    private float sideBarrierSpawnCounter;

    public GameObject mainCanvas, endRunCanvas;

    private int coinsCount, totalCoins;
    private Text coinTxt, coinEndRunTxt;

    void Start()
    {
        GameObjectFind();
        GetPlayerPrefs();
        Inicialization();
    }

    void Update()
    {
        LineDraw();
        if (playerAlive)
        {
            CameraPlayerMov();

            Score();

            SpawnObjects();
        }
        Progress();
        

    }
    private void GameObjectFind()
    {
        player = GameObject.Find("Player");
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
    }
    private void Inicialization()
    {
        maxSpeed = 2;
        upForce = new Vector2(0, 10);
        camSpeed = 0.6f;

        currentHeight = 0;
        topHeightTxt.text = topHeight.ToString("0");
        coinsCount = 0;
        coinTxt.text = coinsCount.ToString();
        setSpawnCounter = 0;
        sideBarrierSpawnCounter = 0;
        missesCounter = 0;

        playerAlive = true;

        doOnce50 = false;
        doOnce100 = false;
        
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
        if(setSpawnCounter > 3.5f)
        {
            float rand = Random.Range(0, 10);
            if(missesCounter >= 3)
            {
                rand = 10;
                missesCounter = 0;
            }
            if (rand > 2)
            {
                int setRand = Random.Range(1, 6);
                switch (setRand)
                {
                    case 1:
                        GameObject set1 = Instantiate(Resources.Load("Set1") as GameObject);
                        set1.transform.position = new Vector3(0, player.transform.position.y + 10, 0);
                        Transform set1Transf = set1.GetComponent<Transform>();
                        foreach (Transform child in set1Transf) if (child.CompareTag("IceBlock"))
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                            }
                        break;
                    case 2:
                        GameObject set2 = Instantiate(Resources.Load("Set2") as GameObject);
                        set2.transform.position = new Vector3(0, player.transform.position.y + 10, 0);
                        Transform set2Transf = set2.GetComponent<Transform>();
                        foreach (Transform child in set2Transf) if (child.CompareTag("IceBlock"))
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                            }
                        break;
                    case 3:
                        GameObject set3 = Instantiate(Resources.Load("Set3") as GameObject);
                        set3.transform.position = new Vector3(0, player.transform.position.y + 10, 0);
                        Transform set3Transf = set3.GetComponent<Transform>();
                        foreach (Transform child in set3Transf) if (child.CompareTag("IceBlock"))
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                            }
                        break;
                    case 4:
                        GameObject set4 = Instantiate(Resources.Load("Set4") as GameObject);
                        set4.transform.position = new Vector3(0, player.transform.position.y + 10, 0);
                        Transform set4Transf = set4.GetComponent<Transform>();
                        foreach (Transform child in set4Transf) if (child.CompareTag("IceBlock"))
                            {
                                child.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                            }
                        break;
                    case 5:
                        GameObject set5 = Instantiate(Resources.Load("Set5") as GameObject);
                        set5.transform.position = new Vector3(0, player.transform.position.y + 10, 0);
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
            if (rand > 3)
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
    private void Progress()
    {
        maxSpeed = 2 + Mathf.Sqrt(currentHeight)/10.0f;
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
        if (playerRB.velocity.y < maxSpeed)
        {
            playerRB.AddForce(upForce, ForceMode2D.Force);
        }

        if (cam.transform.position.y > player.transform.position.y + 1.5f)
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

        mainCanvas.SetActive(false);
        endRunCanvas.SetActive(true);
        currentHeightTxtEndRun = GameObject.Find("CurrentHeightEndRun").GetComponent<Text>();
        currentHeightTxtEndRun.text = currentHeight.ToString("0");
        topHeightTxtEndRun = GameObject.Find("TopHeightEndRun").GetComponent<Text>();
        topHeightTxtEndRun.text = topHeight.ToString("0");
        PlayerPrefs.SetFloat("TopHeight", topHeight);
        coinEndRunTxt = GameObject.Find("CoinCountEndRun").GetComponent<Text>();
        coinEndRunTxt.text = coinsCount.ToString();
        totalCoins += coinsCount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
    }
    public void EndRunButton(int aux)
    {
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

}
