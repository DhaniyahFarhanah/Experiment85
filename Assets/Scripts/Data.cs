using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 
{
    //The fucking name has to match the fucking table name >:C
    public List<RefCharacterData> Character;
    public List<RefDialogueData> Dialogue;
    public List<RefNPCData> NPC;
    public List<RefEnemyData> Enemies;
    public List<RefBuffData> Buffs;
    public List<RefWaveData> Waves;
}
