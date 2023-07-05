using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

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
        foreach (Character C in characterList)
        {
            if (C.charId == AcharId) return C;
        }
        return null;
        //Debug.Log(charId);
        //return characterList.Find(x => x.charId == charId);
    }
}
