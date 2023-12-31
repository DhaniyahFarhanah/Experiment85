using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
//slimeball controller, attached to slimeball prefab. It has different conditions due to the type of the player.
public class Slimeball : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float dmg;
    public float scale;
    public string type;
    
    private bool hitWall = false;
    private Animator anim;
    private Rigidbody2D rb;
    public SpriteRenderer sr;

    private bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        hitWall = false;
        start = true;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!hitWall)
        {
            SlimeballMovement();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void SlimeballMovement()
    {
        rb.velocity = transform.up * speed * 2f;

        //scales the size to big
        if(type == "Powerful")
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        //scales size to smol
        else if(type == "Piercing")
        {
            gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        //scales size to normal
        else
        {
            gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        
    }

    void DestroySlimeball()
    {
        anim.SetBool("destroy", true);
        hitWall = true;
        Destroy(gameObject, 0.2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().enemyHealth -= dmg;

            if (start) //since this one phases through enemies. it will count a hit only on the first enemy it hits.
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLab>().shotsHit++;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLab>().amtOfDamageDealt += GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLab>().currentStatDmg;
                start = false;
            }

            if (type != "Piercing")
            {
                
                DestroySlimeball();
            }
        }

        if(collision.tag == "Wall")
        {
            DestroySlimeball();
        }
    }
}
