using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class DataManage : MonoBehaviour
{
    //This is to populate the data from json into the respective data holders (RefCharacterData & CharacterClass)

    private void Awake()
    {
        LoadRefCharData();
        LoadRefDialogueData();
        LoadRefNPCData();
        LoadRefBuffData();
        LoadRefEnemyData();
        LoadRefWaveData();
    }

    //load char ref data (need specific cause idk if we combining all together
    public void LoadRefCharData()
    {
        //datapath -> for stuff that goes inside the data folder
        //persistentdatapath -> Once write, cannot be rewritten. Use for save data.
        string filePath = Path.Combine(Application.dataPath, "Scripts/Data/export.json");

        string dataString = File.ReadAllText(filePath);
        //Debug.Log(dataString);

        Data charData = JsonUtility.FromJson<Data>(dataString);

        //process data
        ProcessCharData(charData);
    }

    //Function will process everything and store inside the data
    private void ProcessCharData(Data charData)
    {
        List<CharacterClass> characterList = new List<CharacterClass>();

        foreach (RefCharacterData refChar in charData.Character)
        {
            CharacterClass character = new CharacterClass(refChar.charId, refChar.charName, refChar.baseStatHealth, 
                refChar.baseStatDmg, refChar.baseStatSpeed, refChar.type, refChar.baseStatShotSpeed,refChar.baseStatRange,
                refChar.baseStateSlimeRate);
                characterList.Add(character);
        }
        GameData.SetCharacterList(characterList);
    }

    //load char ref data (need specific cause idk if we combining all together
    public void LoadRefDialogueData()
    {
        //datapath -> for stuff that goes inside the data folder
        //persistentdatapath -> Once write, cannot be rewritten. Use for save data.
        string filePath = Path.Combine(Application.dataPath, "Scripts/Data/export.json");

        string dataString = File.ReadAllText(filePath);

        Data dialogueData = JsonUtility.FromJson<Data>(dataString);

        //process data
        ProcessDialogueData(dialogueData);
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

    //load char ref data (need specific cause idk if we combining all together
    public void LoadRefNPCData()
    {
        //datapath -> for stuff that goes inside the data folder
        //persistentdatapath -> Once write, cannot be rewritten. Use for save data.
        string filePath = Path.Combine(Application.dataPath, "Scripts/Data/export.json");

        string dataString = File.ReadAllText(filePath);

        Data npcData = JsonUtility.FromJson<Data>(dataString);

        //process data
        ProcessNPCData(npcData);
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

    public void LoadRefEnemyData()
    {
        //datapath -> for stuff that goes inside the data folder
        //persistentdatapath -> Once write, cannot be rewritten. Use for save data.
        string filePath = Path.Combine(Application.dataPath, "Scripts/Data/export.json");

        string dataString = File.ReadAllText(filePath);

        Data enemyData = JsonUtility.FromJson<Data>(dataString);

        //process data
        ProcessEnemyData(enemyData);
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

    public void LoadRefBuffData()
    {
        //datapath -> for stuff that goes inside the data folder
        //persistentdatapath -> Once write, cannot be rewritten. Use for save data.
        string filePath = Path.Combine(Application.dataPath, "Scripts/Data/export.json");

        string dataString = File.ReadAllText(filePath);

        Data buffData = JsonUtility.FromJson<Data>(dataString);

        //process data
        ProcessBuffData(buffData);
    }

    //Function will process everything and store inside the data
    private void ProcessBuffData(Data buffData)
    {
        List<BuffClass> buffList = new List<BuffClass>();

        foreach (RefBuffData refBuff in buffData.Buffs)
        {
            BuffClass buff = new BuffClass(refBuff.buffId,  refBuff.buffName,    refBuff.stat,     refBuff.value,   refBuff.buffDescription);
            buffList.Add(buff);
        }
        GameData.SetBuffList(buffList);
    }

    public void LoadRefWaveData()
    {
        //datapath -> for stuff that goes inside the data folder
        //persistentdatapath -> Once write, cannot be rewritten. Use for save data.
        string filePath = Path.Combine(Application.dataPath, "Scripts/Data/export.json");

        string dataString = File.ReadAllText(filePath);

        Data waveData = JsonUtility.FromJson<Data>(dataString);

        //process data
        ProcessWaveData(waveData);
    }

    //Function will process everything and store inside the data
    private void ProcessWaveData(Data waveData)
    {
        List<WaveClass> waveList = new List<WaveClass>();

        foreach (RefWaveData refWave in waveData.Waves)
        {
            WaveClass wave = new WaveClass(refWave.waveId,   refWave.keyCardId,   refWave.waveNo,  refWave.nextWave,    refWave.enemyId);
            waveList.Add(wave);
        }
        GameData.SetWaveList(waveList);
    }

    //converting string to enum (in case need)
    /*
     * Lecture vid Week 8 - 11 mins in
     * 
        EnumType nameOfVal = (EnumType)System.Enum.Parse(EnumType, charData.labelOfVal);
    */
}
