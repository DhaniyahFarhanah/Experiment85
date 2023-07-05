using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Player
{
    private string playerId;
    private string charId;

    private int currentStatHealth;
    private float currentStatDmg;
    private float currentStatSpeed;
    private float currentStatShotSpeed;
    private float currentStatRange;
    private float currentStateSlimeRate;

    private bool isDirty; // to check if the values have changed

    public Player(string playerId, string charId)
    {
        this.playerId = playerId;
        this.charId = charId;
        isDirty = true;
    }
   
    public string GetPlayerId()
    {
        UpdateStats();
        return playerId;
    }

    public string GetCharacterId()
    {
        UpdateStats();
        return charId;
    }

    public int GetHealth()
    {
        UpdateStats();
        return currentStatHealth;
    }

    public float GetDmg()
    {
        UpdateStats();
        return currentStatDmg;
    }

    public float GetSpeed()
    {
        UpdateStats();
        return currentStatSpeed;
    }
    public float GetShotSpeed()
    {
        UpdateStats();
        return currentStatShotSpeed;
    }
    public float GetRange()
    {
        UpdateStats();
        return currentStatRange;
    }
    public float GetSlimeRate()
    {
        UpdateStats();
        return currentStateSlimeRate;
    }

    public void SetCharacterId(string ID)
    {
        charId = ID;
        isDirty = true;
    }

    public bool UpdateStats()
    {
        if (!isDirty) return false;

        Character playerCharacter = Game.GetCharacterByCharId(charId);
        // Debug.Log("health:", playerCharacter.baseStatHealth);
        Debug.Log(playerCharacter);
        currentStatHealth = playerCharacter.baseStatHealth; 
        currentStatDmg = playerCharacter.baseStatDmg;
        currentStatSpeed = playerCharacter.baseStatSpeed;
        currentStatShotSpeed = playerCharacter.baseStatShotSpeed;
        currentStatRange = playerCharacter.baseStatRange;
        currentStateSlimeRate = playerCharacter.baseStateSlimeRate;

        return true;
    }
}
