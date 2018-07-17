using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sets : MonoBehaviour {
    GameObject player;
    private GameController gameCont;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        gameCont = GameObject.Find("Main Camera").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameCont.playerAlive)
        {
            if (transform.position.y + 20 < player.transform.position.y)
            {
                Destroy(gameObject);
            }
        }
	}
}
