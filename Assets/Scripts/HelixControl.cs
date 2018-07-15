using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixControl : MonoBehaviour {
    float speed;
	// Use this for initialization
	void Start () {
        int x = Random.Range(0, 2);
        if(x == 0)
        {
            speed = 100;
        }
        else
        {
            speed = -100;
        }
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.forward * (speed * Time.deltaTime));

    }
}
