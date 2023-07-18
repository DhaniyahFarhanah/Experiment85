using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        hitWall = false;
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
        //gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //scales the size
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
            if(type != "Powerful")
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
