using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// little thing. If its still a lose loop, it will remember that the character has already activated the first talk and won't pop up when in lose loop.
public class DialogueUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameClass.HasFirstTalk())
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
