using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// Analytics holder uses a singleton pattern to make the data recording accessable throughout the whole runtime.
public class AnalyticsHolder
{
    //testing singleton method to do the data tracking

    private static AnalyticsHolder instance;

    //constructor
    public AnalyticsHolder() { }

    //check instance. if already exists, don't create new
    public static AnalyticsHolder Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new AnalyticsHolder();
            }

            return instance;
        }
    }

    // Generic Game Stuff (dynamic?)
    public string slimeChosen { get; set; } //character they ended on 
    public string timeElapsed { get; set; } //hh:mm:ss .
    public int numOfFailedAttempts { get; set; } //gets num of loses in all of game .

    //====Lab Analysis=====
    public bool win { get; set; } //records win statement 
    public string timeTaken { get; set; } //hh:mm:ss 
    public int enemiesDefeated { get; set; } //records num of enemies defeated 
    public int numEnemiesSpawned { get; set; } //takes in num of enemies spawned .
    public int hitsTaken { get; set; } //num of hits taken 
    public int waveEnd { get; set; } //wave the game ended on 
    public int waveEndId { get; set; } //wave the game ended on but id based on data .
    public int buffsPicked { get; set; } //num of buffs picked .
    public int buffsDropped { get; set; } //num of buffs dropped .

   
    //====player stat end on=====
    public int health { get; set; } //health player ends on 
    public float damage { get; set; } //damage player ends on 
    public float speed { get; set; } //speed player ends on 
    public float shotSpeed { get; set; } //shot speed player ends on 
    public float range { get; set; } //range player ends on 
    public float slimeRate { get; set; } //slimerate player ends on 
    public int damageReceived { get; set; } //amt of damage received in total 
    public float damageDealt { get; set; } //gets the total num of damage dealt 


    //====Deeper Analysis=====
    public int totalShots { get; set; } //total num of shots shot during run .
    public int shotsMissed { get; set; } //total num of shots missed .
    public int shotsHit { get; set; } //total shots hit .
    public float accuracy { get; set; } //calculated accuracy .


    //====Buff stats=====
    public int numOfBuff1Spawned { get; set; } //num of buff1 dropped .
    public int numOfBuff2Spawned { get; set; } //num of buff2 dropped .
    public int numOfBuff3Spawned { get; set; } //num of buff3 dropped .
    public int numOfBuff4Spawned { get; set; } //num of buff 4 dropped .

    public int takenNumOfBuff1 { get; set; } //num of buff1 taken by player .
    public int takenNumOfBuff2 { get; set; } //num of buff2 taken by player .
    public int takenNumOfBuff3 { get; set; } //num of buff3 taken by player .
    public int takenNumOfBuff4 { get; set; } //num of buff4 taken by player .
    public string mostTakenBuff { get; set; } //most buff taken by player .

    //====Enemy Stats=====
    public int numOfEnemy1Spawned { get; set; } //num of enemy1 spawned by player .
    public int numOfEnemy2Spawned { get; set; } //num of enemy2 spawned by player .
    public int numOfEnemy3Spawned { get; set; } //num of enemy3 spawned by player .
    public int numOfEnemy4Spawned { get; set; } //num of enemy4 spawned by player .


    public int killedNumOfEnemy1 { get; set; } //num of enemy1 killed by player .
    public int killedNumOfEnemy2 { get; set; } //num of enemy2 killed by player .
    public int killedNumOfEnemy3 { get; set; } //num of enemy3 killed by player .
    public int killedNumOfEnemy4 { get; set; } //num of enemy4 killed by player .


    //====Hit reading=====
    public int hitByEnemy1 { get; set; } //num of hits by enemy1 .
    public int hitByEnemy2 { get; set; } //num of hits by enemy1 .
    public int hitByEnemy3 { get; set; } //num of hits by enemy1 .
    public int hitByEnemy4 { get; set; } //num of hits by enemy1 .

    public string mostHitId; //id of enemy that hit the player the most .

 
}
