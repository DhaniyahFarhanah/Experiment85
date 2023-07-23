using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Gerald (Gerald Wei Jie SOH)
public class WaveClass
{
    public int waveId;
    public int keyCardId;
    public int waveNo;
    public int nextWave;
    public string enemyId;

         public WaveClass(int waveId, int keyCardId, int waveNo, int nextWave, string enemyId)
    {
        this.waveId = waveId;
        this.keyCardId = keyCardId;
        this.waveNo = waveNo;
        this.nextWave = nextWave;
        this.enemyId = enemyId;
    }

}
