using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public void LoadRefData()
    {
        string filePath = Path.Combine(Application.dataPath, "Scripts/Data/Character.json");

        string dataString = File.ReadAllText(filePath);
        Debug.Log(dataString);

        DemoData demoData = JsonUtility.FromJson<DemoData>(dataString);

        //process data
        ProcessDemoData(demoData);
    }

    private void ProcessDemoData(DemoData demoData)
    {
        List<Character> characterList = new List<Character>();
        foreach (Character C in demoData.Character)
        {
            Character character = new Character(C.charId, C.charName, C.baseStatHealth,C.baseStatDmg, C.baseStatSpeed, C.baseStatShotSpeed, C.baseStatRange,C.baseStateSlimeRate);
            characterList.Add(character);
        }

        Game.SetCharacterList(characterList);
        Debug.Log(Game.GetCharacterList().Count);
    }
}
