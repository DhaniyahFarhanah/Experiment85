using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyController : MonoBehaviour
{

    //=====Json List=====
    private List<EnemyClass> enemyJsonList;
    private List<BuffClass> enemyBuffList;

    private GameObject player;
    private Rigidbody2D enemyRB;
    private SpriteRenderer sprite;

    PlayerLab playerStats; 

    //this all can be used as private if using json but public for now cause uhhh actually I'mma make switch case to keep them private
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
    

    class Buff
    {
        public string buffId;
        public int buffDropPercentage;
    }

    private List<Buff> buffSpawnList = new List<Buff>();

    private void Awake()
    {
        enemyJsonList = GameData.GetEnemyList();
        enemyBuffList = GameData.GetBuffList();
        GetJsonReading();
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerLab>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //TempSwitchCaseStuff();
    }

    // Update is called once per frame
    void Update()
    {
      if(enemyHealth <= 0)
        {
            spawnBuffLocation = gameObject.transform.position;
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
        if (collision.gameObject == player && !playerStats.isInvisable)
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

    void Die()
    {
        int hasDrop = Random.Range(0, 101);

        if(buffDropRate >= hasDrop)
        {
            SpawnBuff();
            GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuffsDropped++;

        }
        GameObject waveHandler = GameObject.FindGameObjectWithTag("WaveHandler");

        if(waveHandler != null)
        {
            GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().enemyNeeded--;
            playerStats.enemiesDefeated++;

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

    void GetJsonReading()
    {
        foreach(EnemyClass e in enemyJsonList)
        {
            if(enemyId == e.enemyId)
            {
                enemyName = e.enemyName;
                enemyHealth = e.health;
                enemyDamage = e.damage;
                enemySpeed = e.speed / 1.5f;
                buffDrop = e.buffDrop;
                buffDropRate = e.buffDropRate;
            }
        }
    }

    void SplitDropStuff(string buffDrop)
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

    void SpawnBuff()
    {
        int drop = Random.Range(0, 101);
        int prev = 0;

        //I can't think... math is hard
        //TODO MATH (its not working obviously
        foreach(Buff buff in buffSpawnList)
        {
            if(drop <= buff.buffDropPercentage)
            {
                if (prev <= drop)
                {
                    buffDropId = buff.buffId;
                    Debug.Log(buffDropId);
                    break;
                }

                else
                {
                    prev = buff.buffDropPercentage;
                }

            }

            else
            {
                prev = buff.buffDropPercentage;
            }



        }


        //Instantiate Buff
        GameObject buffSpawned = Instantiate(buffPrefab, spawnBuffLocation, Quaternion.identity);
        buffSpawned.GetComponent<BuffScript>().buffId = buffDropId;

        switch (buffDropId)
        {
            case "buff1": GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuff1++; break;
            case "buff2": GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuff2++; break;
            case "buff3": GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuff3++; break;
            case "buff4": GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().numOfBuff1++; break;
        }

        //populate the prefab with buff id.
        //buffscript will do the rest.
    }


    
    
}
