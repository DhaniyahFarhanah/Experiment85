using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //REMEMBER THIS
public class Character
{
    public int charId { get; }
    public string charName { get; }
    public int baseStatHealth { get; }
    public int baseStatDmg { get; }
    public int baseStatSpeed { get; }
    public int baseStatShotSpeed { get; }
    public int baseStatRange { get; }
    public int baseStateSlimeRate { get; }

    public Character(int charId, string charName, int baseStatHealth, int baseStatDmg, int baseStatSpeed, int baseStatShotSpeed, int baseStatRange, int baseStateSlimeRate)
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
