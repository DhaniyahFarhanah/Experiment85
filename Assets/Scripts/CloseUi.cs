using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// eheh yea this just closes the ui is esc pressed :)
public class CloseUi : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
