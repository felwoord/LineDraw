using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpNotification : MonoBehaviour {
    float posy;
	// Use this for initialization
	void Start () {
        posy = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, posy + Mathf.Cos(Time.time * 10) * 5, transform.position.z);
	}
}
