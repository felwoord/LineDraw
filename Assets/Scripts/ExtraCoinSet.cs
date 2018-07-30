using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraCoinSet : MonoBehaviour {
    private int counter;
    private Transform trans;
    private bool spawn;
    private GameController gameCont;
    private GameObject player;

    void Start()
    {
        gameCont = GameObject.Find("Main Camera").GetComponent<GameController>();
        player = GameObject.Find("Player");
        trans = GetComponent<Transform>();
        spawn = false;
    }

    void Update()
    {
        if (!spawn)
        {
            foreach (Transform child in trans) if (child.CompareTag("Coin"))
                {
                    counter++;
                }
            if (counter == 0)
            {
                GameObject extraCoins = Instantiate(Resources.Load("ExtraCoins") as GameObject);
                if (player.transform.position.x > 0)
                {
                    extraCoins.transform.position = new Vector3(player.transform.position.x - 1, player.transform.position.y, 0);
                }
                else
                {
                    extraCoins.transform.position = new Vector3(player.transform.position.x + 1, player.transform.position.y, 0);
                }
                for(int i = 0; i < 5; i++)
                {
                    gameCont.AddCoin();
                }
                int bonusCoinCounter = PlayerPrefs.GetInt("BonusCoinCounter", 0);
                PlayerPrefs.SetInt("BonusCoinCounter", bonusCoinCounter + 1);
                spawn = true;
            }
            else
            {
                counter = 0;
            }
        }

    }
}
