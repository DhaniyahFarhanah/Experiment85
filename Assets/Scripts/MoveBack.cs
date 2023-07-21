using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    GameClass.SetCurrentSlimeId("S01"); //simulate new game
                    GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadChosenLevel(mainMenuIndex);

                }

            }
        }
    }
}
