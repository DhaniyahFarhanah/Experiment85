using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// changes projectile direction for 8 dimentional shooting.
public class FireRotation : MonoBehaviour
{
    private float angle;
    private float shootX;
    private float shootY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootX = Input.GetAxisRaw("ShootHorizontal");
        shootY = Input.GetAxisRaw("ShootVertical");

    }


    public void SetDirection(float updown, float leftright)
    {
        if (updown == 1 && leftright == 0) //shoot up (N)
        {
            angle = 0;
            
        }
        else if (updown == 1 && leftright == 1) //shoot up right (NE)
        {
            angle = 315f;

        }
        else if (updown == 0 && leftright == 1) //shoot right (E)
        {
            angle = 270f;

        }
        else if (updown == -1 && leftright == 1) //shoot right down (SE)
        {
            angle = 215f;

        }
        else if (updown == -1 && leftright == 0) //shoot down (S)
        {
            angle = 180f;

        }
        else if (updown == -1 && leftright == -1) //shoot left down (SW)
        {
            angle = 135f;

        }
        else if (updown == 0 && leftright == -1) //shoot left (W)
        {
            angle = 90f;

        }
        else if (updown == 1 && leftright == -1) //shoot left up (NW)
        {
            angle = 45f;

        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
