using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// Buff script is attached to Buff Prefab and populates the correct values and manipulates the correct values taken from JSON and scripts

public class BuffScript : MonoBehaviour
{

    //====Json list====
    private List<BuffClass> buffList;

    [SerializeField] private GameObject Description;
    [SerializeField] private TMP_Text nameTextBox;
    [SerializeField] private TMP_Text descriptionTextBox;

    private GameObject player;

    //===stats from JSON====
    public string buffId = "buff1";
    private string buffName;
    private string stat;
    private float value;
    private string buffDescription;

    private bool canPickUp = false;

    private float delayTime;
    private float time = 6f;
    private bool blinkPlaying;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        buffList = GameData.GetBuffList();
        FindInJson();

        delayTime = time;
        canPickUp = false;
        
        ChangeSprite();
        PopulateCanvas();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (canPickUp) //allows pickup when in range
        {
            if (Input.GetButtonDown("Interact")) //on enter press
            {
                GiveBuff();

                switch (buffId) //analytics stuff
                {
                    case "buff1": player.GetComponent<PlayerLab>().numOfBuff1Taken++; break;
                    case "buff2": player.GetComponent<PlayerLab>().numOfBuff2Taken++; break;
                    case "buff3": player.GetComponent<PlayerLab>().numOfBuff3Taken++; break;
                    case "buff4": player.GetComponent<PlayerLab>().numOfBuff4Taken++; break;
                }
            }
        }

        delayTime -= Time.deltaTime;

        if(delayTime < time / 2) //plays blinking animation if about to disappear
        {
            if (!blinkPlaying)
            {
                StartCoroutine(Blink());
            }
        }

        if(delayTime < 0) //destroy if times up
        {
            Destroy(gameObject);
        }
        
    }

    void FindInJson() //populates values with the json 
    {
        foreach(BuffClass b in buffList)
        {
            if(buffId == b.buffId)
            {
                buffId = b.buffId;
                buffName = b.buffName;
                stat = b.stat;
                value = b.value;
                buffDescription = b.buffDescription;
            }
        }
    }

    void ChangeSprite() //changes the sprite color because no custom sprites :C
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        switch (buffId)
        {
            case "buff1" : sr.color = Color.green;
                break;
            case "buff2": sr.color = Color.blue;
                break;
            case "buff3": sr.color = Color.red;
                break;
            case "buff4": sr.color = Color.yellow;
                break;
            default: sr.color = Color.white;
                break;
        }
    }

    IEnumerator Blink() //plays blink manually
    {
        blinkPlaying = true;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.5f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f);
            yield return new WaitForSeconds(0.5f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
    }

    void PopulateCanvas() //populates the canvas with a description and name
    {
        nameTextBox.text = buffName;
        descriptionTextBox.text = buffDescription;
    }

    void GiveBuff() //gives the buff to player
    {
        PlayerLab playerStat = player.GetComponent<PlayerLab>();
        playerStat.numOfBuffsTaken++; //Analytics

        //wanted to have a warning like "stat at max value" and don't allow pick up but uhhhh lazy
        switch (stat)
        {
            case "baseStatHealth": 

                if(playerStat.currentStatHealth < playerStat.MaxHealth)
                {
                    playerStat.currentStatHealth += (int) value; 
                }

                break;

            case "baseStateSlimeRate": 
                
                if(playerStat.currentStatSlimeRate > playerStat.MaxSlimeRate)
                {
                    playerStat.currentStatSlimeRate -= value;
                }
                break;

            case "baseStatDmg": 

                if (playerStat.currentStatDmg < playerStat.MaxDmg)
                {
                    playerStat.currentStatDmg += value;

                }
                break;

            case "baseStatShotSpeed":

                if (playerStat.currentStatShotSpeed < playerStat.MaxShotSpeed)
                {
                    playerStat.currentStatShotSpeed += value;

                }
                
                break;
        }

        Destroy(gameObject); //if picked up, destroy it
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Buff"))
        {
            canPickUp = true; //allows pick up in UPDATE
            Description.SetActive(true); //description shows
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Buff"))
        {
            canPickUp = false; //don't allow pick up 
            Description.SetActive(false); //dont show the description
        }
    }


}
