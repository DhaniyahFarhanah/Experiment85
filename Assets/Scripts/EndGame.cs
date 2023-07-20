using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (canEnd)
        {
            if (Input.GetButton("Interact"))
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
