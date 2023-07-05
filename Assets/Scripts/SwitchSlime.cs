using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSlime : MonoBehaviour
{
    [SerializeField] TMP_Text nameTextBox;
    [SerializeField] Animator slimeShowcaseAnim;
    [SerializeField] Animator RightArrow;
    [SerializeField] Animator LeftArrow;

    [SerializeField] GameObject healthStatusBar;
    [SerializeField] GameObject damageStatusBar;
    [SerializeField] GameObject speedStatusBar;

    PlayerController playerController;
    SlimeStatFill healthStat;
    SlimeStatFill damageStat;
    SlimeStatFill speedStat;


     string CharId;
    int tempId;
     string charName;
     int health;
     int damage;
     int speed;


    // Start is called before the first frame update

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthStat = healthStatusBar.GetComponentInChildren<SlimeStatFill>();
        damageStat = damageStatusBar.GetComponentInChildren<SlimeStatFill>();
        speedStat = speedStatusBar.GetComponentInChildren<SlimeStatFill>();
        tempId = 1;
    }
    void Start()
    {
        UpdateData(tempId);
        SetData();
        
    }

    // Update is called once per frame
    void Update()
    {
        slimeShowcaseAnim.SetInteger("id", tempId);
        InputHandler();
    }

    void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) //change to next
        {
            moveRight();
            RightArrow.SetTrigger("Next");  
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) //change to previous
        {
            moveLeft();
            LeftArrow.SetTrigger("Prev");
        }

        if (Input.GetButton("Choose")) //confirms
        {
            Debug.Log("Chosen");
            playerController.tempId = tempId;
            
            GameObject.Find("SlimeSelector").SetActive(false);
        }

    }

    void moveRight()
    {
        if(tempId == 4)
        {
            tempId = 1;
        }
        else
        {
            tempId++;
        }

        UpdateData(tempId);
        SetData();
    }
    void moveLeft()
    {
        if (tempId == 1)
        {
            tempId = 4;
        }
        else
        {
            tempId--;
        }

        UpdateData(tempId);
        SetData();
    }

    void UpdateData(int id)
    {
        Debug.Log(id);

        switch(id) //this will probably be where the json will come in but i'm doing this manually for testing 
        {
            case 1: charName = "85";
                health = 4;
                damage = 4;
                speed = 4;
                break;

            case 2: charName = "Fire Slime";
                health = 3;
                damage = 7;
                speed = 4;
                break;
            case 3:charName = "Water Slime";
                health = 3;
                damage = 2;
                speed = 6;
                break;
            case 4: charName = "Forest Slime";
                health = 9;
                damage = 5;
                speed = 3;
                break;
        }
    }

    void SetData() //fills in all the changed data.
    {
        nameTextBox.text = charName;
        slimeShowcaseAnim.SetInteger("id", tempId);

        healthStat.toBeFilled = health;
        damageStat.toBeFilled = damage;
        speedStat.toBeFilled = speed;

    }
}
