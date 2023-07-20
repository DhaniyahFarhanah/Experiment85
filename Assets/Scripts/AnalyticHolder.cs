using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnalyticHolder
{
    public static string slimeChosen { get; set; }
    public static DateTime dateTimeStamp { get; set; }
    public static string timeElapsedWholeGame { get; set; }

    //====Lab Analysis=====
    public static bool win { get; set; } //records win statement 
    public static int timeTakenMin { get; set; } //records time taken to end game (in min)
    public static int timeTakenSec { get; set; } //records time taken to end game (in sec)
    public static int enemiesDefeated { get; set; } //records num of enemies defeated
    public static int numEnemiesSpawned { get; set; } //takes in num of enemies spawned
    public static int hitsTaken { get; set; } //num of hits taken
    public static int waveEnd { get; set; } //wave the game ended on
    public static int buffsPicked { get; set; } //num of buffs picked
    public static int buffsDropped { get; set; } //num of buffs dropped

    //====player stat end on=====
    public static int health { get; set; }
    public static float damage { get; set; }
    public static float speed { get; set; }
    public static float shotSpeed { get; set; }
    public static float range { get; set; }
    public static float slimeRate { get; set; }

    //====Deeper Analysis=====
    public static int totalShots { get; set; }
    public static int shotsMissed { get; set; }
    public static int shotsHit { get; set; }
    public static int accuracy { get; set; }

    //====Buff stats=====
    public static int numOfBuff1 { get; set; }
    public static int numOfBuff2 { get; set; }
    public static int numOfBuff3 { get; set; }
    public static int numOfBuff4 { get; set; }

    public static int takenNumOfBuff1 { get; set; }
    public static int takenNumOfBuff2 { get; set; }
    public static int takenNumOfBuff3 { get; set; }
    public static int takenNumOfBuff4 { get; set; }
    public static string mostTakenBuff { get; set; }

    //====Enemy Stats=====
    public static int numOfEnemy1Spawned { get; set; }
    public static int numOfEnemy2Spawned { get; set; }
    public static int numOfEnemy3Spawned { get; set; }
    public static int numOfEnemy4Spawned { get; set; }
    public static int killedNumOfEnemy1 { get; set; }
    public static int killedNumOfEnemy2 { get; set; }
    public static int killedNumOfEnemy3 { get; set; }
    public static int killedNumOfEnemy4 { get; set; }

    //====Hit reading=====
    public static int hitByEnemy1 { get; set; }
    public static int hitByEnemy2 { get; set; }
    public static int hitByEnemy3 { get; set; }
    public static int hitByEnemy4 { get; set; }


}
