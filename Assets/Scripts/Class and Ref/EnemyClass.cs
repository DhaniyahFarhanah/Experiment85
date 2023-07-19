using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyClass
{
    public string enemyId;
    public string enemyName;
    public int itemDropRate;
    public string itemDrop;
    public int buffDropRate;
    public string buffDrop;
    public int health;
    public int damage;
    public float speed;
    public string enemyDesc;

    public EnemyClass(string enemyId, string enemyName, int itemDropRate ,string itemDrop, int buffDropRate, string buffDrop, int health, int damage, float speed, string stringenemyDesc)
    {
        this.enemyId = enemyId;
        this.enemyName = enemyName;
        this.itemDropRate = itemDropRate;
        this.itemDrop = itemDrop;
        this.buffDropRate = buffDropRate;
        this.buffDrop = buffDrop;
        this.health = health;
        this.damage = damage;
        this.speed = speed;
        this.enemyDesc = stringenemyDesc;
    }
}
