using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Script done by: Gerald (Gerald Wei Jie SOH)

public class Options : MonoBehaviour
{
    public Dialogue dialogue;

    [SerializeField] GameObject ChoicePopUp;
    [SerializeField] public Button button1, button2;
    [SerializeField] public TMP_Text text1, text2;

    public string dialogue1, dialogue2;
    public int nextId1, nextId2;

    string[] container;
    char delimiter2 = '#';



    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(delegate { Op1Click(); });
        button2.onClick.AddListener(delegate { Op2Click(); });
        //button2.onClick.AddListener(delegate { ParameterOnClick("Button was pressed!"); });

        // GameObject.Find("Options").SetActive(true);
    }

    public void furtherSplit() //split by "#"
    {
        ChoicePopUp.SetActive(true);
        container = dialogue.option1.Split(delimiter2);
        dialogue1 = container[0];
        nextId1 = int.Parse(container[1]);
        text1.text = dialogue1;
        Array.Clear(container, 0, container.Length);
        container = dialogue.option2.Split(delimiter2);
        dialogue2 = container[0];
        text2.text = dialogue2;
        nextId2 = int.Parse(container[1]);
        //Debug.Log("dialogue1: " + dialogue1);
        //Debug.Log("nextId1: " + nextId1);
        //Debug.Log("dialogue2: " + dialogue2);
        //Debug.Log("nextId2: " + nextId2);
    }

    private void Op1Click()
    {
        ChoicePopUp.SetActive(false);
        dialogue.initDialogueID = nextId1;
        dialogue.nextID = nextId1 += 2;
        //Debug.Log("nextId3: " + dialogue.initDialogueID);
        //Debug.Log("nextId3: " + dialogue.nextID);
        dialogue.NextLine();
    }

    private void Op2Click()
    {
        ChoicePopUp.SetActive(false);
        dialogue.initDialogueID = nextId2;
        dialogue.nextID = nextId2 += 1;
        dialogue.NextLine();
    }

    private void ParameterOnClick(string test)
    {
        //Debug.Log("testtttett");
    }
}