using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public void LoadRefData()
    {
        string filePath = Path.Combine(Application.dataPath, "Scripts/Data/export.json");

        string dataString = File.ReadAllText(filePath);
        Debug.Log(dataString);

        DemoData demoData = JsonUtility.FromJson<DemoData>(dataString);

        //process data
        ProcessDemoData(demoData);
    }

    private void ProcessDemoData(DemoData demoData)
    {
        //List<Character> characterList = new List<Character>();
        //foreach (Character C in demoData.Character)
        //{
        //    Debug.Log("ADD " + C.charId);
        //    Character character = new Character(C.charId, C.charName, C.baseStatHealth,C.baseStatDmg, C.baseStatSpeed, C.baseStatShotSpeed, C.baseStatRange,C.baseStateSlimeRate);
        //    characterList.Add(character);
        //} Removed as JY states that this section of code converts Character json, but is not necessary due to the nature of our table.
        Game.SetCharacterList(demoData.Character);
        Debug.Log("DataManager:" + Game.GetCharacterList());
        Debug.Log("Character List Count:" + Game.GetCharacterList().Count);
    }
}
