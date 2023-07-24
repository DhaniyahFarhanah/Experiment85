using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// sets static game variables to persist between scenes
public class GameClass
{
    //Here is holds all the data to be held between scenes. Default is S01
    public static string currentSlimeId = "S01";
    public static bool hasFirstTalk = false;

    //highscore stuff. Would have made it more scalable by recording the highscore of each slime and populate it but it's too late and we needed to reduce scope.
    public static int highscoreWave = 0;
    public static int highscoreEnemiesDefeated = 0;
    public static float highscoreDamageDone = 0f;


    //====Slime stuff=====
    public static void SetCurrentSlimeId(string Id)
    {
        currentSlimeId = Id;
        
    }
    
    public static string GetCurrentSlimeId()
    {
        return currentSlimeId;
    }

    public static void SetHasFirstTalk(bool talk)
    {
        hasFirstTalk = talk;
    }

    public static bool HasFirstTalk()
    {
        return hasFirstTalk;
    }

    
    //Set highscores
    public static void SetWaveHighscore(int wave)
    {

        highscoreWave = wave;       

    }

    public static void SetEnemiesHighscore(int enemies)
    {
        highscoreEnemiesDefeated = enemies;
    }

    public static void SetDamageHighscore(float damage)
    {
        highscoreDamageDone = damage;
    }

    //get highscores
    public static int GetWaveHighscore()
    {
        return highscoreWave;
    }

    public static int GetEnemiesDefeatedHighscore()
    {
        return highscoreEnemiesDefeated;
    }

    public static float GetDamageHighscore()
    {
        return highscoreDamageDone;
    }
}
