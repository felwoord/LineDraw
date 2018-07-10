using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAura : MonoBehaviour {
    private Quaternion rot;

    
    private void Awake()
    {
        rot = transform.rotation;
    }
    void Start () {


	}
	void Update () {

    }
    private void LateUpdate()
    {
        transform.rotation = rot;
    }
}
