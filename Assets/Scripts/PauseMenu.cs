using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ContinueButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        int index = SceneManager.GetActiveScene().buildIndex;

        if(index == 0)
        {
            //if main menu, restart main menu
            GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadChosenLevel(0);
        }
        else
        {
            //always return to saferoom on restart if not main menu
            GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadChosenLevel(1);
        }
    }

    public void QuitButton()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    
}
