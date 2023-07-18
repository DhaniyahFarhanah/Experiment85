using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCClass
{
    public int npcId;
    public string npcName;
    public int firstDialogueId;
    public int shopDialogueId;
    public string upgradeId;

    public NPCClass(int npcId, string npcName, int firstDialogueId, int shopDialogueId, string upgradeId)
    {
        this.npcId = npcId;
        this.npcName = npcName;
        this.firstDialogueId = firstDialogueId;
        this.shopDialogueId = shopDialogueId;
        this.upgradeId = upgradeId;
    }
}
