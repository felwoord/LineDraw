using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameController gameCont;
    private bool endGame;
    private float endGameCount;

    private bool bounced;
    private float counter;

    private bool invincible;
    private float inviCounter;
    private GameObject inviAura;

    private bool magnet;
    private float magnetCounter;
    private GameObject magnetAura;

    private bool slowTime;
    private float slowTimeCounter;
    private GameObject slowTimeAura;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameCont = GameObject.Find("Main Camera").GetComponent<GameController>();
        inviAura = GameObject.Find("InviAura");
        inviAura.SetActive(false);
        magnetAura = GameObject.Find("MagnetAura");
        magnetAura.SetActive(false);
        slowTimeAura = GameObject.Find("SlowAura");
        slowTimeAura.SetActive(false);
        endGame = false;
        endGameCount = 0;
        bounced = false;
        counter = 0;
        invincible = false;
        inviCounter = 0;
    }
    
    void Update()
    {
        if(endGame)
        {
            endGameCount += Time.deltaTime;
            if(endGameCount > 0.05f)
            {
                Destroy(gameObject);
                gameCont.EndGame();
            }
        }
        if(bounced)
        {
            counter += Time.deltaTime;
            if(counter >= 0.02)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y) * 2;
                counter = 0;
                bounced = false;
            }
        }
        if(invincible)
        {
            inviCounter += Time.deltaTime;
            if(inviCounter > 15f)
            {
                inviCounter = 0;
                invincible = false;
                inviAura.SetActive(false);
            }
        }
        if(magnet)
        {
            magnetCounter += Time.deltaTime;
            if(magnetCounter > 20)
            {
                magnetCounter = 0;
                magnet = false;
                magnetAura.SetActive(false);
            }
        }
        if (slowTime)
        {
            slowTimeCounter += Time.deltaTime;
            if (slowTimeCounter > 10)
            {
                slowTimeCounter = 0;
                slowTime = false;
                slowTimeAura.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.tag == "Spike")
            {
                endGame = true;
            }
            if (collision.gameObject.tag == "HighBounceBarrier")
            {
                bounced = true;
            }
        }
        else
        {
            if (collision.gameObject.tag != "Line")
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin" && !magnet)
        {
            Destroy(collision.gameObject);
            gameCont.AddCoin();
        }
        if (collision.tag == "Invincible")
        {
            invincible = true;
            inviAura.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Shield")
        {
            GameObject shield = Instantiate(Resources.Load("ShieldSet") as GameObject);
            shield.transform.position = new Vector3(0, transform.position.y + 5, 0);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Magnet")
        {
            magnetAura.SetActive(true);
            magnet = true;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "SlowTime")
        {
            slowTimeAura.SetActive(true);
            slowTime = true;
            Time.timeScale = 0.5f;
            Destroy(collision.gameObject);
        }
    }
    void OnBecameInvisible()
    {
        endGame = true;
    }
}
