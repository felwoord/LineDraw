using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotAnimation : MonoBehaviour {
    private float yPosO, yPosF;
    private float counter = 0;
    private RectTransform rectTrans;

	// Use this for initialization
	void Start () {
        rectTrans = GetComponent<RectTransform>();
        yPosO = rectTrans.transform.position.y;
        yPosF = yPosO + 20;
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if(counter < 0.5f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(rectTrans. transform.position.y, yPosF, 0.2f), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(rectTrans.transform.position.y, yPosO, 0.2f), transform.position.z);
        }

        if(counter > 1)
        {
            counter = 0;
        }
	}
}
