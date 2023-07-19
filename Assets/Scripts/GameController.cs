using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool gameInProgress;
    public float totalTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        gameInProgress = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameInProgress)
        {
            totalTime += Time.deltaTime;
        }
        else
        {
            Debug.Log("Time Taken: " + totalTime);
        }
        
    }
}
