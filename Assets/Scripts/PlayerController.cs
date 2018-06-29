using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameCont;
    private bool endGame;
    private float endGameCount;

    // Use this for initialization
    void Start()
    {
        gameCont = GameObject.Find("Main Camera").GetComponent<GameController>();
        endGame = false;
        endGameCount = 0;
    }

    // Update is called once per frame
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            endGame = true;
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
