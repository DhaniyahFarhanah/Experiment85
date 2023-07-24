using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// showcases all the data

public class ShowAnalytics : MonoBehaviour
{ //I hate myself for this but its 2.30am and i am too lazy to think of a better way to do this. Sorry JY :( -nana
    [SerializeField] GameObject mostHitGM;
    [SerializeField] GameObject mostbuffGM;

    [SerializeField] Animator slimeAnimator;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] TMP_Text slimeName;
    [SerializeField] TMP_Text slimeType;

    [SerializeField] TMP_Text win;
    [SerializeField] TMP_Text health;
    [SerializeField] TMP_Text damage;
    [SerializeField] TMP_Text speed;
    [SerializeField] TMP_Text shotspeed;
    [SerializeField] TMP_Text range;
    [SerializeField] TMP_Text slimeRate;

    [SerializeField] TMP_Text accuracy;
    [SerializeField] TMP_Text shotHit;
    [SerializeField] TMP_Text shotMissed;

    [SerializeField] TMP_Text lastHitEnemy;
    [SerializeField] Animator lastHitAnimator;

    [SerializeField] TMP_Text timeTaken;
    [SerializeField] TMP_Text waveEnded;
    [SerializeField] TMP_Text enemiesDefeated;
    [SerializeField] TMP_Text damageDealt;
    [SerializeField] TMP_Text damageReceived;

    [SerializeField] TMP_Text hitTimes;
    [SerializeField] TMP_Text hazmatHit;
    [SerializeField] TMP_Text chaserHit;
    [SerializeField] TMP_Text juggHit;
    [SerializeField] TMP_Text reaperHit;

    [SerializeField] TMP_Text hazmatDetails;
    [SerializeField] TMP_Text chaserDetails;
    [SerializeField] TMP_Text juggDetails;
    [SerializeField] TMP_Text reaperDetails;
    [SerializeField] TMP_Text mostHitName;
    [SerializeField] TMP_Text numHitTimes;

    [SerializeField] Image hazmat;
    [SerializeField] Image chaser;
    [SerializeField] Image jugg;
    [SerializeField] Image reaper;
    [SerializeField] Image mostHit;
    [SerializeField] Image hpImage;
    [SerializeField] Image slimerateImage;
    [SerializeField] Image damageImage;
    [SerializeField] Image shotSpeedImage;
    [SerializeField] Image mostBuffImage;

    [SerializeField] TMP_Text buffPicked;
    [SerializeField] TMP_Text buff1Details;
    [SerializeField] TMP_Text buff2Details;
    [SerializeField] TMP_Text buff3Details;
    [SerializeField] TMP_Text buff4Details;
    [SerializeField] TMP_Text mostBuffName;
    [SerializeField] TMP_Text numBuffPicked;

    private List<CharacterClass> characterJSONList;
    private List<EnemyClass> enemyJSONlist;
    private List<BuffClass> buffJSONList;
    private bool fillOnce;


    // Start is called before the first frame update
    void Start()
    {
        fillOnce = true;
        characterJSONList = GameData.GetCharacterList();
        enemyJSONlist = GameData.GetEnemyList();
        buffJSONList = GameData.GetBuffList();

        waveEnded.color = Color.white;
        damageDealt.color = Color.white;
        enemiesDefeated.color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        //so it doesnt overload system lol
        if (fillOnce)
        {
            SetValues();
            fillOnce = false;

            if(AnalyticsHolder.Instance.mostHitId == "")
            {
                mostHitGM.SetActive(false);
            }
            else if(AnalyticsHolder.Instance.mostTakenBuff == "")
            {
                mostbuffGM.SetActive(false);
            }
            else
            {
                mostHitGM.SetActive(true);
                mostbuffGM.SetActive(true);
            }
        }
    }

    void SetValues()
    {

        GetSlimeName();

        if (AnalyticsHolder.Instance.win)
        {
            win.text = "Successful Escape";
        }
        else if (!AnalyticsHolder.Instance.win)
        {
            win.text = "Attempt Failed :C";
        }

        health.text = "Health: " + AnalyticsHolder.Instance.health.ToString();
        damage.text = "Damage: " + AnalyticsHolder.Instance.damage.ToString("F2");
        speed.text = "Speed: " + AnalyticsHolder.Instance.speed.ToString("F2");
        shotspeed.text = "Shot Speed: " + AnalyticsHolder.Instance.shotSpeed.ToString("F2");
        range.text = "Range: " + AnalyticsHolder.Instance.range.ToString("F2");
        slimeRate.text = "Slime Rate: " + AnalyticsHolder.Instance.slimeRate.ToString("F2");

        accuracy.text = AnalyticsHolder.Instance.accuracy.ToString("F2") + "% Accuracy";
        shotHit.text = AnalyticsHolder.Instance.shotsHit.ToString() + "/" + AnalyticsHolder.Instance.totalShots.ToString() + " shots hit";
        shotMissed.text = AnalyticsHolder.Instance.shotsMissed.ToString() + " shots missed"; 

        timeTaken.text = "Time Spent: " + AnalyticsHolder.Instance.timeTaken.ToString();
        waveEnded.text = "Ended on Wave " + AnalyticsHolder.Instance.waveEnd.ToString();
        enemiesDefeated.text = AnalyticsHolder.Instance.enemiesDefeated + "/" + AnalyticsHolder.Instance.numEnemiesSpawned.ToString() + " Enemies Defeated";
        damageReceived.text = "Total Damage Received: " + AnalyticsHolder.Instance.damageReceived.ToString();
        damageDealt.text = "Total Damage Dealt: " + AnalyticsHolder.Instance.damageDealt.ToString();

        GetKilledBy();

        hitTimes.text = "Hit " + AnalyticsHolder.Instance.hitsTaken.ToString() + " times";
        hazmatHit.text = AnalyticsHolder.Instance.hitByEnemy1.ToString() + " time(s) by HAZMAT"; 
        chaserHit.text = AnalyticsHolder.Instance.hitByEnemy2.ToString() + " time(s) by CHASER";
        juggHit.text = AnalyticsHolder.Instance.hitByEnemy3.ToString() + " time(s) by JUGGARNAUT";
        reaperHit.text = AnalyticsHolder.Instance.hitByEnemy4.ToString() + " time(s) by REAPER";

        hazmatDetails.text = AnalyticsHolder.Instance.killedNumOfEnemy1.ToString() + "/" + AnalyticsHolder.Instance.numOfEnemy1Spawned.ToString() + " killed";
        chaserDetails.text = AnalyticsHolder.Instance.killedNumOfEnemy2.ToString() + "/" + AnalyticsHolder.Instance.numOfEnemy2Spawned.ToString() + " killed";
        juggDetails.text = AnalyticsHolder.Instance.killedNumOfEnemy3.ToString() + "/" + AnalyticsHolder.Instance.numOfEnemy3Spawned.ToString() + " killed";
        reaperDetails.text = AnalyticsHolder.Instance.killedNumOfEnemy4.ToString() + "/" + AnalyticsHolder.Instance.numOfEnemy4Spawned.ToString() + " killed";

        GetMostHit();

        buffPicked.text = AnalyticsHolder.Instance.buffsPicked.ToString() + "/" + AnalyticsHolder.Instance.buffsDropped.ToString() + " buffs picked";
        buff1Details.text = AnalyticsHolder.Instance.takenNumOfBuff1.ToString() + "/" + AnalyticsHolder.Instance.numOfBuff1Spawned.ToString() + " picked up";
        buff2Details.text = AnalyticsHolder.Instance.takenNumOfBuff2.ToString() + "/" + AnalyticsHolder.Instance.numOfBuff2Spawned.ToString() + " picked up";
        buff3Details.text = AnalyticsHolder.Instance.takenNumOfBuff3.ToString() + "/" + AnalyticsHolder.Instance.numOfBuff3Spawned.ToString() + " picked up";
        buff4Details.text = AnalyticsHolder.Instance.takenNumOfBuff4.ToString() + "/" + AnalyticsHolder.Instance.numOfBuff4Spawned.ToString() + " picked up";

        GetMostBuff();

        SetSprites();

        CheckHighscores();
    }

    void GetSlimeName()
    {
        foreach(CharacterClass c in characterJSONList)
        {
            if(c.charId == AnalyticsHolder.Instance.slimeChosen)
            {
                slimeName.text = c.charName;
                slimeType.text = c.type;
            }
        }
    }

    void GetMostHit()
    {
        foreach(EnemyClass e in enemyJSONlist)
        {
            if(e.enemyId == AnalyticsHolder.Instance.mostHitId)
            {
                mostHitName.text = e.enemyName;

            }
        }

        switch(AnalyticsHolder.Instance.mostHitId)
        {
            case "E01": numHitTimes.text = "Hit " + AnalyticsHolder.Instance.hitByEnemy1.ToString() + " Times"; break;
            case "E02": numHitTimes.text = "Hit " + AnalyticsHolder.Instance.hitByEnemy2.ToString() + " Times"; break;
            case "E03": numHitTimes.text = "Hit " + AnalyticsHolder.Instance.hitByEnemy3.ToString() + " Times"; break;
            case "E04": numHitTimes.text = "Hit " + AnalyticsHolder.Instance.hitByEnemy4.ToString() + " Times"; break;
            default: numHitTimes.text = "Unidentified"; break;
        }
    }
    void GetKilledBy()
    {
        foreach (EnemyClass e in enemyJSONlist)
        {
            if (e.enemyId == AnalyticsHolder.Instance.mostHitId)
            {
                lastHitEnemy.text = e.enemyName;

            }
        }
    }

    void GetMostBuff() 
    {
        foreach (BuffClass b in buffJSONList)
        {
            if (b.buffId == AnalyticsHolder.Instance.mostTakenBuff)
            {
                mostBuffName.text = b.buffName;

            }
        }

        switch (AnalyticsHolder.Instance.mostTakenBuff)
        {
            case "buff1": numBuffPicked.text = "Picked Up " + AnalyticsHolder.Instance.takenNumOfBuff1.ToString() + " Times"; mostBuffImage.color = Color.green; break;
            case "buff2": numBuffPicked.text = "Picked Up " + AnalyticsHolder.Instance.takenNumOfBuff2.ToString() + " Times"; mostBuffImage.color = Color.blue; break;
            case "buff3": numBuffPicked.text = "Picked Up " + AnalyticsHolder.Instance.takenNumOfBuff3.ToString() + " Times"; mostBuffImage.color = Color.red; break;
            case "buff4": numBuffPicked.text = "Picked Up " + AnalyticsHolder.Instance.takenNumOfBuff4.ToString() + " Times"; mostBuffImage.color = Color.yellow; break;
            default: numBuffPicked.text = "Unidentified"; break;
        }
        
    }

    void SetSprites()
    {
        switch (AnalyticsHolder.Instance.slimeChosen)
        {
            case "S01": slimeAnimator.SetInteger("id", 0); break;
            case "S02": slimeAnimator.SetInteger("id", 1); break;
            case "S03": slimeAnimator.SetInteger("id", 2); break;
            case "S04": slimeAnimator.SetInteger("id", 3); break;
        }

        switch (AnalyticsHolder.Instance.mostHitId)
        {
            case "E01": enemyAnimator.SetInteger("index", 0); break;
            case "E02": enemyAnimator.SetInteger("index", 1); break;
            case "E03": enemyAnimator.SetInteger("index", 2); break;
            case "E04": enemyAnimator.SetInteger("index", 3); break;
        }

        switch (AnalyticsHolder.Instance.killedBy)
        {
            case "E01": enemyAnimator.SetInteger("index", 0); break;
            case "E02": enemyAnimator.SetInteger("index", 1); break;
            case "E03": enemyAnimator.SetInteger("index", 2); break;
            case "E04": enemyAnimator.SetInteger("index", 3); break;
        }

        switch (AnalyticsHolder.Instance.killedBy)
        {
            case "E01": lastHitAnimator.SetInteger("index", 0); break;
            case "E02": lastHitAnimator.SetInteger("index", 1); break;
            case "E03": lastHitAnimator.SetInteger("index", 2); break;
            case "E04": lastHitAnimator.SetInteger("index", 3); break;
        }

        hpImage.color = Color.green;
        slimerateImage.color = Color.blue;
        damageImage.color = Color.red;
        shotSpeedImage.color = Color.yellow;


    }

    void CheckHighscores()
    {
        if(AnalyticsHolder.Instance.waveEnd > GameClass.GetWaveHighscore())
        {
            GameClass.SetWaveHighscore(AnalyticsHolder.Instance.waveEnd);
            waveEnded.text = "(NEW!) " + waveEnded.text;
            waveEnded.color = Color.yellow;
        }

        if(AnalyticsHolder.Instance.enemiesDefeated > GameClass.GetEnemiesDefeatedHighscore())
        {
            GameClass.SetEnemiesHighscore(AnalyticsHolder.Instance.enemiesDefeated);
            enemiesDefeated.text = "(NEW!) " + enemiesDefeated.text;
            enemiesDefeated.color = Color.yellow;
        }

        if(AnalyticsHolder.Instance.damageDealt > GameClass.GetDamageHighscore())
        {
            GameClass.SetDamageHighscore(AnalyticsHolder.Instance.damageDealt);
            damageDealt.text = "(NEW!) " + damageDealt.text;
            damageDealt.color = Color.yellow;
        }
    }

}
