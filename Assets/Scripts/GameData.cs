using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// Script done by: Gerald (Gerald Wei Jie SOH)
public class GameData 
{
    private static List<CharacterClass> characterList;
    private static List<DialogueClass> dialogueList;
    private static List<NPCClass> npcList;
    private static List<EnemyClass> enemyList;
    private static List<BuffClass> buffList;
    private static List<WaveClass> waveList;

    public static List<CharacterClass> GetCharacterList()
    {
        return characterList;
    }

    public static List<DialogueClass> GetDialogueList()
    {
        return dialogueList;
    }

    public static List<NPCClass> GetNPCList()
    {
        return npcList;
    }
    public static List<EnemyClass> GetEnemyList()
    {
        return enemyList;
       
    }
    public static List<BuffClass> GetBuffList()
    {
        return buffList;
    }
    public static List<WaveClass> GetWaveList()
    {
        return waveList;
    }

    public static CharacterClass GetCharacterByRefId(string charRefId)
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

    public static void SetDialogueList(List<DialogueClass> dList)
    {
        dialogueList = dList;
    }

    public static void SetNPCList(List<NPCClass> nList)
    {
        npcList = nList;
    }
    public static void SetEnemyList(List<EnemyClass> eList)
    {
        enemyList = eList;
    }
    public static void SetBuffList(List<BuffClass> bList)
    {
        buffList = bList;
    }
    public static void SetWaveList(List<WaveClass> wList)
    {
        waveList = wList;
    }
}
