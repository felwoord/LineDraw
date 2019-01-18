using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchiviementsController : MonoBehaviour
{
    public Text[] achvhName;
    public Text[] achvDescrpt;
    //public Button[] getReward;
    public GameObject[] getRewardGO;
    public Transform[] achvTransform;
    private int currentLv;

    private float totalDistance;
    private int bonusCoinCounter;
    private int deathCounter;
    private int continueCounter;
    private int diamondsUsed;
    private int achvCompleted;
    private int coinsUsed;
    private int nerdBoy;
    private int adsWatched;

    private MenuController menuCont;

    public GameObject achvNot;

    public void Start()
    {
        menuCont = GameObject.Find("Main Camera").GetComponent<MenuController>();

        totalDistance = PlayerPrefs.GetFloat("TotalDistance", 0);
        bonusCoinCounter = PlayerPrefs.GetInt("BonusCoinCounter", 0);
        deathCounter = PlayerPrefs.GetInt("DeathCounter", 0);
        continueCounter = PlayerPrefs.GetInt("ContinueCounter", 0);
        diamondsUsed = PlayerPrefs.GetInt("DiamondsUsed", 0);
        achvCompleted = PlayerPrefs.GetInt("AchvCompleted", 0);
        coinsUsed = PlayerPrefs.GetInt("CoinsUsed", 0);
        nerdBoy = PlayerPrefs.GetInt("NerdBoy", 0);
        adsWatched = PlayerPrefs.GetInt("AdsWatched", 0);

        CheckAchv();

    }


    void Update()
    {

    }

    void CheckAchv()
    {
        currentLv = PlayerPrefs.GetInt("Lv0", 1);               //Reach x Height
        switch (currentLv)
        {
            case 1:
                achvhName[0].text = "Climber I";
                achvDescrpt[0].text = "Reach 50 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 50)
                {
                    getRewardGO[0].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[0].text = "Climber II";
                achvDescrpt[0].text = "Reach 100 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 100)
                {
                    getRewardGO[0].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[0].text = "Climber III";
                achvDescrpt[0].text = "Reach 250 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 250)
                {
                    getRewardGO[0].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[0].text = "Climber IV";
                achvDescrpt[0].text = "Reach 500 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 500)
                {
                    getRewardGO[0].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[0].text = "Climber V";
                achvDescrpt[0].text = "Reach 1.000 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 1000)
                {
                    getRewardGO[0].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[0].text = "Climber VI";
                achvDescrpt[0].text = "Reach 2.000 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 2000)
                {
                    getRewardGO[0].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[0].text = "Climber VII";
                achvDescrpt[0].text = "Reach 5.000 meters height!";
                if (PlayerPrefs.GetFloat("TopHeight", 0) >= 5000)
                {
                    getRewardGO[0].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[0].text = "Climber";
                achvDescrpt[0].text = "Completed";
                achvTransform[0].SetAsLastSibling();
                getRewardGO[0].SetActive(false);
                break;
        }

        currentLv = PlayerPrefs.GetInt("Lv1", 1);               //Total Distance Traveled
        switch (currentLv)
        {
            case 1:
                achvhName[1].text = "Traveler I";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/1.000 meters!";
                if (totalDistance >= 1000)
                {
                    getRewardGO[1].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[1].text = "Traveler II";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/2.000 meters!";
                if (totalDistance >= 2000)
                {
                    getRewardGO[1].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[1].text = "Traveler III";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/5.000 meters!";
                if (totalDistance >= 5000)
                {
                    getRewardGO[1].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[1].text = "Traveler IV";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/10.000 meters!";
                if (totalDistance >= 10000)
                {
                    getRewardGO[1].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[1].text = "Traveler V";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/20.000 meters!";
                if (totalDistance >= 20000)
                {
                    getRewardGO[1].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[1].text = "Traveler VI";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/50.000 meters!";
                if (totalDistance >= 50000)
                {
                    getRewardGO[1].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[1].text = "Traveler VII";
                achvDescrpt[1].text = "Travel " + totalDistance.ToString("0") + "/100.000 meters!";
                if (totalDistance >= 100000)
                {
                    getRewardGO[1].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[1].text = "Traveler";
                achvDescrpt[1].text = "Completed! (Current total: " + totalDistance.ToString("0") + " meters!)";
                achvTransform[1].SetAsLastSibling();
                getRewardGO[1].SetActive(false);
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
                    getRewardGO[2].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[2].text = "Collector II";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/20 times!";
                if (bonusCoinCounter >= 20)
                {
                    getRewardGO[2].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[2].text = "Collector III";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/50 times!";
                if (bonusCoinCounter >= 50)
                {
                    getRewardGO[2].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[2].text = "Collector IV";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/100 times!";
                if (bonusCoinCounter >= 100)
                {
                    getRewardGO[2].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[2].text = "Collector V";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/200 times!";
                if (bonusCoinCounter >= 200)
                {
                    getRewardGO[2].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[2].text = "Collector VI";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/500 times!";
                if (bonusCoinCounter >= 500)
                {
                    getRewardGO[2].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[2].text = "Collector VII";
                achvDescrpt[2].text = "Collect all coins from the Set " + bonusCoinCounter + "/1.000 times!";
                if (bonusCoinCounter >= 1000)
                {
                    getRewardGO[2].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[2].text = "Collector";
                achvDescrpt[2].text = "Completed! (Current total: " + bonusCoinCounter + " times!)";
                achvTransform[2].SetAsLastSibling();
                getRewardGO[2].SetActive(false);
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
                    getRewardGO[3].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[3].text = "Death is only the beginning II";
                achvDescrpt[3].text = "Die " + deathCounter + "/20 times!";
                if (deathCounter >= 20)
                {
                    getRewardGO[3].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[3].text = "Death is only the beginning III";
                achvDescrpt[3].text = "Die " + deathCounter + "/50 times!";
                if (deathCounter >= 50)
                {
                    getRewardGO[3].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[3].text = "Death is only the beginning IV";
                achvDescrpt[3].text = "Die " + deathCounter + "/100 times!";
                if (deathCounter >= 100)
                {
                    getRewardGO[3].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[3].text = "Death is only the beginning V";
                achvDescrpt[3].text = "Die " + deathCounter + "/200 times!";
                if (deathCounter >= 200)
                {
                    getRewardGO[3].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[3].text = "Death is only the beginning VI";
                achvDescrpt[3].text = "Die " + deathCounter + "/500 times!";
                if (deathCounter >= 500)
                {
                    getRewardGO[3].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[3].text = "Death is only the beginning VII";
                achvDescrpt[3].text = "Die " + deathCounter + "/1.000 times!";
                if (deathCounter >= 1000)
                {
                    getRewardGO[3].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[3].text = "Death is only the beginning";
                achvDescrpt[3].text = "Completed! (Current total: " + deathCounter + " deaths!)";
                achvTransform[3].SetAsLastSibling();
                getRewardGO[3].SetActive(false);
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
                    getRewardGO[4].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[4].text = "One more chance II";
                achvDescrpt[4].text = "Use " + continueCounter + "/5 continue!";
                if (continueCounter >= 5)
                {
                    getRewardGO[4].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[4].text = "One more chance III";
                achvDescrpt[4].text = "Use " + continueCounter + "/20 continue!";
                if (continueCounter >= 20)
                {
                    getRewardGO[4].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[4].text = "One more chance IV";
                achvDescrpt[4].text = "Use " + continueCounter + "/50 continue!";
                if (continueCounter >= 50)
                {
                    getRewardGO[4].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[4].text = "One more chance V";
                achvDescrpt[4].text = "Use " + continueCounter + "/200 continue!";
                if (continueCounter >= 200)
                {
                    getRewardGO[4].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[4].text = "One more chance VI";
                achvDescrpt[4].text = "Use " + continueCounter + "/500 continue!";
                if (continueCounter >= 500)
                {
                    getRewardGO[4].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[4].text = "One more chance VII";
                achvDescrpt[4].text = "Use " + continueCounter + "/1.000 continue!";
                if (continueCounter >= 1000)
                {
                    getRewardGO[4].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[4].text = "One more chance";
                achvDescrpt[4].text = "Completed! (Current total: " + continueCounter + " continues!)";
                achvTransform[4].SetAsLastSibling();
                getRewardGO[4].SetActive(false);
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
                    getRewardGO[5].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[5].text = "Girl's best friend II";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/50 Diamonds!";
                if (diamondsUsed >= 50)
                {
                    getRewardGO[5].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[5].text = "Girl's best friend III";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/100 Diamonds!";
                if (diamondsUsed >= 100)
                {
                    getRewardGO[5].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[5].text = "Girl's best friend IV";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/500 Diamonds!";
                if (diamondsUsed >= 500)
                {
                    getRewardGO[5].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[5].text = "Girl's best friend V";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/1.000 Diamonds!";
                if (diamondsUsed >= 1000)
                {
                    getRewardGO[5].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[5].text = "Girl's best friend VI";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/10.000 Diamonds!";
                if (diamondsUsed >= 10000)
                {
                    getRewardGO[5].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[5].text = "Girl's best friend VII";
                achvDescrpt[5].text = "Use " + diamondsUsed + "/15.000 Diamonds!";
                if (diamondsUsed >= 15000)
                {
                    getRewardGO[5].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[5].text = "Girl's best friend";
                achvDescrpt[5].text = "Completed! (Current total: " + diamondsUsed + " Diamonds!)";
                achvTransform[5].SetAsLastSibling();
                getRewardGO[5].SetActive(false);
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
                    getRewardGO[6].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[6].text = "Go-Getter II";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/5 achievementes!";
                if (achvCompleted >= 5)
                {
                    getRewardGO[6].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[6].text = "Go-Getter III";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/10 achievementes!";
                if (achvCompleted >= 10)
                {
                    getRewardGO[6].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[6].text = "Go-Getter IV";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/15 achievementes!";
                if (achvCompleted >= 15)
                {
                    getRewardGO[6].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[6].text = "Go-Getter V";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/20 achievementes!";
                if (achvCompleted >= 20)
                {
                    getRewardGO[6].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[6].text = "Go-Getter VI";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/25 achievementes!";
                if (achvCompleted >= 25)
                {
                    getRewardGO[6].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[6].text = "Go-Getter VII";
                achvDescrpt[6].text = "Complete " + achvCompleted + "/35 achievementes!";
                if (achvCompleted >= 35)
                {
                    getRewardGO[6].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[6].text = "Go-Getter";
                achvDescrpt[6].text = "Completed! (Current total: " + achvCompleted + " achievements!)";
                achvTransform[6].SetAsLastSibling();
                getRewardGO[6].SetActive(false);
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
                    getRewardGO[7].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 2:
                achvhName[7].text = "Spendthrift II";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/1.000 coins!";
                if (achvCompleted >= 1000)
                {
                    getRewardGO[7].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 3:
                achvhName[7].text = "Spendthrift III";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/5.000 coins!";
                if (achvCompleted >= 5000)
                {
                    getRewardGO[7].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 4:
                achvhName[7].text = "Spendthrift IV";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/15.000 coins!";
                if (achvCompleted >= 15000)
                {
                    getRewardGO[7].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 5:
                achvhName[7].text = "Spendthrift V";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/30.000 coins!";
                if (achvCompleted >= 30000)
                {
                    getRewardGO[7].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 6:
                achvhName[7].text = "Spendthrift VI";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/50.000 coins!";
                if (achvCompleted >= 50000)
                {
                    getRewardGO[7].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            case 7:
                achvhName[7].text = "Spendthrift VII";
                achvDescrpt[7].text = "Spend " + coinsUsed + "/100.000 coins!";
                if (achvCompleted >= 100000)
                {
                    getRewardGO[7].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[7].text = "Spendthrift";
                achvDescrpt[7].text = "Completed! (Current total: " + coinsUsed + " coins!)";
                achvTransform[7].SetAsLastSibling();
                getRewardGO[7].SetActive(false);
                break;
        }

        currentLv = PlayerPrefs.GetInt("Lv8", 1);               //Nerd Boy
        switch (currentLv)
        {
            case 1:
                achvhName[8].text = "Nerd Boy";
                achvDescrpt[8].text = "Read Help menu";
                if (nerdBoy == 1)
                {
                    getRewardGO[8].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[8].text = "Nerd Boy";
                achvDescrpt[8].text = "Completed";
                achvTransform[8].SetAsLastSibling();
                getRewardGO[8].SetActive(false);
                break;
        }
        currentLv = PlayerPrefs.GetInt("Lv9", 1);               //Nerd Boy
        switch (currentLv)
        {
            case 1:
                achvhName[9].text = "Gotta Make a Living";
                achvDescrpt[9].text = "Choose to watch " + adsWatched + " / 50 ads!";
                if (adsWatched >= 50)
                {
                    getRewardGO[9].SetActive(true);
                    achvNot.SetActive(true);
                }
                break;
            default:
                achvhName[9].text = "Gotta Make a Living";
                achvDescrpt[9].text = "Completed";
                achvTransform[9].SetAsLastSibling();
                getRewardGO[9].SetActive(false);
                break;
        }
    }

    public void GetReward(int currentAchv)
    {
        if (currentAchv == 8)
        {
            menuCont.BuyDiamond(5);
        }
        else if (currentAchv == 9)
        {
            menuCont.OpenReceivedUI(0, 1);
        }
        else
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
        }
        getRewardGO[currentAchv].SetActive(false);
        PlayerPrefs.SetInt("Lv" + currentAchv, currentLv + 1);

        achvCompleted = PlayerPrefs.GetInt("AchvCompleted", 0);
        achvCompleted++;
        PlayerPrefs.SetInt("AchvCompleted", achvCompleted);

        achvNot.SetActive(false);

        Start();
    }
}
