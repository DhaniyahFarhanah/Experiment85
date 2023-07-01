using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float charSpeed = 5f; //to test speed (default speed is 5) TO BE REF FROM JSON
    public Rigidbody2D rb;

    public Animator slimeAnim;

    private Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        rb.velocity = new Vector3(moveDir.x * charSpeed, moveDir.y * charSpeed);

        //OLD CODE - It was more flowy than static
        //Vector3 movement = new Vector3(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"), 0.0f);

        //Anim stuff
        slimeAnim.SetFloat("Horizontal", moveDir.x);
        slimeAnim.SetFloat("Vertical", moveDir.y);
        slimeAnim.SetFloat("Magnitude", moveDir.magnitude);

        //Register Movement
        //transform.position = transform.position + movement * Time.deltaTime * charSpeed;
    }
}
