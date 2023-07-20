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
    private int enemyDamage;
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
        }
        GameObject waveHandler = GameObject.FindGameObjectWithTag("WaveHandler");

        if(waveHandler != null)
        {
            Debug.Log("minus");
            GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().enemyNeeded--;
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

        //populate the prefab with buff id.
        //buffscript will do the rest.
    }


    // IMPORTANT: SPEED NEEDS BALANCING. Speed values are different from data because it needs to be balanced. This is balanced data. 
    /*void TempSwitchCaseStuff() //ignore this, this is more for manual shit

    {
        switch(enemyId)
        {
            case "E01":

                enemyName = "Hazmat";
                health = 8f;
                damage = 1;
                speed = 1.7f;
                enemyDesc = "Basic Enemy with Low Health, Damage and Speed";

                break;

            case "E02":

                enemyName = "Chaser";
                health = 4f;
                damage = 2;
                speed = 4f; 
                enemyDesc = "Speedy Enemy with Low Health, Moderate Damage and High Speed";

                break;

            case "E03":

                enemyName = "Juggernaut";
                health = 16f;
                damage = 2;
                speed = 1f;
                enemyDesc = "Juggernaut Enemy with High Health, High Damage and Low Speed";

                break;

            case "E04":

                enemyName = "Reaper";
                health = 14f;
                damage = 3;
                speed = 2.5f;
                enemyDesc = "Dangerous Enemy with Moderate Health, High Damage and Moderate Speed";

                break;

        }
    }*/
    
}
