using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// Script done by: Gerald (Gerald Wei Jie SOH)
// Game Controller just makes it a don't destroy on load.

public class GameController : MonoBehaviour
{
    private bool gameInProgress;
    private int hour;
    private int minute;
    private int second;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        gameInProgress = true;
    }

    // Update is called once per frame
    void Update()
    { 
        //records time that the game ran for since main menu.
        if (gameInProgress)
        {
            //record time elapsed. Analytics
            hour = (int) Time.unscaledTime / 3600;
            minute = (int) (Time.unscaledTime % 3600) / 60;
            second = (int) Time.unscaledTime % 60;

            AnalyticsHolder.Instance.timeElapsed = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
        }
        
    }
}
