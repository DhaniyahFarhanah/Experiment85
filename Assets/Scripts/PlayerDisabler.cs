using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// disables player movement and shooting when UI are active
public class PlayerDisabler : MonoBehaviour
{
    private GameObject player;

    [SerializeField] GameObject[] UI; //array for more customization

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        player.GetComponent<PlayerController>().disabled = checkDisable();
        player.GetComponent<PlayerLab>().disabled = checkDisable();
        
    }

    bool checkDisable() //foreaches the array and if any are open, disable player
    {
        bool toReturn = false;

        foreach(GameObject c in UI)
        {
            if (c.activeSelf)
            {
                toReturn = true;
            }
        }

        return toReturn;
    }
}
