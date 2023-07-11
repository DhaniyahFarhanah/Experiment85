using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    private static List<CharacterClass> characterList;

    public static List<CharacterClass> GetCharacterList()
    {
        return characterList;
        
    }

    public static CharacterClass GetCharacterByRefIf(string charRefId)
    {
        //Way 1
        foreach (CharacterClass c in characterList)
        {
            if(c.charId == charRefId)
            {
                return c;
            }
        }
        return null;

        //Way 2
        //return characterList.Find(x => x.charId == charRefId);
    }

    public static void SetCharacterList(List<CharacterClass> cList)
    {
        characterList = cList;
    }

    
}
