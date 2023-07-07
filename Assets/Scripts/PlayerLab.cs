using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLab : MonoBehaviour
{ 
    PlayerController playerController;
    TestDisplay display;
    SpriteRenderer playerSprite;

    [SerializeField] GameObject DisplayCanvas;

    //This will all be dynamic values that will change accordingly during the LAB scene
    public int currentStatHealth;
    public float currentStatDmg;
    public float currentStatSpeed;
    public float currentStatShotSpeed;
    public float currentStatRange;
    public float currentStatSlimeRate;

   
    public bool isHit; //check if it's been hit
    float invisibileTime = 2f; //time for insibility
    private int attackedTimes; //for debug recording
    public bool isInvisable; //check if it's currently invisible
    public bool start; //this uhhh yea idk im just testing shit. This is supposed to be so that data is only set once when starting scene.

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        display = DisplayCanvas.GetComponent<TestDisplay>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isInvisable = false;
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            SetDataFromScript(); //just for setting stuff, nothing important i dont think
            start = false;
        }

    }

    //When enemy first collides with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //When first collided with enemy, it is attacked and will now initiate invisibility frames.
        if(collision.gameObject.tag == "Enemy")
        {
            if(!isInvisable)
            {
                isHitByEnemy(collision.gameObject); //registers hit
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if player is still being collided by enemy, it is attacked and will initiate invisibility frames.
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isInvisable)
            {
                isHitByEnemy(collision.gameObject); //registers hit
            }

        }
    }

    void isHitByEnemy(GameObject collided)
    {
        isHit = true;
        attackedTimes++; //for debug
        StartCoroutine(InvisibilityFrames());
        display.debug.text = "Hit " + attackedTimes.ToString() + " time(s)" + "\n" + "Hit by: " + collided.name + "\n"; //for debug

    }

    //initiates the invisibility
    IEnumerator InvisibilityFrames()
    {
        isInvisable = true;
        StartCoroutine(playInvis());

        yield return new WaitForSeconds(invisibileTime);
        
        isInvisable = false;
        StopCoroutine(playInvis()); 
    }

    //plays the blinking
    IEnumerator playInvis()
    {
        float delay = 0.3f;

        for(float n = 0; n < invisibileTime; n+=(delay*2))
        {
            yield return new WaitForSeconds(delay);
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0);
            yield return new WaitForSeconds(delay);
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1);

        }

    }


    void SetDataFromScript() //to get data from playercontroller so no code manipulation inside playercontroller values will probably change idk
    {
        currentStatHealth = playerController.baseStatHealth;
        currentStatDmg = playerController.baseStatDmg;
        currentStatSpeed = playerController.baseStatSpeed;
        currentStatShotSpeed = playerController.baseStatShotSpeed;
        currentStatRange = playerController.baseStatRange;
        currentStatSlimeRate = playerController.baseStateSlimeRate;

        if (display != null)
        {
            SetToDislay();
        }

    }   

    public void SetToDislay() //to show in the canvas for debugging (for now)
    {
        display.healthDisplay.text = currentStatHealth.ToString();
        display.damageDisplay.text = currentStatDmg.ToString();
        display.speedDisplay.text = currentStatSpeed.ToString();
        display.shotSpeedDisplay.text = currentStatShotSpeed.ToString();
        display.rangeDisplay.text = currentStatRange.ToString();
        display.slimeRateDisplay.text = currentStatSlimeRate.ToString();

    }
}
