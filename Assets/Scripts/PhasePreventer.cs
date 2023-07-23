using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
//on invisibility, the player is able to phase through the enemies. But it shouldn't phase through walls
public class PhasePreventer : MonoBehaviour
{
    //This is for preventing the slime from phasing through walls. nothing too important
    [SerializeField] CapsuleCollider2D capsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            capsuleCollider.enabled = true;
        }
    }
}
