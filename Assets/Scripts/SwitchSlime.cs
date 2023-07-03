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


     int CharId = 1;
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
    }
    void Start()
    {
        CharId = 2;
        UpdateData(CharId);
        SetData();
    }

    // Update is called once per frame
    void Update()
    {
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
            playerController.charId = CharId;
            GameObject.Find("SlimeSelector").SetActive(false);
        }

    }

    void moveRight()
    {
        if(CharId == 3)
        {
            CharId = 1;
        }
        else
        {
            CharId++;
        }

        UpdateData(CharId);
        SetData();
    }
    void moveLeft()
    {
        if (CharId == 1)
        {
            CharId = 3;
        }
        else
        {
            CharId--;
        }

        UpdateData(CharId);
        SetData();
    }

    void UpdateData(int id)
    {
        Debug.Log(id);

        switch(id) //this will probably be where the json will come in but i'm doing this manually for testing 
        {
            case 1: charName = "Fire Slime";
                health = 3;
                damage = 7;
                speed = 4;
                break;
            case 2:charName = "Water Slime";
                health = 5;
                damage = 5;
                speed = 5;
                break;
            case 3: charName = "Forest Slime";
                health = 9;
                damage = 5;
                speed = 3;
                break;
        }
    }

    void SetData() //fills in all the changed data.
    {
        nameTextBox.text = charName;
        slimeShowcaseAnim.SetInteger("id", CharId);

        healthStat.toBeFilled = health;
        damageStat.toBeFilled = damage;
        speedStat.toBeFilled = speed;

    }
}
