using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAura : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerCont;
    private GameController gameCont;

    void Start()
    {
        player = GameObject.Find("Player");
        playerCont = player.GetComponent<PlayerController>();
        gameCont = GameObject.Find("Main Camera").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x - 0.75f * Time.deltaTime, transform.localScale.y - 0.75f * Time.deltaTime, transform.localScale.z - 0.75f * Time.deltaTime);
        if (transform.localScale.x < 0.1f)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Coin" || collision.tag == "SuperCoin" || collision.tag == "Magnet" || collision.tag == "Shield" || collision.tag == "Invincible" || collision.tag == "SlowTime")
        {
            collision.transform.position = new Vector2(Mathf.Lerp(collision.transform.position.x, player.transform.position.x, 0.05f), Mathf.Lerp(collision.transform.position.y, player.transform.position.y, 0.05f));
            float dist = Vector2.Distance(player.transform.position, collision.transform.position);
            if (dist < 0.75f)
            {
                if (collision.tag == "Coin")
                {
                    playerCont.GotIt(1, collision);
                }
                if (collision.tag == "SuperCoin")
                {
                    playerCont.GotIt(2, collision);
                }
                if (collision.tag == "Magnet")
                {
                    playerCont.GotIt(3, collision);
                }
                if (collision.tag == "Shield")
                {
                    playerCont.GotIt(4, collision);
                }
                if (collision.tag == "Invincible")
                {
                    playerCont.GotIt(5, collision);
                }
                if (collision.tag == "SlowTime")
                {
                    playerCont.GotIt(6, collision);
                }
            }
        }
    }
}
