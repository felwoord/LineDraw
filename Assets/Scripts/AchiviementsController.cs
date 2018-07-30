using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchiviementsController : MonoBehaviour {
    public Text[] achvhName;
    public Text[] achvDescrpt;
    public Button[] getReward;
    private int currentLv;

    private float totalDistance;
    private int bonusCoinCounter;
    private int deathCounter;
    private int continueCounter;
    private int diamondsUsed;
    private int achvCompleted;
    private int coinsUsed;

    private MenuController menuCont;

    public GameObject achvNot;

	public void Start () {
        menuCont = GameObject.Find("Main Camera").GetComponent<MenuController>();

        totalDistance = PlayerPrefs.GetFloat("TotalDistance", 0);      
        bonusCoinCounter = PlayerPrefs.GetInt("BonusCoinCounter", 0);  
        deathCounter = PlayerPrefs.GetInt("DeathCounter", 0);           
        continueCounter = PlayerPrefs.GetInt("ContinueCounter", 0);     
        diamondsUsed = PlayerPrefs.GetInt("DiamondsUsed", 0);           
        achvCompleted = PlayerPrefs.GetInt("AchvCompleted", 0);
        coinsUsed = PlayerPrefs.GetInt("CoinsUsed", 0);                 

        CheckAchv();

	}


	void Update () {
		
	}
    
    void CheckAchv()
    {
        currentLv = PlayerPrefs.GetInt("Lv0", 1);               //Reach x Height
        switch (currentLv)
        {
            case 1:
                achvhName[0].text = "Climber I";
                achvDescrpt[0].text = "reach 50 meters height!";
                if(PlayerPrefs.GetFloat("TopHeight", 0) >= 50){
                    getReward[0].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[0].text = "Climber II";
                achvDescrpt[0].text = "reach 100 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 100)
                {
                    getReward[0].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[0].text = "Climber III";
                achvDescrpt[0].text = "reach 250 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 250)
                {
                    getReward[0].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[0].text = "Climber IV";
                achvDescrpt[0].text = "reach 500 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 500)
                {
                    getReward[0].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[0].text = "Climber V";
                achvDescrpt[0].text = "reach 1.000 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 1000)
                {
                    getReward[0].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[0].text = "Climber VI";
                achvDescrpt[0].text = "reach 2.000 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 2000)
                {
                    getReward[0].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[0].text = "Climber VII";
                achvDescrpt[0].text = "reach 5.000 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 5000)
                {
                    getReward[0].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            default:
                break;
        }

        currentLv = PlayerPrefs.GetInt("Lv1", 1);               //Total Distance Traveled
        switch (currentLv)
        {
            case 1:
                achvhName[1].text = "Traveler I";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/1.000 meters!";
                if(totalDistance >= 1000)
                {
                    getReward[1].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[1].text = "Traveler II";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/2.000 meters!";
                if (totalDistance >= 2000)
                {
                    getReward[1].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[1].text = "Traveler III";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/5.000 meters!";
                if (totalDistance >= 5000)
                {
                    getReward[1].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[1].text = "Traveler IV";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/10.000 meters!";
                if (totalDistance >= 10000)
                {
                    getReward[1].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[1].text = "Traveler V";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/20.000 meters!";
                if (totalDistance >= 20000)
                {
                    getReward[1].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[1].text = "Traveler VI";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/50.000 meters!";
                if (totalDistance >= 50000)
                {
                    getReward[1].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[1].text = "Traveler VII";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/100.000 meters!";
                if (totalDistance >= 100000)
                {
                    getReward[1].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            default:
                break;
        }

        currentLv = PlayerPrefs.GetInt("Lv2", 1);               //Total Bonus Coins Collected
        switch (currentLv)
        {
            case 1:
                achvhName[2].text = "Collector I";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/10 times!";
                if (bonusCoinCounter >= 10)
                {
                    getReward[2].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[2].text = "Collector II";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/20 times!";
                if (bonusCoinCounter >= 20)
                {
                    getReward[2].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[2].text = "Collector III";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/50 times!";
                if (bonusCoinCounter >= 50)
                {
                    getReward[2].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[2].text = "Collector IV";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/100 times!";
                if (bonusCoinCounter >= 100)
                {
                    getReward[2].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[2].text = "Collector V";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/200 times!";
                if (bonusCoinCounter >= 200)
                {
                    getReward[2].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[2].text = "Collector VI";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/500 times!";
                if (bonusCoinCounter >= 500)
                {
                    getReward[2].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[2].text = "Collector VII";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/1.000 times!";
                if (bonusCoinCounter >= 1000)
                {
                    getReward[2].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            default:
                break;
        }

        currentLv = PlayerPrefs.GetInt("Lv3", 1);               //Total Deaths Counter
        switch (currentLv)
        {
            case 1:
                achvhName[3].text = "Death is only the beginning I";
                achvDescrpt[3].text = "Die " + deathCounter + "/10 times!";
                if (deathCounter >= 10)
                {
                    getReward[3].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[3].text = "Death is only the beginning II";
                achvDescrpt[3].text = "Die " + deathCounter + "/20 times!";
                if (deathCounter >= 20)
                {
                    getReward[3].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[3].text = "Death is only the beginning III";
                achvDescrpt[3].text = "Die " + deathCounter + "/50 times!";
                if (deathCounter >= 50)
                {
                    getReward[3].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[3].text = "Death is only the beginning IV";
                achvDescrpt[3].text = "Die " + deathCounter + "/100 times!";
                if (deathCounter >= 100)
                {
                    getReward[3].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[3].text = "Death is only the beginning V";
                achvDescrpt[3].text = "Die " + deathCounter + "/200 times!";
                if (deathCounter >= 200)
                {
                    getReward[3].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[3].text = "Death is only the beginning VI";
                achvDescrpt[3].text = "Die " + deathCounter + "/500 times!";
                if (deathCounter >= 500)
                {
                    getReward[3].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[3].text = "Death is only the beginning VII";
                achvDescrpt[3].text = "Die " + deathCounter + "/1.000 times!";
                if (deathCounter >= 1000)
                {
                    getReward[3].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            default:
                break;
        }

        currentLv = PlayerPrefs.GetInt("Lv4", 1);               //Continues Counter
        switch (currentLv)
        {
            case 1:
                achvhName[4].text = "One more chance I";
                achvDescrpt[4].text = "Use " + continueCounter + "/1 continue!";
                if (continueCounter >= 1)
                {
                    getReward[4].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[4].text = "One more chance II";
                achvDescrpt[4].text = "Use " + continueCounter + "/5 continue!";
                if (continueCounter >= 5)
                {
                    getReward[4].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[4].text = "One more chance III";
                achvDescrpt[4].text = "Use " + continueCounter + "/20 continue!";
                if (continueCounter >= 20)
                {
                    getReward[4].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[4].text = "One more chance IV";
                achvDescrpt[4].text = "Use " + continueCounter + "/50 continue!";
                if (continueCounter >= 50)
                {
                    getReward[4].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[4].text = "One more chance V";
                achvDescrpt[4].text = "Use " + continueCounter + "/200 continue!";
                if (continueCounter >= 200)
                {
                    getReward[4].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[4].text = "One more chance VI";
                achvDescrpt[4].text = "Use " + continueCounter + "/500 continue!";
                if (continueCounter >= 500)
                {
                    getReward[4].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[4].text = "One more chance VII";
                achvDescrpt[4].text = "Use " + continueCounter + "/1.000 continue!";
                if (continueCounter >= 1000)
                {
                    getReward[4].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            default:
                break;
        }

        currentLv = PlayerPrefs.GetInt("Lv5", 1);               //Dimonds Used
        switch (currentLv)
        {
            case 1:
                achvhName[5].text = "Girl's best friend I";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/10 Diamonds!";
                if (diamondsUsed >= 10)
                {
                    getReward[5].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[5].text = "Girl's best friend II";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/50 Diamonds!";
                if (diamondsUsed >= 50)
                {
                    getReward[5].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[5].text = "Girl's best friend III";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/100 Diamonds!";
                if (diamondsUsed >= 100)
                {
                    getReward[5].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[5].text = "Girl's best friend IV";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/500 Diamonds!";
                if (diamondsUsed >= 500)
                {
                    getReward[5].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[5].text = "Girl's best friend V";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/1.000 Diamonds!";
                if (diamondsUsed >= 1000)
                {
                    getReward[5].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[5].text = "Girl's best friend VI";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/10.000 Diamonds!";
                if (diamondsUsed >= 10000)
                {
                    getReward[5].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[5].text = "Girl's best friend VII";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/15.000 Diamonds!";
                if (diamondsUsed >= 15000)
                {
                    getReward[5].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            default:
                break;
        }

        currentLv = PlayerPrefs.GetInt("Lv6", 1);               //Achv Completed
        switch (currentLv)
        {
            case 1:
                achvhName[6].text = "Go-Getter I";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/1 achievementes!";
                if (achvCompleted >= 1)
                {
                    getReward[6].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[6].text = "Go-Getter II";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/5 achievementes!";
                if (achvCompleted >= 5)
                {
                    getReward[6].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[6].text = "Go-Getter III";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/10 achievementes!";
                if (achvCompleted >= 10)
                {
                    getReward[6].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[6].text = "Go-Getter IV";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/15 achievementes!";
                if (achvCompleted >= 15)
                {
                    getReward[6].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[6].text = "Go-Getter V";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/20 achievementes!";
                if (achvCompleted >= 20)
                {
                    getReward[6].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[6].text = "Go-Getter VI";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/25 achievementes!";
                if (achvCompleted >= 25)
                {
                    getReward[6].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[6].text = "Go-Getter VII";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/35 achievementes!";
                if (achvCompleted >= 35)
                {
                    getReward[6].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            default:
                break;
        }

        currentLv = PlayerPrefs.GetInt("Lv7", 1);               //Coins Used
        switch (currentLv)
        {
            case 1:
                achvhName[7].text = "Spendthrift I";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/500 coins!";
                if (achvCompleted >= 500)
                {
                    getReward[7].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[7].text = "Spendthrift II";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/1.000 coins!";
                if (achvCompleted >= 1000)
                {
                    getReward[7].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[7].text = "Spendthrift III";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/5.000 coins!";
                if (achvCompleted >= 5000)
                {
                    getReward[7].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[7].text = "Spendthrift IV";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/15.000 coins!";
                if (achvCompleted >= 15000)
                {
                    getReward[7].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[7].text = "Spendthrift V";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/30.000 coins!";
                if (achvCompleted >= 30000)
                {
                    getReward[7].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[7].text = "Spendthrift VI";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/50.000 coins!";
                if (achvCompleted >= 50000)
                {
                    getReward[7].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[7].text = "Spendthrift VII";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/100.000 coins!";
                if (achvCompleted >= 100000)
                {
                    getReward[7].interactable = true;
                    achvNot.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    public void GetReward(int currentAchv)
    {
        currentLv = PlayerPrefs.GetInt("Lv" + currentAchv, 1);

        switch (currentLv)
        {
            case 1:
                menuCont.BuyDiamond(1);
                break;
            case 2:
                menuCont.BuyDiamond(5);
                break;
            case 3:
                menuCont.BuyDiamond(15);
                break;
            case 4:
                menuCont.BuyDiamond(50);
                break;
            case 5:
                menuCont.BuyDiamond(100);
                break;
            case 6:
                menuCont.BuyDiamond(200);
                break;
            case 7:
                menuCont.BuyDiamond(500);
                break;
            default:
                break;
        }
        getReward[currentAchv].interactable = false;
        PlayerPrefs.SetInt("Lv" + currentAchv, currentLv + 1);

        achvCompleted = PlayerPrefs.GetInt("AchvCompleted", 0);
        achvCompleted++;
        PlayerPrefs.SetInt("AchvCompleted", achvCompleted);

        achvNot.SetActive(false);

        Start();
    }
}
