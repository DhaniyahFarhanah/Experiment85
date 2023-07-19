using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuffClass
{
    public string  buffId;
    public string buffName;
    public string stat;
    public float value;
    public string buffDescription;

    public BuffClass(string buffId, string buffName, string stat, float value, string buffDescription)
    {
        this.buffId = buffId;
        this.buffName = buffName;
        this.stat = stat;
        this.value = value;
        this.buffDescription = buffDescription;

    }

}
