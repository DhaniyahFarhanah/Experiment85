using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBack : MonoBehaviour
{
    [SerializeField] GameObject SceneManage;
    [SerializeField] int loseIndex;
    [SerializeField] int mainMenuIndex;

    AnalyticClass refClass = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetButtonDown("Enter"))
            {
                if(refClass.win == true)
                {
                    SceneManage.GetComponent<SceneLoader>().LoadChosenLevel(loseIndex);
                    SceneManage.GetComponent<SceneLoader>().LoadChosenLevel(mainMenuIndex);
                }
            }
        }
    }
}
