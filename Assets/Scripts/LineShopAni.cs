using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineShopAni : MonoBehaviour
{
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(img.fillAmount >= 0.99f)
        {
            img.fillAmount = 0;
        }
        img.fillAmount += Time.deltaTime * 1.5f;
    }
}
