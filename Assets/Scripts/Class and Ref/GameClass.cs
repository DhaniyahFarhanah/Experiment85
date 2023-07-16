using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClass
{
    //Here is holds all the data to be held between scenes.
    public static string currentSlimeId = "S01";

    //level info holder
    public static string currentLevelId = "grey";

    //Inventory holder
    public static int amtOfRubber;
    public static int amtOfCircuit;
    public static int amtOfPlasma;
    public static int amtOfTitanium;

    //====Slime stuff=====
    public static void SetCurrentSlimeId(string Id)
    {
        currentSlimeId = Id;
        
    }
    
    public static string GetCurrentSlimeId()
    {
        return currentSlimeId;
    }

    //=====Lab stuff======
    public static void SetCurrentLevelId(string levelId)
    {
        currentLevelId = levelId;
    }

    public static string GetCurrentLevelId()
    {
        return currentLevelId;
    }

    //=====Inventory Stuff======
    public static void SetInventory(int rubber, int circuit, int plasma, int titanium)
    {
        amtOfRubber = rubber;
        amtOfCircuit = circuit;
        amtOfPlasma = plasma;
        amtOfTitanium = titanium;
    }

    public static int GetRubber()
    {
        return amtOfRubber;
    }
    public static int GetCircuit()
    {
        return amtOfCircuit;
    }
    public static int GetPlasma()
    {
        return amtOfPlasma;
    }
    public static int GetTitanium()
    {
        return amtOfTitanium;
    }
}
