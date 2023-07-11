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

    List<CharacterClass> characterList;
    int index = 0;


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
        UpdateData();
        SetData();
        
    }

    // Update is called once per frame
    void Update()
    {
        slimeShowcaseAnim.SetInteger("id", index);
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
            playerController.charId = characterList[index].charId;
            
            GameObject.Find("SlimeSelector").SetActive(false);
        }

    }

    void moveRight()
    {
        index++;
        if(index >= characterList.Count)
        {
            index = 0;
        }

        SetData();
    }
    void moveLeft()
    {
        index--;
        if(index < 0)
        {
            index = characterList.Count - 1;
        }

        SetData();
    }

    void UpdateData()
    {
        //TODO revise this (it only has to happen once i think
        characterList = GameData.GetCharacterList();

    }

   
    void SetData() //fills in all the changed data.
    {
        //populate into private variable
        charName = characterList[index].charName;
        health = characterList[index].baseStatHealth;
        damage = (int)characterList[index].baseStatDmg;
        speed = (int)characterList[index].baseStatSpeed;


        //populate into the canvas
        nameTextBox.text = charName;
        slimeShowcaseAnim.SetInteger("id", index);

        healthStat.toBeFilled = health;
        damageStat.toBeFilled = damage;
        speedStat.toBeFilled = speed;

    }
}
