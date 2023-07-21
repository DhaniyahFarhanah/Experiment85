using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//=========PLAYER LAB=============
//This information is seperated due to the temporary upgrades during a run.


public class PlayerLab : MonoBehaviour
{ 
    PlayerController playerController;
    TestDisplay display;
    SpriteRenderer playerSprite;

    //======For Display Testing====== TOBE DELETED
    [SerializeField] GameObject DisplayCanvas;

    public string Id;
    //This will all be dynamic values that will change accordingly during the LAB scene
    public int currentStatHealth;
    public float currentStatDmg;
    public float currentStatSpeed;
    public float currentStatShotSpeed;
    public float currentStatRange;
    public float currentStatSlimeRate;
    public string type;

    // =====MAX Values====
    public int MaxHealth;
    public float MaxDmg = 100f;
    public float MaxSpeed = 10f;
    public float MaxShotSpeed = 25f;
    public float MaxRange = 10f;
    public float MaxSlimeRate = 0.1f;

    // ====CONDITIONAl STUFF====
    float invisibileTime = 2f; //time for invisibility
    private int attackedTimes; //for debug recording (DEBUG)
    public bool isInvisable; //check if it's currently invisible (DEBUG)
    private float fireTimer; //check if shoot is on cooldown
    private bool phase;
    Collider2D slimeCollider;
    public bool disabled;

    //====Slimeball====
    [SerializeField] private GameObject slimeballPrefab;
    [SerializeField] private GameObject firepoint;
    private FireRotation fr; //the rotation for the bullet

    //====Analytic readings====
    public int shotsHit;
    private int shotsFired;

    public int enemiesDefeated;
    public int numOfBuffsTaken;
    public int amtOfDamageReceived;
    private int amtOfDamageDealt;
    private int numOfFailedAttempts;

    public int numOfBuff1Taken;
    public int numOfBuff2Taken;
    public int numOfBuff3Taken;
    public int numOfBuff4Taken;

    public int numOfEnemy1Killed;
    public int numOfEnemy2Killed;
    public int numOfEnemy3Killed;
    public int numOfEnemy4Killed;

    private int hitByEnemy1;
    private int hitByEnemy2;
    private int hitByEnemy3;
    private int hitByEnemy4;





    private void Awake()
    {
        slimeCollider = gameObject.GetComponent<CapsuleCollider2D>();
        fr = firepoint.GetComponent<FireRotation>();
        playerController = GetComponent<PlayerController>();
        display = DisplayCanvas.GetComponent<TestDisplay>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //restart values
        enemiesDefeated = 0;
        numOfBuffsTaken = 0;

        numOfEnemy1Killed = 0;
        numOfEnemy2Killed = 0;
        numOfEnemy3Killed = 0;
        numOfEnemy4Killed = 0;

        hitByEnemy1 = 0;
        hitByEnemy2 = 0;
        hitByEnemy3 = 0;
        hitByEnemy4 = 0;

        amtOfDamageDealt = 0;
        amtOfDamageReceived = 0;

        

        isInvisable = false;
        disabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Id != gameObject.GetComponent<PlayerController>().charId)
        {
            SetDataFromScript(); //just for setting stuff, nothing important i dont think
            Id = GameClass.GetCurrentSlimeId();
        }
        //round to 2dp
        currentStatDmg = Mathf.Round(currentStatDmg * 100f) * 0.01f;

        SetToDislay();
        CheckIfMax();

        if (!disabled)
        {
            ShootInput();

        }

        if(currentStatHealth <= 0)
        {
            Die();
        }

        //keep analytics through game
        
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

    void ShootInput() //shooting input
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

    void Shoot() //method to shoot slimeball
    {
        //if in lab, start recording
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            shotsFired++;
        }

