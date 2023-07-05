using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //REMEMBER THIS
public class Character
{
    public string charId { get; set; }
    public string charName { get; set; }
    public int baseStatHealth { get; set; }
    public float baseStatDmg { get; set; }
    public float baseStatSpeed { get; set; }
    public float baseStatShotSpeed { get; set; }
    public float baseStatRange { get; set; }
    public float baseStateSlimeRate { get; set; }

    public Character(string charId, string charName, int baseStatHealth, float baseStatDmg, float baseStatSpeed, float baseStatShotSpeed, float baseStatRange, float baseStateSlimeRate)
    {
        this.charId = charId;
        this.charName = charName;
        this.baseStatHealth = baseStatHealth;
        this.baseStatDmg = baseStatDmg;
        this.baseStatSpeed = baseStatSpeed;
        this.baseStatShotSpeed = baseStatShotSpeed;
        this.baseStatRange = baseStatRange;
        this.baseStateSlimeRate = baseStateSlimeRate;
    }
}
