using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchiviementsController : MonoBehaviour {
    public Text[] achvhName;
    public Text[] achvDescrpt;
    private int currentLv;

    private float totalDistance;
    private int bonusCoinCounter;
    private int deathCounter;
    private int continueCounter;
    private int diamondsUsed;
    private int achvCompleted;
    private int coinsUsed;

	public void Start () {
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
                break;
            case 2:
                achvhName[0].text = "Climber II";
                achvDescrpt[0].text = "reach 100 meters height!";
                break;
            case 3:
                achvhName[0].text = "Climber III";
                achvDescrpt[0].text = "reach 500 meters height!";
                break;
            case 4:
                achvhName[0].text = "Climber IV";
                achvDescrpt[0].text = "reach 1.000 meters height!";
                break;
            case 5:
                achvhName[0].text = "Climber V";
                achvDescrpt[0].text = "reach 2.000 meters height!";
                break;
            case 6:
                achvhName[0].text = "Climber VI";
                achvDescrpt[0].text = "reach 2.000 meters height!";
                break;
            case 7:
                achvhName[0].text = "Climber VII";
                achvDescrpt[0].text = "reach 5.000 meters height!";
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
                break;
            case 2:
                achvhName[1].text = "Traveler II";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/2.000 meters!";
                break;
            case 3:
                achvhName[1].text = "Traveler III";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/5.000 meters!";
                break;
            case 4:
                achvhName[1].text = "Traveler IV";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/10.000 meters!";
                break;
            case 5:
                achvhName[1].text = "Traveler V";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/20.000 meters!";
                break;
            case 6:
                achvhName[1].text = "Traveler VI";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/50.000 meters!";
                break;
            case 7:
                achvhName[1].text = "Traveler VII";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/100.000 meters!";
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
                break;
            case 2:
                achvhName[2].text = "Collector II";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/20 times!";
                break;
            case 3:
                achvhName[2].text = "Collector III";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/50 times!";
                break;
            case 4:
                achvhName[2].text = "Collector IV";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/100 times!";
                break;
            case 5:
                achvhName[2].text = "Collector V";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/200 times!";
                break;
            case 6:
                achvhName[2].text = "Collector VI";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/500 times!";
                break;
            case 7:
                achvhName[2].text = "Collector VII";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/1.000 times!";
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
                break;
            case 2:
                achvhName[3].text = "Death is only the beginning II";
                achvDescrpt[3].text = "Die " + deathCounter + "/20 times!";
                break;
            case 3:
                achvhName[3].text = "Death is only the beginning III";
                achvDescrpt[3].text = "Die " + deathCounter + "/50 times!";
                break;
            case 4:
                achvhName[3].text = "Death is only the beginning IV";
                achvDescrpt[3].text = "Die " + deathCounter + "/100 times!";
                break;
            case 5:
                achvhName[3].text = "Death is only the beginning V";
                achvDescrpt[3].text = "Die " + deathCounter + "/200 times!";
                break;
            case 6:
                achvhName[3].text = "Death is only the beginning VI";
                achvDescrpt[3].text = "Die " + deathCounter + "/500 times!";
                break;
            case 7:
                achvhName[3].text = "Death is only the beginning VII";
                achvDescrpt[3].text = "Die " + deathCounter + "/1.000 times!";
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
                break;
            case 2:
                achvhName[4].text = "One more chance II";
                achvDescrpt[4].text = "Use " + continueCounter + "/5 continue!";
                break;
            case 3:
                achvhName[4].text = "One more chance III";
                achvDescrpt[4].text = "Use " + continueCounter + "/20 continue!";
                break;
            case 4:
                achvhName[4].text = "One more chance IV";
                achvDescrpt[4].text = "Use " + continueCounter + "/50 continue!";
                break;
            case 5:
                achvhName[4].text = "One more chance V";
                achvDescrpt[4].text = "Use " + continueCounter + "/200 continue!";
                break;
            case 6:
                achvhName[4].text = "One more chance VI";
                achvDescrpt[4].text = "Use " + continueCounter + "/500 continue!";
                break;
            case 7:
                achvhName[4].text = "One more chance VII";
                achvDescrpt[4].text = "Use " + continueCounter + "/1.000 continue!";
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
                break;
            case 2:
                achvhName[5].text = "Girl's best friend II";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/50 Diamonds!";
                break;
            case 3:
                achvhName[5].text = "Girl's best friend III";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/100 Diamonds!";
                break;
            case 4:
                achvhName[5].text = "Girl's best friend IV";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/500 Diamonds!";
                break;
            case 5:
                achvhName[5].text = "Girl's best friend V";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/1.000 Diamonds!";
                break;
            case 6:
                achvhName[5].text = "Girl's best friend VI";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/10.000 Diamonds!";
                break;
            case 7:
                achvhName[5].text = "Girl's best friend VII";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/15.000 Diamonds!";
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
                break;
            case 2:
                achvhName[6].text = "Go-Getter II";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/5 achievementes!";
                break;
            case 3:
                achvhName[6].text = "Go-Getter III";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/10 achievementes!";
                break;
            case 4:
                achvhName[6].text = "Go-Getter IV";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/15 achievementes!";
                break;
            case 5:
                achvhName[6].text = "Go-Getter V";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/20 achievementes!";
                break;
            case 6:
                achvhName[6].text = "Go-Getter VI";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/25 achievementes!";
                break;
            case 7:
                achvhName[6].text = "Go-Getter VII";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/35 achievementes!";
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
                break;
            case 2:
                achvhName[7].text = "Spendthrift II";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/1.000 coins!";
                break;
            case 3:
                achvhName[7].text = "Spendthrift III";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/5.000 coins!";
                break;
            case 4:
                achvhName[7].text = "Spendthrift IV";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/15.000 coins!";
                break;
            case 5:
                achvhName[7].text = "Spendthrift V";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/30.000 coins!";
                break;
            case 6:
                achvhName[7].text = "Spendthrift VI";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/50.000 coins!";
                break;
            case 7:
                achvhName[7].text = "Spendthrift VII";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/100.000 coins!";
                break;
            default:
                break;
        }
    }
}
