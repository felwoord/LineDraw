﻿using System.Collections;
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

    private float spikeSpawnCounter;
    private float sideBarrierSpawnCounter;

    public GameObject mainCanvas, endRunCanvas;

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
    }
    private void GetPlayerPrefs()
    {
        topHeight = PlayerPrefs.GetFloat("TopHeight", 0);
    }
    private void Inicialization()
    {
        maxSpeed = 2;
        upForce = new Vector2(0, 10);
        camSpeed = 0.6f;

        currentHeight = 0;
        topHeightTxt.text = topHeight.ToString();

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
        spikeSpawnCounter += Time.deltaTime;
        if(spikeSpawnCounter > 3)
        {
            float rand = Random.Range(0, 10);
            if(rand > 3)
            {
                GameObject spike = Instantiate(Resources.Load("IceBlock") as GameObject);
                spike.transform.position = new Vector3(Random.Range(-2.25f, 2.25f), player.transform.position.y + 12, 0);
                spike.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 70));
                  
            }
            spikeSpawnCounter = 0;
        }

        sideBarrierSpawnCounter += Time.deltaTime;
        if(sideBarrierSpawnCounter > 1)
        {
            float rand = Random.Range(0, 10);
            if (rand > 0)
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
        mainCanvas.SetActive(false);
        endRunCanvas.SetActive(true);
        currentHeightTxtEndRun = GameObject.Find("CurrentHeightEndRun").GetComponent<Text>();
        currentHeightTxtEndRun.text = currentHeight.ToString("0");
        topHeightTxtEndRun = GameObject.Find("TopHeightEndRun").GetComponent<Text>();
        topHeightTxtEndRun.text = topHeight.ToString("0");
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

}
