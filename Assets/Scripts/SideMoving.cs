using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMoving : MonoBehaviour {
    private bool mov;
    private float speed;
    private bool right;
    private GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        float rand = Random.Range(0, 10);
        if (rand > 5 && player.transform.position.y > 100)
        {
            mov = true;
            speed = Random.Range(0.5f, 3f);
        }
        else
        {
            mov = false;
            speed = 0;
        }
        if (gameObject.tag == "Coin")
            speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (mov)
        {
            if (right)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            if(transform.position.x > 1.6f)
            {
                right = false;
            }
            if(transform.position.x < -1.6f)
            {
                right = true;
            }
        }
    }
}
