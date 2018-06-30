using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public GameObject mainCanvas, shopCanvas, settingsCanvas;
    private Text totalCoinsTxt;
    private float totalCoins;

    // Use this for initialization
    void Start() {
        GameObjectFind();
        GetPlayerPrefs();
        Inicialization();
    }

    // Update is called once per frame
    void Update() {

    }

    private void GameObjectFind()
    {
        totalCoinsTxt = GameObject.Find("TotalCoins").GetComponent<Text>();
    }
    private void GetPlayerPrefs()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins");
    }
    private void Inicialization()
    {
        totalCoinsTxt.text = totalCoins.ToString();
    }

  

    public void ChangeCanvas(int aux)
    {
        switch (aux)
        {
            case 1:                            //main -> shop
                mainCanvas.SetActive(false);
                shopCanvas.SetActive(true);
                break;
            case 2:                            //main -> settings
                mainCanvas.SetActive(false);
                settingsCanvas.SetActive(true);
                break;
            case 3:                            //shop -> main
                shopCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                break;
            case 4:                            //settings -> main
                settingsCanvas.SetActive(false);
                mainCanvas.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
   
}
