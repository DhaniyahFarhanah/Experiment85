using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLab : MonoBehaviour
{ 
    PlayerController playerController;
    TestDisplay display;

    [SerializeField] GameObject DisplayCanvas;

    //This will all be dynamic values that will change accordingly during the LAB scene
    public int currentStatHealth;
    public float currentStatDmg;
    public float currentStatSpeed;
    public float currentStatShotSpeed;
    public float currentStatRange;
    public float currentStatSlimeRate;

    public bool start; //this uhhh yea idk im just testing shit. This is supposed to be so that data is only set once when starting scene.

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        display = DisplayCanvas.GetComponent<TestDisplay>();
    }

    // Start is called before the first frame update
    void Start()
    {
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            SetDataFromScript();
            start = false;
        }
    }

    void SetDataFromScript() //to get data from playercontroller so no code manipulation inside playercontroller values
    {
        currentStatHealth = playerController.baseStatHealth;
        currentStatDmg = playerController.baseStatDmg;
        currentStatSpeed = playerController.baseStatSpeed;
        currentStatShotSpeed = playerController.baseStatShotSpeed;
        currentStatRange = playerController.baseStatRange;
        currentStatSlimeRate = playerController.baseStateSlimeRate;

        SetToDislay();
    }   

    void SetToDislay() //to show in the canvas for debugging (for now)
    {
        display.healthDisplay.text = currentStatHealth.ToString();
        display.damageDisplay.text = currentStatDmg.ToString();
        display.speedDisplay.text = currentStatSpeed.ToString();
        display.shotSpeedDisplay.text = currentStatShotSpeed.ToString();
        display.rangeDisplay.text = currentStatRange.ToString();
        display.slimeRateDisplay.text = currentStatSlimeRate.ToString();

    }
}
