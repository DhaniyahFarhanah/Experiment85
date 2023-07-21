using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Dialogue : MonoBehaviour
{
    public Options optionsScript;

    [SerializeField] public GameObject npcImage;
    public Image slimeImage, NPCTranslucent, SlimeTranslucent;

    public TextMeshProUGUI textComponent;
    public string TXT, currentSpeaker;
    private float textSpeed;
    private bool textFinished;
    public int imageId = 3;

    [SerializeField] TMP_Text nameText;

    public string option1, option2;
    public int initDialogueID, nextID;
    List<int> firstDialogues = new List<int>();

    private int dialogueId;
    private int nextCutsceneRefId;
    private int npcId;
    private string dialogue;
    private GameObject NPC;

    List<DialogueClass> dialogueList; //get dialogue list from Json
    List<NPCClass> npcList; //get npc list from Json

    // Start is called before the first frame update
    void Start()
    {
        npcList = GameData.GetNPCList();
        dialogueList = GameData.GetDialogueList();
        textComponent.text = string.Empty;
        initDialogueID = 101001; // start the first dialogue
        nextID = 101002;
        StartFirstDialogue();
        StoreFirstDialogues();
    }

    // Update is called once per frame
    void Update()
    {
        changeNPC();
        changeSpeaker();

        if (Input.GetKeyDown(KeyCode.RightArrow) && textFinished) //checking for right arrow
        {
            NextLine();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("Dialogue").SetActive(false);
        }
    }

    private void Close()
    {
        GameClass.SetHasFirstTalk(true);
        GameObject.Find("Dialogue").SetActive(false);
        textComponent.text = string.Empty;
    }

    void StoreFirstDialogues() //Store the first dialogue IDs from NPC list
    {
        foreach (NPCClass n in npcList)
        {
            firstDialogues.Add(n.dialogueId);
        }
    }

    IEnumerator typeLine() //type each character 1 by 1
    {
        textComponent.text = string.Empty;
        textFinished = false;
        foreach (char c in TXT)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;

        initDialogueID += 1;
        nextID += 1;
    }

    public void StartFirstDialogue() // search for the first dialogue based on the initDialogueID then store into TXT
    {
        this.gameObject.SetActive(true);
        foreach (int item in firstDialogues)
        {
            if (initDialogueID == item)
            {
                initDialogueID = item;
                //nextID += 1;

                firstDialogues.Remove(item);
                break;
            }
        }
        NextLine();
    }

    public void changeSpeaker() //highlight the current speaker
    {
            if (currentSpeaker == "Right")
            {
                NPCTranslucent.enabled = true;
                SlimeTranslucent.enabled = false;
            }
            if (currentSpeaker == "Left")
            {
                NPCTranslucent.enabled = false;
                SlimeTranslucent.enabled = true;
            }
    }

    public void NextLine()
    {
        char delimiter1 = '@';
        string[] container;
        //Debug.Log("dialogueID: " + initDialogueID);
        //Debug.Log("nextCutsceneRefId: " + nextID);
        foreach (DialogueClass d in dialogueList) // search the whole list for ID
        {
            if (d.dialogueId == initDialogueID && d.nextCutsceneRefId == nextID) //regular conversation
            {
                currentSpeaker = d.currentSpeaker;
                imageId = d.npcId;
                TXT = d.dialogue;
                StartCoroutine(typeLine());
                //Debug.Log("NextLine" + d.dialogue);

            }
            if (nextID - initDialogueID == 2) //normalize
            {
                currentSpeaker = d.currentSpeaker;
                imageId = d.npcId;
                initDialogueID += 2;
                nextID += 1;
                //Debug.Log("Conversation 1");
                continue;
            }
            if (d.dialogueId == initDialogueID && d.nextCutsceneRefId == -1) //options
            {
                currentSpeaker = d.currentSpeaker;
                imageId = d.npcId;
                container = d.dialogue.Split(delimiter1);
                option1 = container[0];
                option2 = container[1];
                //Debug.Log("option 1: " + option1);
                //Debug.Log("option 2: " + option2);
                optionsScript.furtherSplit();
                //textFinished = false
            }
            if (d.dialogueId == initDialogueID && d.nextCutsceneRefId == -2) //end dialogue
            {
                currentSpeaker = d.currentSpeaker;
                imageId = d.npcId;
                TXT = d.dialogue;
                StartCoroutine(typeLine());
                //Debug.Log("Dialogue(1): " + TXT);
                //Debug.Log("ID(3): " + initDialogueID);
                //Debug.Log("ID(4): " + nextID);
                Invoke("Close", 2.0f);
            }
 

        }
    }

    public void changeNPC()
    {
        foreach (NPCClass n in npcList) // search the whole list for ID
        {
            if (n.npcId == imageId)
            {
                nameText.text = n.npcName;
                AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(n.npcName);
                npcImage.GetComponent<Image>().sprite = handle.Result;
            }
        }

    }



}