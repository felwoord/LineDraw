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
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameCont = GameObject.Find("Main Camera").GetComponent<GameController>();
        endGame = false;
        endGameCount = 0;
        bounced = false;
        counter = 0;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            endGame = true;
        }
        if(collision.gameObject.tag == "HighBounceBarrier")
        {
            bounced = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            gameCont.AddCoin();
        }
    }
    void OnBecameInvisible()
    {
        endGame = true;
    }
}
