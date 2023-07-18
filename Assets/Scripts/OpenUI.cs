using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeField] GameObject Interact;
    [SerializeField] GameObject UI;

    public Dialogue dialogue;
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
                Debug.Log("Interacting");

                if (this.gameObject.name == "Sensor1")
                {
                    dialogue.initDialogueID = 101001;
                    dialogue.nextID = 101002;
                    dialogue.StartFirstDialogue();
                }
                if (this.gameObject.name == "Sensor2")
                {
                    dialogue.initDialogueID = 103001;
                    dialogue.nextID = 103002;
                    dialogue.StartFirstDialogue();
                }

                if (this.gameObject.name == "Sensor3")
                {
                    dialogue.initDialogueID = 105001;
                    dialogue.nextID = 105002;
                    dialogue.StartFirstDialogue();
                }
                UI.SetActive(true);
                canInteract = false;
            }
        }

    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Interact with player");
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
