﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarrier : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
