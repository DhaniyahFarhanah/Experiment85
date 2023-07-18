using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCClass
{
    public int npcId;
    public string npcName;
    public string image;
    public int dialogueId;

    public NPCClass(int npcId, string npcName, string image, int dialogueId)
    {
        this.npcId = npcId;
        this.npcName = npcName;
        this.image = image;
        this.dialogueId = dialogueId;
    }
}
