using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraCoins : MonoBehaviour {
    private float counter;
	void Start () {
        counter = 0;
	}
	
	void Update () {
        counter += Time.deltaTime;	
        if(counter < 0.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 2 * Time.deltaTime, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 2 * Time.deltaTime, 0);
        }
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
