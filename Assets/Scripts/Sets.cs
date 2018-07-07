using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sets : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y + 20 < player.transform.position.y)
        {//////////////////////////////////
            Destroy(gameObject);
        }////////////////////////////////
	}
}
