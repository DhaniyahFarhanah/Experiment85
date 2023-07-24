using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// allows Invisibility cheat
public class Cheats : MonoBehaviour
{
    [SerializeField] GameObject cheat;
    bool invisibilityCheat;
    // Start is called before the first frame update
    void Start()
    {
        invisibilityCheat = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("InvisibleCheat")) //if f2 pressed
        {
            if(!invisibilityCheat)
            {
                invisibilityCheat = true;
                cheat.SetActive(true);
            }
            else
            {
                invisibilityCheat = false;
                cheat.SetActive(false);

                bool once = false;
                if (!once)
                {
                    if(GameObject.FindGameObjectWithTag("Player") != null) //only if there's a player so no error
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLab>().isInvisable = false;
                    }
                    once = true;
                }
            }
        }

        if (invisibilityCheat) //if the cheat is active,
        {

            if (GameObject.FindGameObjectWithTag("Player") != null) //only if there's a player so no error
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLab>().isInvisable = true;
            }
        }

        
    }

}
