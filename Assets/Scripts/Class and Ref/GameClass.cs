using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClass
{
    //Here is holds all the data to be held between scenes. Default is S01
    public static string currentSlimeId = "S01";


    //====Slime stuff=====
    public static void SetCurrentSlimeId(string Id)
    {
        currentSlimeId = Id;
        
    }
    
    public static string GetCurrentSlimeId()
    {
        return currentSlimeId;
    }

}
