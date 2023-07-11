using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string charId = "S01";

    public int baseStatHealth;
    public float baseStatDmg;
    public float baseStatSpeed; //to test speed (default speed is 5) TO BE REF FROM JSON
    public float baseStatShotSpeed;
    public float baseStatRange;
    public float baseStateSlimeRate;

    public Rigidbody2D rb;

    List<CharacterClass> characterList; //get character list from Json

    public Animator slimeAnim;
    [SerializeField] AnimatorOverrideController greySlime;
    [SerializeField] AnimatorOverrideController redSlime;
    [SerializeField] AnimatorOverrideController greenSlime;
    [SerializeField] AnimatorOverrideController blueSlime;

    private Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        charId = "S01";
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
        
        //Testing arrow key direction shooting
        if (Input.GetButton("FireUp"))
        {
            Debug.Log("Shooting Upwards");
        }
        else if (Input.GetButton("FireDown"))
        {
            Debug.Log("Shooting Downwards");
        }
        else if (Input.GetButton("FireLeft"))
        {

            Debug.Log("Shooting Left");
        }
        else if (Input.GetButton("FireRight"))
        {
            Debug.Log("Shooting Right");
            
        }

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

    void FillDataFromJson()
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

        switch (charId)
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
}
