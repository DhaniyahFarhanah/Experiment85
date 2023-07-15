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

    //======For Display Testing======
    [SerializeField] GameObject DisplayCanvas;

    public string Id;
    //This will all be dynamic values that will change accordingly during the LAB scene
    public int currentStatHealth;
    public float currentStatDmg;
    public float currentStatSpeed;
    public float currentStatShotSpeed;
    public float currentStatRange;
    public float currentStatSlimeRate;

    // ====CONDITIONAl STUFF====
    public bool isHit; //check if it's been hit
    float invisibileTime = 2f; //time for invisibility
    private int attackedTimes; //for debug recording (DEBUG)
    public bool isInvisable; //check if it's currently invisible (DEBUG)
    private float fireTimer; //check if shoot is on cooldown

    //====Slimeball====
    [SerializeField] private GameObject slimeballPrefab;
    [SerializeField] private GameObject firepoint;
    private FireRotation fr;

    private void Awake()
    {
        fr = firepoint.GetComponent<FireRotation>();
        playerController = GetComponent<PlayerController>();
        display = DisplayCanvas.GetComponent<TestDisplay>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isInvisable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Id != gameObject.GetComponent<PlayerController>().charId)
        {
            SetDataFromScript(); //just for setting stuff, nothing important i dont think
        }

        ShootInput();
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

    void ShootInput()
    {
        float shootX = Input.GetAxisRaw("ShootHorizontal");
        float shootY = Input.GetAxisRaw("ShootVertical");

        fr.SetDirection(shootY, shootX);

        if ((shootX != 0 || shootY != 0) && fireTimer <= 0) //is shooting
        {
            
            Shoot();
            fireTimer = currentStatSlimeRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject slimeball = Instantiate(slimeballPrefab, firepoint.transform.position, firepoint.transform.rotation);
        Slimeball setter = slimeball.GetComponent<Slimeball>();
        setter.speed = currentStatSpeed;
        setter.lifeTime = currentStatRange;
        setter.dmg = currentStatDmg;
        SetColor(setter.sr);
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
            playerSprite.color = new Color(255, 0, 0, 0.5f);
            yield return new WaitForSeconds(delay);
            playerSprite.color = new Color(255, 255, 255, 1);

        }

    }

    void SetColor(SpriteRenderer sr)
    {
        
        switch (Id)
        {
            case "S01": 
                sr.color = Color.gray;
                break;
            case "S02":
                sr.color = Color.red;
                break;
            case "S03":
                sr.color = Color.blue;
                break;
            case "S04":
                sr.color = Color.green;
                break;
        }

    }

    public void SetDataFromScript() //to get data from playercontroller so no code manipulation inside playercontroller values will probably change idk
    {
        playerController.FillDataFromJson();
        Id = playerController.charId;
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
