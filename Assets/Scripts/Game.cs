using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    private static List<Character> characterList;

    public static List<Character> GetCharacterList()
    {
        return characterList;
    }

    public static void SetCharacterList(List<Character> List)
    {
        characterList = List;
    }

    public static Character GetCharacterByCharId(int charId)
    {
        return characterList.Find(x => x.charId == charId);
    }
}
