using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int charId;

    public int maxHealth;
    public int basedamage;
    public float basecharSpeed; //to test speed (default speed is 5) TO BE REF FROM JSON
    public int baseshotSpeed;
    public int baserange;
    public int baseprojectileRate;

    public Rigidbody2D rb;

    public Animator slimeAnim;
    [SerializeField] AnimatorOverrideController redSlime;
    [SerializeField] AnimatorOverrideController greenSlime;
    [SerializeField] AnimatorOverrideController blueSlime;

    private Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        charId = 2;
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
        rb.velocity = new Vector3(moveDir.x * basecharSpeed, moveDir.y * basecharSpeed);

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
        switch (charId)
        {
            case 1:
                maxHealth = 3;
                basedamage = 7;
                basecharSpeed = 4;
                slimeAnim.runtimeAnimatorController = redSlime;

                break;
            case 2:
                maxHealth = 5;
                basedamage = 5;
                basecharSpeed = 5;
                slimeAnim.runtimeAnimatorController = blueSlime;

                break;
            case 3:
                maxHealth = 9;
                basedamage = 5;
                basecharSpeed = 3;
                slimeAnim.runtimeAnimatorController = greenSlime;

                break;
        }
    }
}
