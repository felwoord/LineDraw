using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondFlare : MonoBehaviour {
    private float alphaColor;

    private bool up = true;

    private Color auxColor;
    private Color col;
    private float counter = 0;
	// Use this for initialization
	void Start () {
        col = GetComponent<Image>().color;
        auxColor = col;
    }
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if(up)
        {
            alphaColor = Mathf.Lerp(alphaColor, 1, 0.05f);
            auxColor.a = alphaColor;
            gameObject.GetComponent<Image>().color = auxColor;
            if (alphaColor > 0.95f)
            {
                up = false;
            }
        }
        else
        {
            alphaColor = Mathf.Lerp(alphaColor, 0, 0.05f);
            auxColor.a = alphaColor;
            gameObject.GetComponent<Image>().color = auxColor;

            if (alphaColor < 0.05f)
            {
                up = true;
            }
        }

        transform.Rotate(Vector3.forward * (25 * Time.deltaTime));
    }
}
