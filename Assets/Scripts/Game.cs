using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Game
{
    private static Player mainPlayer;
    private static List<Character> characterList;

    public static Player GetPlayer()
    {
        return mainPlayer;
    }

    public static void SetPlayer(Player player)
    {
        mainPlayer = player;
    }

    public static List<Character> GetCharacterList()
    {
        return characterList;
    }

    public static void SetCharacterList(List<Character> List)
    {
        characterList = List;
    }

    public static Character GetCharacterByCharId(string AcharId)
    {
        Debug.Log("charID:" + AcharId);
        Debug.Log("Character List Count:" + Game.GetCharacterList().Count);
        foreach (Character C in characterList)
        {
            Debug.Log("charId " + C.charId);
            if (C.charId == AcharId) return C;
        }
        Debug.Log("Character Not Found!");
        return null;
        
        //return characterList.Find(x => x.charId == charId);
    }
}
