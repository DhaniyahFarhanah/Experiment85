using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)

// This is to activate the door to be interactable and exist when the win condition is achieved. Players can choose to keep going for an endless run if they want to. Just don't interact with door
public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject Interact;

    private bool canEnd;
    // Start is called before the first frame update
    void Start()
    {
        canEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canEnd) //if condition met, door is interactable
        {
            if (Input.GetButton("Interact")) //if enter is pressed
            {
                //end game
                GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().end = true;
                GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().win = true; 

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canEnd = true;
            Interact.SetActive(true);
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canEnd = false;
            Interact.SetActive(false);
        }
    }
}
