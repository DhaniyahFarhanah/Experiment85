using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D enemyRB;
    [SerializeField] GameObject sprite;

    public string enemyId;

    //this all can be used as private if using json but public for now cause uhhh actually I'mma make switch case to keep them private
    private string enemyName;
    private float health;
    private int damage;
    private float speed;
    private string enemyDesc;

    //item drop stuff


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

    void FixedUpdate()
    {
        Movement();  
    }

    void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed/2) * Time.deltaTime);

        //to flip character
        if(transform.position.x < player.transform.position.x)
        {
            sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            sprite.transform.localRotation = Quaternion.Euler(0, -180, 0);
        }
    }

    void FlipImage()
    {

    }

    void TempSwitchCaseStuff() //ignore this, this is more for manual shit

    {
        switch(enemyId)
        {
            case "E01":

                enemyName = "Hazmat";
                health = 8f;
                damage = 1;
                speed = 5f;
                enemyDesc = "Basic Enemy with Low Health, Damage and Speed";

                break;

            case "E02":

                enemyName = "Chaser";
                health = 4f;
                damage = 2;
                speed = 7f;
                enemyDesc = "Speedy Enemy with Low Health, Moderate Damage and High Speed";

                break;

            case "E03":

                enemyName = "Juggernaut";
                health = 200f;
                damage = 4;
                speed = 2f;
                enemyDesc = "Juggernaut Enemy with High Health, High Damage and Low Speed";

                break;

            case "E04":

                enemyName = "Reaper";
                health = 40f;
                damage = 5;
                speed = 5f;
                enemyDesc = "Dangerous Enemy with Moderate Health, High Damage and Moderate Speed";

                break;

        }
    }
    
}
