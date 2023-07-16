using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string currentLevel;

    [SerializeField] private GameObject greyLevel;
    [SerializeField] private GameObject greenLevel;
    [SerializeField] private GameObject blueLevel;
    [SerializeField] private GameObject redLevel;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = GameClass.GetCurrentLevelId();
    }

    // Update is called once per frame
    void Update()
    {
        LevelLoader();
    }

    void LevelLoader()
    {
        switch (currentLevel)
        {
            case "greenKey":
                greyLevel.SetActive(false);
                greenLevel.SetActive(true);
                blueLevel.SetActive(false);
                redLevel.SetActive(false);
                break;

            case "blueKey":
                greyLevel.SetActive(false);
                greenLevel.SetActive(false);
                blueLevel.SetActive(true);
                redLevel.SetActive(false);
                break;

            case "redKey":
                greyLevel.SetActive(false);
                greenLevel.SetActive(false);
                blueLevel.SetActive(false);
                redLevel.SetActive(true);
                break;

            default: //grey
                greyLevel.SetActive(true);
                greenLevel.SetActive(false);
                blueLevel.SetActive(false);
                redLevel.SetActive(false);
                break;
        }
    }
}
