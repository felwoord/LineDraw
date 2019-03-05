using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkDoubleCoinButton : MonoBehaviour
{

    public Text doubleCoinText;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameObject.GetComponent<Button>().interactable)
        {
            gameObject.GetComponent<BlinkDoubleCoinButton>().enabled = false;
        }
        else
        {
            counter = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter < 0.2f)
        {
            doubleCoinText.enabled = false;
        }
        else
        {
            doubleCoinText.enabled = true;
        }

        if(counter > 0.4f)
        {
            counter = 0;
        }
    }
}
