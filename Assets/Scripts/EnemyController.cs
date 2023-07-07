using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D enemyRB;
    [SerializeField] GameObject sprite; //ignore this

    PlayerLab playerStats; 

    public string enemyId;

    //this all can be used as private if using json but public for now cause uhhh actually I'mma make switch case to keep them private
    private string enemyName;
    private float health;
    private int damage;
    private float speed; //speed has been balanced to some extent
    private string enemyDesc;

    //item drop stuff
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerLab>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TempSwitchCaseStuff();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //Fixed update calculates updates at a fixed 50fps (i think)
    void FixedUpdate()
    {
        Movement();  
    }


    //check if collided to hit player
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            DoDamage();
        }
    }

    //this code is for them to move towards player
    void Movement() 
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        enemyRB.velocity = direction * speed;

        //this code sucks so I'mma just-
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed/2) * Time.deltaTime); 

        //to flip character sprite depending on where player is.
        if (transform.position.x < player.transform.position.x)
        {
            sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            sprite.transform.localRotation = Quaternion.Euler(0, -180, 0);
        }
    }

    //attack player and remove health from player.
    void DoDamage() 
    {
        if(playerStats.isHit) //if the player is hit then do damage
        {
            playerStats.currentStatHealth -= damage;
            playerStats.SetToDislay();
            playerStats.isHit = false;
            Debug.Log("Attack! Damage dealt: " + damage);

        }
       
    }


    // IMPORTANT: SPEED NEEDS BALANCING. Speed values are different from data because it needs to be balanced. This is balanced data. 
    void TempSwitchCaseStuff() //ignore this, this is more for manual shit

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
                speed = 2.5f; 
                enemyDesc = "Speedy Enemy with Low Health, Moderate Damage and High Speed";

                break;

            case "E03":

                enemyName = "Juggernaut";
                health = 200f;
                damage = 4;
                speed = 1f;
                enemyDesc = "Juggernaut Enemy with High Health, High Damage and Low Speed";

                break;

            case "E04":

                enemyName = "Reaper";
                health = 40f;
                damage = 5;
                speed = 1.7f;
                enemyDesc = "Dangerous Enemy with Moderate Health, High Damage and Moderate Speed";

                break;

        }
    }
    
}
