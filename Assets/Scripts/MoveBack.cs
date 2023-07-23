using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// This script just returns game saferoom if lose and main menu if win.
public class MoveBack : MonoBehaviour
{
    [SerializeField] int safeRoomIndex;
    [SerializeField] int mainMenuIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            bool win = GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().win;

            if (Input.GetButtonDown("Enter"))
            {
                //supposed to go back to saferoom when MainMenu
                if (!win)
                {
                    GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadChosenLevel(safeRoomIndex);
                    

                }
                //supposed to end game if win so go to main menu
                else
                {
                    //set to defaults in gameclass. Simulates a "new game"
                    GameClass.SetCurrentSlimeId("S01");
                    GameClass.hasFirstTalk = false;
                    GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadChosenLevel(mainMenuIndex);

                }

            }
        }
    }
}
