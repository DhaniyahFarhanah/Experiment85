using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManage : MonoBehaviour
{
    //This is to populate the data from json into the respective data holders (RefCharacterData & CharacterClass)
    private void Awake()
    {
        LoadRefCharData();
        LoadRefDialogueData();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

    //converting string to enum (in case need)
    /*
     * Lecture vid Week 8 - 11 mins in
     * 
        EnumType nameOfVal = (EnumType)System.Enum.Parse(EnumType, charData.labelOfVal);
    */
}
