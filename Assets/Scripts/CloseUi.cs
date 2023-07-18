using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
