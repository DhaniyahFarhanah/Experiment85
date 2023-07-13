using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public string TXT;
    public float textSpeed;

    public int dialogueId;
    public int nextCutsceneRefId;
    public string currentSpeaker;
    public int npcId;
    public string dialogue;

    private int index;

    List<DialogueClass> dialogueList; //get dialogue list from Json


    // Start is called before the first frame update
    void Start()
    {
        dialogueList = GameData.GetDialogueList();
        textComponent.text = string.Empty;
        startDialogue();
        FillDataFromJson();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (textComponent.text == lines[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        
    }

        void startDialogue()
    {
        index = 0;
        StartCoroutine(typeLine());
    }

    IEnumerator typeLine()
    {
        //type each character 1 by 1
        foreach (char c in lines[index]) //convert string array to character array
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void FillDataFromJson()
    {
        //foreach (DialogueClass d in dialogueList)
        //{
        //    if (d.dialogueId == dialogueId)
        //    {
        //        TXT = d.dialogue;
        //    }
        //}
        Debug.Log("D_List: " + dialogueList.Count);
    }

        void nextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(typeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
