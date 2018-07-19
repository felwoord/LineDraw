using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleAura : MonoBehaviour {
    private Color tmp;
    private SpriteRenderer rend;
    private Color tmpPlayer;
    private SpriteRenderer rendPlayer;
    private bool up;

    private float counter;
    private bool ending;
    private float vel;
	public void Start () {
        rend = GetComponent<SpriteRenderer>();
        rendPlayer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        up = false;
        counter = 0;
        ending = false;
        vel = 1;
        tmp.a = 1;
        rend.color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        tmp = rend.color;
        tmpPlayer = rendPlayer.color;

        if (up)
        {
            tmp.a += vel * Time.deltaTime;
            tmpPlayer.a += vel * Time.deltaTime;
        }
        else
        {
            tmp.a -= vel * Time.deltaTime;
            tmpPlayer.a -= vel * Time.deltaTime;
        }

        rend.color = tmp;
        rendPlayer.color = tmpPlayer;

        if (tmp.a > 0.8f)
        {
            up = false;
        }
        if (tmp.a < 0.2f)
        {
            up = true;
        }

        if (counter > 13)
        {
            ending = true;
        }
        if (ending)
        {
            vel = 10f;
        }
        else
        {
            vel = 1;
        }
    }
}
