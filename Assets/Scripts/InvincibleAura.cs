using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleAura : MonoBehaviour {
    private Color tmp;
    private SpriteRenderer rend;
    private bool up;
	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        up = false;
	}
	
	// Update is called once per frame
	void Update () {
        tmp = rend.color;

        if (up)
        {
            tmp.a += 1 * Time.deltaTime;
        }
        else
        {
            tmp.a -= 1 * Time.deltaTime;
        }

        rend.color = tmp;

        Debug.Log(rend.color.a);
        if(tmp.a > 0.9f)
        {
            up = false;
        }
        if(tmp.a < 0.1f)
        {
            up = true;
        }
	}
}