        GameObject slimeball = Instantiate(slimeballPrefab, firepoint.transform.position, firepoint.transform.rotation);
        Slimeball setter = slimeball.GetComponent<Slimeball>();
        setter.speed = currentStatSpeed;
        setter.lifeTime = currentStatRange;
        setter.dmg = currentStatDmg;
        setter.type = type;
        SetColor(setter.sr);
    }

    void isHitByEnemy(GameObject collided) //if is hit, register (along with debug on the console)
    {
        StartCoroutine(InvisibilityFrames());
        attackedTimes++; //for debug

        string hitById = collided.GetComponent<EnemyController>().enemyId;

        switch (hitById)
        {
            case "E01": hitByEnemy1++; break;
            case "E02": hitByEnemy2++; break;
            case "E03": hitByEnemy3++; break;
            case "E04": hitByEnemy4++; break;
        }

        amtOfDamageReceived += collided.GetComponent<EnemyController>().enemyDamage;

        display.debug.text = "Hit " + attackedTimes.ToString() + " time(s)" + "\n" + "Hit by: " + collided.name + "\n"; //for debug

    }

    //initiates the invisibility
    IEnumerator InvisibilityFrames()
    {
        isInvisable = true;
        slimeCollider.enabled = false;
        StartCoroutine(playInvis());

        yield return new WaitForSeconds(invisibileTime);
        
        isInvisable = false;
        slimeCollider.enabled = true;
        StopCoroutine(playInvis()); 
    }

    //plays the blinking and invisible frames
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

    void Die()
    {
        GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().end = true;
        GameObject.FindGameObjectWithTag("WaveHandler").GetComponent<WaveHandler>().win = false;
        StopAllCoroutines();
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<PlayerController>().rb.velocity = Vector3.zero;

        //sets analytics
        SetPlayerLabAnalytics();

    }

    void CheckIfMax() //to make sure values have a cap
    {
        if (currentStatHealth > MaxHealth) //if health more than max
        {
            currentStatHealth = MaxHealth;
        }

        if (currentStatSlimeRate < MaxSlimeRate) //if slimerate less than max (cooldown)
        {
            currentStatSlimeRate = MaxSlimeRate;
        }

        if (currentStatDmg > MaxDmg) //if dmg more than max
        {
            currentStatDmg = MaxDmg;
        }

        if (currentStatShotSpeed > MaxShotSpeed) //if shotspeed more than max
        {
            currentStatShotSpeed = MaxShotSpeed;
        }

    }

    void SetColor(SpriteRenderer sr) //more for visual stuff
    {
        
        switch (Id)
        {
            case "S01": 
                sr.color = Color.white;
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

        Id = playerController.charId;
        currentStatHealth = playerController.baseStatHealth;
        currentStatDmg = playerController.baseStatDmg;
        currentStatSpeed = playerController.baseStatSpeed;
        currentStatShotSpeed = playerController.baseStatShotSpeed;
        currentStatRange = playerController.baseStatRange;
        currentStatSlimeRate = playerController.baseStateSlimeRate;
        type = playerController.type;

        MaxHealth = playerController.baseStatHealth;

        SetToDislay();

    }   

    void SetPlayerLabAnalytics()
    {
        AnalyticsHolder.Instance.slimeChosen = Id;
        AnalyticsHolder.Instance.numOfFailedAttempts = numOfFailedAttempts;

        AnalyticsHolder.Instance.damage = currentStatDmg;
        AnalyticsHolder.Instance.speed = currentStatSpeed;
        AnalyticsHolder.Instance.shotSpeed = currentStatShotSpeed;
        AnalyticsHolder.Instance.range = currentStatRange;
        AnalyticsHolder.Instance.slimeRate = currentStatSlimeRate;
        AnalyticsHolder.Instance.damageReceived = amtOfDamageReceived;
        AnalyticsHolder.Instance.damageDealt = amtOfDamageDealt;

        AnalyticsHolder.Instance.hitsTaken = attackedTimes;
        AnalyticsHolder.Instance.enemiesDefeated = enemiesDefeated;

        AnalyticsHolder.Instance.buffsPicked = numOfBuffsTaken;

        AnalyticsHolder.Instance.totalShots = shotsFired;
        AnalyticsHolder.Instance.shotsHit = shotsHit;
        AnalyticsHolder.Instance.shotsMissed = shotsFired - shotsHit;
        AnalyticsHolder.Instance.accuracy = ((shotsFired - shotsHit) / shotsFired) * 100f;

        AnalyticsHolder.Instance.takenNumOfBuff1 = numOfBuff1Taken;
        AnalyticsHolder.Instance.takenNumOfBuff2 = numOfBuff2Taken;
        AnalyticsHolder.Instance.takenNumOfBuff3 = numOfBuff3Taken;
        AnalyticsHolder.Instance.takenNumOfBuff4 = numOfBuff4Taken;

        AnalyticsHolder.Instance.killedNumOfEnemy1 = numOfEnemy1Killed;
        AnalyticsHolder.Instance.killedNumOfEnemy2 = numOfEnemy2Killed;
        AnalyticsHolder.Instance.killedNumOfEnemy3 = numOfEnemy3Killed;
        AnalyticsHolder.Instance.killedNumOfEnemy4 = numOfEnemy4Killed;

        AnalyticsHolder.Instance.hitByEnemy1 = hitByEnemy1;
        AnalyticsHolder.Instance.hitByEnemy2 = hitByEnemy2;
        AnalyticsHolder.Instance.hitByEnemy3 = hitByEnemy3;
        AnalyticsHolder.Instance.hitByEnemy4 = hitByEnemy4;
        AnalyticsHolder.Instance.mostHitId = FindMaxEnemy();

    }

    //yea im sorry im not manually using the lists my brain is fried
    string FindMaxEnemy()
    {
        int max = 0;
        string mostTakenId = "";
        int[] enemyHit = { hitByEnemy1, hitByEnemy2, hitByEnemy3, hitByEnemy4 };

        for (int i = 0; i < enemyHit.Length; i++)
        {
            if (max < enemyHit[i])
            {
                max = enemyHit[i];

                switch (i)
                {
                    case 0: mostTakenId = "E01"; break;
                    case 1: mostTakenId = "E02"; break;
                    case 3: mostTakenId = "E03"; break;
                    case 4: mostTakenId = "E04"; break;
                }
            }
        }

        return mostTakenId;

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
