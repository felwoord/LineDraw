using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public GameObject mainCanvas, shopCanvas, settingsCanvas;

    // Use this for initialization
    void Start() {
        GameObjectFind();
    }

    // Update is called once per frame
    void Update() {

    }

    private void GameObjectFind()
    {

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
