using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string charId;
    public string currentAvatar;
    public int tempId;

    public int baseStatHealth;
    public float baseStatDmg;
    public float baseStatSpeed; //to test speed (default speed is 5) TO BE REF FROM JSON
    public float baseStatShotSpeed;
    public float baseStatRange;
    public float baseStateSlimeRate;

    public Rigidbody2D rb;

    public Animator slimeAnim;
    [SerializeField] AnimatorOverrideController greySlime;
    [SerializeField] AnimatorOverrideController redSlime;
    [SerializeField] AnimatorOverrideController greenSlime;
    [SerializeField] AnimatorOverrideController blueSlime;

    private Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        tempId = 1;
        SetCharacterStats();
    }

    // Update is called once per frame
    void Update()
    {
        SetCharacterStats();
        ProcessInputs();
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
<<<<<<< Updated upstream
=======

        //Testing arrow key direction shooting
        float shootX = Input.GetAxisRaw("ShootHorizontal");
        float shootY = Input.GetAxisRaw("ShootVertical");

       // Debug.Log("Horizontal shoot: " + shootX + ". Vertical shoot: " + shootY);

>>>>>>> Stashed changes
        
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
<<<<<<< Updated upstream
=======

    void chooseDirection(float updown, float leftright)
    {
        if (updown == 1 && leftright == 0) //shoot up (N)
        {
            Debug.Log("Shoot North");
        }
        else if (updown == 1 && leftright == 1) //shoot up right (NE)
        {
            Debug.Log("Shoot North East");
        }
        else if (updown == 0 && leftright == 1) //shoot right (E)
        {
            //Debug.Log("Shoot East");
        }
        else if (updown == -1 && leftright == 1) //shoot right down (SE)
        {
            Debug.Log("Shoot South East");
        }
        else if (updown == -1 && leftright == 0) //shoot down (S)
        {
            Debug.Log("Shoot South");
        }
        else if (updown == -1 && leftright == -1) //shoot left down (SW)
        {
            Debug.Log("Shoot South West");
        }
        else if (updown == 0 && leftright == -1) //shoot left (W)
        {
            Debug.Log("Shoot West");
        }
        else if ( updown == 1 && leftright == -1) //shoot left up (NW)
        {
            Debug.Log("Shoot North West");
        }
    }

>>>>>>> Stashed changes
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

    void SetCharacterStats() //better way to do this but this is used for testing. It probably wont be used since we have JSON.
    {
        switch (tempId)
        {
            case 1:
                baseStatHealth = 4;
                baseStatDmg = 4;
                baseStatSpeed = 4;
                slimeAnim.runtimeAnimatorController = greySlime;

                break;

            case 2:
                baseStatHealth = 3;
                baseStatDmg = 7;
                baseStatSpeed = 4;
                slimeAnim.runtimeAnimatorController = redSlime;

                break;

            case 3:
                baseStatHealth = 3;
                baseStatDmg = 2;
                baseStatSpeed = 6;
                slimeAnim.runtimeAnimatorController = blueSlime;

                break;

            case 4:
                baseStatHealth = 9;
                baseStatDmg = 5;
                baseStatSpeed = 3;
                slimeAnim.runtimeAnimatorController = greenSlime;
                break;

        }
    }
}
