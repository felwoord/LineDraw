using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour {
    private BoxCollider2D boxCol;
    private EdgeCollider2D edgeCol;
    private PlayerController playerCont;

	// Use this for initialization
	void Start () {
	    if(gameObject.tag == "Spike")
        {
            edgeCol = GetComponent<EdgeCollider2D>();
        }
        else
        {
            boxCol = GetComponent<BoxCollider2D>();
        }
        playerCont = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerCont.invincible)
        {
            if (gameObject.tag == "Spike")
            {
                edgeCol.enabled = false;
            }
            else
            {
                boxCol.enabled = false;
            }
        }
        else
        {
            if (gameObject.tag == "Spike")
            {
                edgeCol.enabled = true;
            }
            else
            {
                boxCol.enabled = true;
            }
        }
	}
}
