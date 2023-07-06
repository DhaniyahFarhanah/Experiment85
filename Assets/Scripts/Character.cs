using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //REMEMBER THIS
public class Character
{
    public string charId;
    public string charName;
    public int baseStatHealth;
    public float baseStatDmg;
    public float baseStatSpeed;
    public string type;
    public float baseStatShotSpeed;
    public float baseStatRange;
    public float baseStateSlimeRate;
    //get and set removed as not compatible with json function, plus public already means it can be get and set.
    public Character(string charId, string charName, int baseStatHealth, float baseStatDmg, float baseStatSpeed, string type, float baseStatShotSpeed, float baseStatRange, float baseStateSlimeRate)
    {
        this.charId = charId;
        this.charName = charName;
        this.baseStatHealth = baseStatHealth;
        this.baseStatDmg = baseStatDmg;
        this.baseStatSpeed = baseStatSpeed;

        this.type = type;
        this.baseStatShotSpeed = baseStatShotSpeed;
        this.baseStatRange = baseStatRange;
        this.baseStateSlimeRate = baseStateSlimeRate;
    }
}
