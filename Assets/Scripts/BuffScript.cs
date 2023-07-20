using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffScript : MonoBehaviour
{
    //====Json list====
    private List<BuffClass> buffList;

    [SerializeField] private GameObject Description;
    [SerializeField] private TMP_Text nameTextBox;
    [SerializeField] private TMP_Text descriptionTextBox;

    private GameObject player;

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
        
        if (canPickUp)
        {
            if (Input.GetButtonDown("Interact"))
            {
                GiveBuff();
            }
        }

        delayTime -= Time.deltaTime;
        if(delayTime < time / 2)
        {
            if (!blinkPlaying)
            {
                StartCoroutine(Blink());
            }
        }

        if(delayTime < 0)
        {
            Destroy(gameObject);
        }
        
    }

    void FindInJson()
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

    void ChangeSprite()
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

    IEnumerator Blink()
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

    void PopulateCanvas()
    {
        nameTextBox.text = buffName;
        descriptionTextBox.text = buffDescription;
    }

    void GiveBuff()
    {
        PlayerLab playerStat = player.GetComponent<PlayerLab>();

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

            case "baseStatDmg": playerStat.currentStatDmg += value;

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

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Buff"))
        {
            canPickUp = true;
            Description.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Buff"))
        {
            canPickUp = false;
            Description.SetActive(false);
        }
    }


}
