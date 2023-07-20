using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AnalyticClass
{
    //Generic Game Stuff (dynamic?)
    public static string SlimeChosen; //which slime is chosen
    public static DateTime dateTimeStamp; //(timestamp for when data set
    public static string timeElapsedWholeGame; //time from start to finish of the whole game in "mm:ss"

    public static void SetSlimeChosen(string Id)
    {
        SlimeChosen = Id;

    } 
    public static string GetSlimeChose()
    {
        return SlimeChosen;
    }

    public static void SetDateTimeStamp(DateTime dateTime)
    {
        dateTimeStamp = dateTime;
    }
    public static DateTime GetDateTimeStamp()
    {
        return dateTimeStamp;
    }

    public static void SetTimeElapsedWholeGame(string time)
    {
        timeElapsedWholeGame = time;
    }
    public static string GetTimeElapsedWholeGame()
    {
        return timeElapsedWholeGame;
    }

    //====Lab Analysis=====
    public bool win; //records win statement 
    public int timeTakenMin; //records time taken to end game (in min)
    public int timeTakenSec; //records time taken to end game (in sec)
    public int enemiesDefeated; //records num of enemies defeated
    public int numEnemiesSpawned; //takes in num of enemies spawned
    public int hitsTaken; //num of hits taken
    public int waveEnd; //wave the game ended on
    public int buffsPicked; //num of buffs picked
    public int buffsDropped; //num of buffs dropped

    public void SetWaveDetails(bool Win, int TimeTakenMin, int TimeTakenSec, int EnemiesDefeated, int NumEnemiesSpawned, int HitsTaken, int WavEnd, int BuffsPicked, int BuffsDropped)
    {
        win = Win;
        timeTakenMin = TimeTakenMin;
        timeTakenSec = TimeTakenSec;
        enemiesDefeated = EnemiesDefeated;
        numEnemiesSpawned = NumEnemiesSpawned;
        hitsTaken = HitsTaken;
        waveEnd = WavEnd;
        buffsPicked = BuffsPicked;
        buffsDropped = BuffsDropped;
    }

    //====player stat end on=====
    public int health;
    public float damage;
    public float speed;
    public float shotSpeed;
    public float range;
    public float slimeRate;

    public void SetCharacterEndStats(int Health, float Damage, float Speed, float ShotSpeed,float Range, float SlimeRate)
    {
        health = Health;
        damage = Damage;
        speed = Speed;
        shotSpeed = ShotSpeed;
        range = Range;
        slimeRate = SlimeRate;
    }

    //====Deeper Analysis=====
    public int totalShots;
    public int shotsMissed;
    public int shotsHit;
    public int accuracy;

    public void SetAccuracy(int TotalShots, int ShotsMissed, int ShotsHit, int Accuracy)
    {
        totalShots = TotalShots;
        shotsMissed = ShotsMissed;
        shotsHit = ShotsHit;
        accuracy = Accuracy;
    }

    //Buff stats
    public int numOfBuff1;
    public int numOfBuff2;
    public int numOfBuff3;
    public int numOfBuff4;
    public int takenNumOfBuff1;
    public int takenNumOfBuff2;
    public int takenNumOfBuff3;
    public int takenNumOfBuff4;
    public string mostTakenBuff;

    public void SetBuffAmount(int NumOfBuff1, int NumOfBuff2, int NumOfBuff3, int NumOfBuff4)
    {
        numOfBuff1 = NumOfBuff1;
        numOfBuff2 = NumOfBuff2;
        numOfBuff3 = NumOfBuff3;
        numOfBuff4 = NumOfBuff4;
    }

    public void SetBuffTaken(int TakenNumOfBuff1, int TakenNumOfBuff2, int TakenNumOfBuff3, int TakenNumOfBuff4)
    {
        takenNumOfBuff1 = TakenNumOfBuff1;
        takenNumOfBuff2 = TakenNumOfBuff2;
        takenNumOfBuff3 = TakenNumOfBuff3;
        takenNumOfBuff4 = TakenNumOfBuff4;
    }

    //enemy stats
    public int numOfEnemy1Spawned;
    public int numOfEnemy2Spawned;
    public int numOfEnemy3Spawned;
    public int numOfEnemy4Spawned;
    public int killedNumOfEnemy1;
    public int killedNumOfEnemy2;
    public int killedNumOfEnemy3;
    public int killedNumOfEnemy4;
    public void SetNumOfEnemySpawned(int NumOfEnemy1, int NumOfEnemy2, int NumOfEnemy3, int NumOfEnemy4)
    {
        numOfEnemy1Spawned = NumOfEnemy1;
        numOfEnemy2Spawned = NumOfEnemy2;
        numOfEnemy3Spawned = NumOfEnemy3;
        numOfEnemy4Spawned = NumOfEnemy4;
    }

    public void SetKilledEnemyStat(int NumOfEnemy1, int NumOfEnemy2, int NumOfEnemy3, int NumOfEnemy4)
    {
        killedNumOfEnemy1 = NumOfEnemy1;
        killedNumOfEnemy2 = NumOfEnemy2;
        killedNumOfEnemy3 = NumOfEnemy3;
        killedNumOfEnemy4 = NumOfEnemy4;
    }

    //hit reading
    public int hitByEnemy1;
    public int hitByEnemy2;
    public int hitByEnemy3;
    public int hitByEnemy4;

    public void SetHitEnemy(int HitByEnemy1, int HitByEnemy2, int HitByEnemy3, int HitByEnemy4)
    {
        hitByEnemy1 = HitByEnemy1;
        hitByEnemy2 = HitByEnemy2;
        hitByEnemy3 = HitByEnemy3;
        hitByEnemy4 = HitByEnemy4;
    }


}
