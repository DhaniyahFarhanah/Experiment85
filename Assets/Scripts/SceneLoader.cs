using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)

//===============SCENE LOADER===============
// This script allows the scene to move onto the next scene in the build setting with a transition. Now allows immediate transitions or without the transition.
public class SceneLoader : MonoBehaviour
{
    public Animator sceneTranAnimator; //animator with the transition
    public float tranWaitTime = 1f; //time for animator to play the animation
    
    public bool withTran; //choice to have transition or instant change.

    // Update is called once per frame
    void Update()
    {
        if (withTran) //disables the transition animator if transition isn't wanted
        {
            sceneTranAnimator.enabled = true;
        }
        else
        {
            sceneTranAnimator.enabled = false;
        }
    }
    public void LoadChosenLevel(int levelIndexToLoad) //loads the chosen level if needed with int condition so can choose scene to load
    {
        if (withTran) //with transition
        {
            StartCoroutine(LoadLevel(levelIndexToLoad)); 
        }

        else //immediate change
        {
            SceneManager.LoadScene(levelIndexToLoad);
        }
    }
    public void LoadNextLevel() //loads next level in build settings
    {
        if(withTran) //with transition
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); //retrieves the index of the current scene and adds 1, moving onto the next scene in build settings.
        }

        else //immediate change
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        }  
    }

    //COROUTINES LETS GOOOO

    IEnumerator LoadLevel(int levelIndex)
    {
        //play animation
        sceneTranAnimator.SetTrigger("Start"); //triggers the transition animation.

        yield return new WaitForSeconds(tranWaitTime);

        //LoadScene
        SceneManager.LoadScene(levelIndex); //goes to next level
    }
}
