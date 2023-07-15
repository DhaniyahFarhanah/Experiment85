using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimeball : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float dmg;

    private Animator anim;
    private Rigidbody2D rb;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
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
        rb.velocity = transform.up * speed;
    }

    public void SlimeballMovement(float speed)
    {
        rb.velocity = transform.up * speed;
    }

    void DestroySlimeball()
    {
        anim.SetBool("destroy", true);
        rb.velocity = Vector2.zero;
        Destroy(gameObject, 0.1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().health -= dmg;
            DestroySlimeball();
        }
    }
}
