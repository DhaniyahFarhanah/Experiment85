using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisabler : MonoBehaviour
{
    private GameObject player;

    [SerializeField] GameObject[] UI;

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

    bool checkDisable()
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
