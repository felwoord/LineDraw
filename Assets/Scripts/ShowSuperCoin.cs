using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSuperCoin : MonoBehaviour {
    public GameObject superCoin, shadowCoin;
    private int counter;
    private Transform trans;
    private bool spawn;
	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        spawn = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!spawn)
        {
            foreach (Transform child in trans) if (child.CompareTag("Coin"))
                {
                    counter++;
                }
            if (counter == 0)
            {
                superCoin.SetActive(true);
                shadowCoin.SetActive(false);
                spawn = true;
            }
            else
            {
                counter = 0;
            }
        }

    }
}
