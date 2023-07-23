using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Gerald (Gerald Wei Jie SOH)
public class DialogueClass
{
    public int dialogueId;
    public int nextCutsceneRefId;
    public string currentSpeaker;
    public int npcId;
    public string dialogue;

   public DialogueClass(int dialogueId, int nextCutsceneRefId, string currentSpeaker, int npcId, string dialogue)
    {
        this.dialogueId  = dialogueId;
        this.nextCutsceneRefId  = nextCutsceneRefId;
        this.currentSpeaker  = currentSpeaker;
        this.npcId  = npcId;
        this.dialogue = dialogue;
    }
}
