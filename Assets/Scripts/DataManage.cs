using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// Script done by: Gerald (Gerald Wei Jie SOH)

//Data manager for all the data created. JSON stuff. Followed JY stuff
public class DataManage : MonoBehaviour
{
    //This is to populate the data from json into the respective data holders (RefCharacterData & CharacterClass)
    public TextAsset exportFile;
    
    public bool callOnce;

    private void Awake()
    {
        LoadRefData();
        SaveAnalytics();
    }

    public void LoadRefData()
    {
        Data data = ReadData<Data>();
        ProcessCharData(data);
        ProcessDialogueData(data);
        ProcessNPCData(data);
        ProcessBuffData(data);
        ProcessEnemyData(data);
        ProcessWaveData(data);
    }

    public T ReadData<T>()
    {
        string dataString = exportFile.text;
        T data = JsonUtility.FromJson<T>(dataString);
        return data;
    }

    public void SaveAnalytics() //saves the analytics into an external file
    {
        if (!callOnce && !string.IsNullOrEmpty(AnalyticsHolder.Instance.slimeChosen))
        {
            AnalyticsHolder analytics = new AnalyticsHolder();
            string filePath = Path.Combine(Application.persistentDataPath + "/export.csv");
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string header = "timestamp,charId,slimeStats,shotStats,winLose,waveId,timeTaken,enemiesDefeated,buffPicked,favouriteBuff,damageReceived,hitsTaken,damageDealt,precision,overallAccuracy";
            string data = timestamp + "," +
                        AnalyticsHolder.Instance.slimeChosen + "," + 
                        "Health:" + AnalyticsHolder.Instance.health + "|Damage:" + 
                        AnalyticsHolder.Instance.damage + "|Speed:" + AnalyticsHolder.Instance.speed + "," +
                        "shotSpeed:" + AnalyticsHolder.Instance.shotSpeed + "|Range:" + 
                        AnalyticsHolder.Instance.range + "|slimeRate:" + AnalyticsHolder.Instance.slimeRate + "," +
                        AnalyticsHolder.Instance.win + "," + AnalyticsHolder.Instance.waveEnd + "," +
                        AnalyticsHolder.Instance.timeTaken + "," + AnalyticsHolder.Instance.enemiesDefeated + "," +
                        "buffsPicked:" + AnalyticsHolder.Instance.buffsPicked + "/" + AnalyticsHolder.Instance.buffsDropped + "," +
                        AnalyticsHolder.Instance.mostTakenBuff + "," +
                        AnalyticsHolder.Instance.damageReceived + "," +
                        "E01#" + AnalyticsHolder.Instance.hitByEnemy1 + "@E02#" + AnalyticsHolder.Instance.hitByEnemy2 + "@E03#" + 
                        AnalyticsHolder.Instance.hitByEnemy3 + "@E04#" + AnalyticsHolder.Instance.hitByEnemy4 + "," +
                        AnalyticsHolder.Instance.damageDealt + "," +
                        "shotsHit:" + AnalyticsHolder.Instance.shotsHit + "/" + AnalyticsHolder.Instance.totalShots + "," +
                        AnalyticsHolder.Instance.accuracy;
           
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, header + Environment.NewLine);
                    //SW.WriteLine(header);
                }
                //SW.WriteLine(data);
                File.AppendAllText(filePath, data + Environment.NewLine);
                callOnce = true;
            
        }
    }

    //Function will process everything and store inside the data
    private void ProcessCharData(Data charData)
    {
        List<CharacterClass> characterList = new List<CharacterClass>();

        foreach (RefCharacterData refChar in charData.Character)
        {
            CharacterClass character = new CharacterClass(refChar.charId, refChar.charName, refChar.baseStatHealth,
                refChar.baseStatDmg, refChar.baseStatSpeed, refChar.type, refChar.baseStatShotSpeed, refChar.baseStatRange,
                refChar.baseStateSlimeRate);
            characterList.Add(character);
        }
        GameData.SetCharacterList(characterList);
    }

    //Function will process everything and store inside the data
    private void ProcessDialogueData(Data dialogueData)
    {
        List<DialogueClass> dialogueList = new List<DialogueClass>();

        foreach (RefDialogueData refDialogue in dialogueData.Dialogue)
        {
            DialogueClass dialogue = new DialogueClass(refDialogue.dialogueId, refDialogue.nextCutsceneRefId, refDialogue.currentSpeaker, refDialogue.npcId, refDialogue.dialogue);
            dialogueList.Add(dialogue);
        }
        GameData.SetDialogueList(dialogueList);
    }

    //Function will process everything and store inside the data
    private void ProcessNPCData(Data npcData)
    {
        List<NPCClass> npcList = new List<NPCClass>();

        foreach (RefNPCData refNPC in npcData.NPC)
        {
            NPCClass npc = new NPCClass(refNPC.npcId, refNPC.npcName, refNPC.image, refNPC.dialogueId);
            npcList.Add(npc);
        }
        GameData.SetNPCList(npcList);
    }

    //Function will process everything and store inside the data
    private void ProcessEnemyData(Data enemyData)
    {
        List<EnemyClass> enemyList = new List<EnemyClass>();

        foreach (RefEnemyData refEnemy in enemyData.Enemies)
        {
            EnemyClass enemy = new EnemyClass(refEnemy.enemyId, refEnemy.enemyName, refEnemy.itemDropRate, refEnemy.itemDrop, refEnemy.buffDropRate, refEnemy.buffDrop, refEnemy.health, refEnemy.damage, refEnemy.speed, refEnemy.enemyDesc);
            enemyList.Add(enemy);
        }
        GameData.SetEnemyList(enemyList);
        //Debug.Log("enemyList: " + enemyList.Count);
    }

    //Function will process everything and store inside the data
    private void ProcessBuffData(Data buffData)
    {
        List<BuffClass> buffList = new List<BuffClass>();

        foreach (RefBuffData refBuff in buffData.Buffs)
        {
            BuffClass buff = new BuffClass(refBuff.buffId, refBuff.buffName, refBuff.stat, refBuff.value, refBuff.buffDescription);
            buffList.Add(buff);
        }
        GameData.SetBuffList(buffList);
    }

    //Function will process everything and store inside the data
    private void ProcessWaveData(Data waveData)
    {
        List<WaveClass> waveList = new List<WaveClass>();

        foreach (RefWaveData refWave in waveData.Waves)
        {
            WaveClass wave = new WaveClass(refWave.waveId, refWave.keyCardId, refWave.waveNo, refWave.nextWave, refWave.enemyId);
            waveList.Add(wave);
        }
        GameData.SetWaveList(waveList);
    }

    /*
     * Lecture vid Week 8 - 11 mins in
     * 
        EnumType nameOfVal = (EnumType)System.Enum.Parse(EnumType, charData.labelOfVal);
    */
}

