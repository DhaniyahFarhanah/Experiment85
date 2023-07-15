using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeField] GameObject Interact;
    [SerializeField] GameObject UI;

    bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if (Input.GetButton("Interact"))
            {
                UI.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Interact.SetActive(true);
            canInteract = true;

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Interact.SetActive(false);
            canInteract = false;
            
        }
    }
}
