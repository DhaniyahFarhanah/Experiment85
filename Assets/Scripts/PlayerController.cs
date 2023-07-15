using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string charId = "S01";

    //====Player Stats==== TOBE POPULATED BY JSON
    public int baseStatHealth;
    public float baseStatDmg;
    public float baseStatSpeed; 
    public float baseStatShotSpeed;
    public float baseStatRange;
    public float baseStateSlimeRate;

    //====Movement=====
    public Rigidbody2D rb;
    private Vector2 moveDir;

    //====JSON Lists====
    List<CharacterClass> characterList; //get character list from Json

    //=====Animators=====
    public Animator slimeAnim;
    [SerializeField] AnimatorOverrideController greySlime;
    [SerializeField] AnimatorOverrideController redSlime;
    [SerializeField] AnimatorOverrideController greenSlime;
    [SerializeField] AnimatorOverrideController blueSlime;

    //======Other stuff=====
    PlayerLab set;

    private void Awake()
    {
        set = gameObject.GetComponent<PlayerLab>();
    }

    // Start is called before the first frame update
    void Start()
    {
        charId = GameClass.GetCurrentSlimeId();
        SetCharacterStats();
        FillDataFromJson();
    }

    // Update is called once per frame
    void Update()
    {
        SetCharacterStats();
        ProcessInputs();
        FillDataFromJson();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("MoveHorizontal");
        float moveY = Input.GetAxisRaw("MoveVertical");

        moveDir = new Vector2(moveX, moveY).normalized; //No added speed when diagonal movement
    }

    void Movement()
    {
        rb.velocity = new Vector3(moveDir.x * baseStatSpeed, moveDir.y * baseStatSpeed);

        //OLD CODE - It was more flowy than static
        //Vector3 movement = new Vector3(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"), 0.0f);

        //Anim stuff
        slimeAnim.SetFloat("Horizontal", moveDir.x);
        slimeAnim.SetFloat("Vertical", moveDir.y);
        slimeAnim.SetFloat("Magnitude", moveDir.magnitude);

        //Register Movement
        //transform.position = transform.position + movement * Time.deltaTime * charSpeed;
    }

    void SetCharacterStats() //Gets list of characters by Json
    {
        characterList = GameData.GetCharacterList();
    }


    public void FillDataFromJson()
    {

        foreach(CharacterClass c in characterList)
        {
            if(c.charId == charId)
            {
                //populate if the charId matches
                baseStatHealth = c.baseStatHealth;
                baseStatDmg = c.baseStatDmg;
                // >:C
                baseStatSpeed = c.baseStatSpeed * 1.5f;
                baseStatShotSpeed = c.baseStatShotSpeed;
                baseStatRange = c.baseStatRange;
                baseStateSlimeRate = c.baseStateSlimeRate;
            }
          
        }

        GameClass.SetCurrentSlimeId(charId);

        SetPlayerLab(); //this is to set the info into PlayerLab script whenever a change is made.

        switch (charId) //animator stuff
        {
            case "S01": slimeAnim.runtimeAnimatorController = greySlime;
                break;
            case "S02": slimeAnim.runtimeAnimatorController = redSlime;
                break;
            case "S03": slimeAnim.runtimeAnimatorController = blueSlime;
                break;
            case "S04": slimeAnim.runtimeAnimatorController = greenSlime;
                break;
        }
        
    }

    //seperated due to upgrades adding more stats along with the temporary upgrades that shouldn't manipulate with the base values.
    void SetPlayerLab()
    {

        set.Id = charId;
        set.currentStatHealth = baseStatHealth;
        set.currentStatDmg = baseStatDmg;
        set.currentStatSpeed = baseStatSpeed;
        set.currentStatShotSpeed = baseStatShotSpeed;
        set.currentStatRange = baseStatRange;
        set.currentStatSlimeRate = baseStateSlimeRate;

    }

}
