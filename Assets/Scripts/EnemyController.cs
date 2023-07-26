using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// script that controls enemy behaviour and drops.
public class EnemyController : MonoBehaviour
{

    //=====Json List=====
    private List<EnemyClass> enemyJsonList;

    private GameObject player;
    private Rigidbody2D enemyRB;
    private SpriteRenderer sprite;
    private Animator anim;

    PlayerLab playerStats; 


    //values to be populated by JSON
    public string enemyId;
    private string enemyName;
    public float enemyHealth;
    public int enemyDamage;
    private float enemySpeed; //speed has been balanced to some extent
    private string enemyDesc;


    //item drop stuff
    [SerializeField] GameObject buffPrefab;
    private string buffDropId;
    private int buffDropRate;
    private string buffDrop;
    Vector3 spawnBuffLocation;
    
    // new class to stop individual info for split
    class Buff
    {
        public string buffId;
        public int buffDropPercentage;
    }

    private List<Buff> buffSpawnList = new List<Buff>();

    private void Awake()
    {
        enemyJsonList = GameData.GetEnemyList();
        GetJsonReading();
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerLab>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //sets animator cause last minute add ahahahahah
        anim.SetInteger("index", AnimSwitchCase());

    }

    // Update is called once per frame
    void Update()
    {
      if(enemyHealth <= 0) //if health less than or = to 0
        {
            spawnBuffLocation = gameObject.transform.position; //gets where enemy dies at
            Die();
        }

        SplitDropStuff(buffDrop);

    }

    //Fixed update calculates updates at a fixed 50fps (i think)
    void FixedUpdate()
    {
        Movement();  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player && !playerStats.isInvisable) //if the player is contacted on and the slime is allowed to get hurt then do damage
        {
            DoDamage(); 
        }
    }

    //check if collided to hit player
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == player && !playerStats.isInvisable)
        {
            DoDamage();
        }
    }

    //this code is for them to move towards player
    void Movement() 
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        enemyRB.velocity = direction * enemySpeed;

        //this code sucks so I'mma just-
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed/2) * Time.deltaTime); 

        //to flip character sprite depending on where player is.
        if (transform.position.x < player.transform.position.x)
        {
            sprite.flipX = true;
        }
        else if (transform.position.x > player.transform.position.x)
        {
            sprite.flipX = false;
        }
    }

    //attack player and remove health from player.
    void DoDamage() 
    {
        playerStats.amtOfDamageReceived += enemyDamage;

        playerStats.currentStatHealth -= enemyDamage;
        playerStats.SetToDislay();
        Debug.Log("Attack! Damage dealt: " + enemyDamage);
    }
    
    //d i e
    void Die()
    {
        int hasDrop = Random.Range(1, 101);

        //if the random is less than drop rate, drop the buff
        //eg. enemy1 has 80% drop rate. Random = 67. since 67 < drop rate. drops a buff
        if(hasDrop <= buffDropRate)
        {
            SpawnBuff();

        }
        GameObject waveHandler = GameObject.FindGameObjectWithTag("WaveHandler");

        if(waveHandler != null)
        {
            //analytics
            GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().enemyNeeded--;
            playerStats.enemiesDefeated++;

            //analytics
            switch (enemyId)
            {
                case "E01": playerStats.numOfEnemy1Killed++; break;
                case "E02": playerStats.numOfEnemy2Killed++; break;
                case "E03": playerStats.numOfEnemy3Killed++; break;
                case "E04": playerStats.numOfEnemy4Killed++; break;
            }
        }
        
        Destroy(gameObject);
    }

    void GetJsonReading() //get json readings and populate into variables
    {
        foreach(EnemyClass e in enemyJsonList)
        {
            if(enemyId == e.enemyId)
            {
                enemyName = e.enemyName;
                enemyHealth = e.health;
                enemyDamage = e.damage;
                enemySpeed = e.speed / 1.7f;
                buffDrop = e.buffDrop;
                buffDropRate = e.buffDropRate;
            }
        }
    }

    void SplitDropStuff(string buffDrop) //splits the string of "buff1#25@buff2#50" blah blah from json
    {
        buffSpawnList.Clear();

        string[] splitString = buffDrop.Split('@');

        foreach(string s in splitString)
        {
            Buff toAdd = new Buff();

            string[] splitAgain = s.Split('#');
            int index = 0;

            foreach(string s2 in splitAgain)
            {
                if(index == 0)
                {
                    toAdd.buffId = s2;
                }
                else
                {
                    toAdd.buffDropPercentage = int.Parse(s2);
                }
                index++;
            }

            buffSpawnList.Add(toAdd);
        }

    }

    //this SpawnBuff() function...made me so mad :'c

    /*so the reading for the JSON is like this
      eg.
      buff1 20
      buff2 60
      buff3 100

      the number is the max range. it would convert like this:

      20 < buff2 < 60

      making buff 1 have a 20% drop rate
      buff 2 have a 40% drop rate.
      buff 3 have a 40% drop rate
      making it a total of 100%. Since the percentage that it drops ANYTHING is in another reading back in update. 

      the math took FOREVER OMG MY BRAIN WAS SMOKING MY MIND NO WORK. Had to change how we stored the json reading to come up
      with an effective way to do this calculation.

     cause previously it was like
      eg.
      buff1 20
      buff2 40
      buff3 40

      but I was like HOW TO DO IN CODE AAAHHHHH.

      sorry.
      thought it was interesting :D
     */

    void SpawnBuff()
    {
        GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuffsDropped++;

        int drop = Random.Range(1, 101);
        int prev = 1; 

        //I can't think... math is hard
        foreach(Buff buff in buffSpawnList)
        {
            //if not 100
            if(drop <= buff.buffDropPercentage)
            {
                if (prev <= drop)
                {
                    buffDropId = buff.buffId;
                    break;
                }

                else
                {
                    prev = buff.buffDropPercentage; //repopulate with the previous reading
                }

            }

            else
            {
                prev = buff.buffDropPercentage;
            }



        }


        //Instantiate Buff
        GameObject buffSpawned = Instantiate(buffPrefab, spawnBuffLocation, Quaternion.identity);
        //returns buffId so it populates with buffJSOn
        buffSpawned.GetComponent<BuffScript>().buffId = buffDropId;

        //analytics
        switch (buffDropId)
        {
            case "buff1": GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuff1++; break;
            case "buff2": GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuff2++; break;
            case "buff3": GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuff3++; break;
            case "buff4": GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuff4++; break;
        }

        //populate the prefab with buff id.
        //buffscript will do the rest.
    }

    //switches animator controller index
    int AnimSwitchCase()
    {
        int returned = 0;

        switch (enemyId)
        {
            case "E01": returned = 0; break;
            case "E02": returned = 1; break;
            case "E03": returned = 2; break;
            case "E04": returned = 3; break;
        }

        return returned; 
    }
    
}
