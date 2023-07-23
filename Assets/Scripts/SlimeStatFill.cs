using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)
// this is to control the states of the boxes from the slime selector.
public class SlimeStatFill : MonoBehaviour
{
    [SerializeField] GameObject[] statFill;
    [SerializeField] Sprite statFilled;
    [SerializeField] Sprite statEmpty;

    public bool changed; //variable to only update when slime is changed
    public int toBeFilled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetFill();
    }

    //Set fill will look through the amount of gameobject boxes. 
    //eg stattofill = 8, it will change the sprite to fill and the rest to empty.

    public void SetFill() 
    {
        for (int i = 0; i < statFill.Length; i++)
        {
            if(i < toBeFilled)
            {
                statFill[i].GetComponent<Image>().sprite = statFilled;
            }
            else
            {
                statFill[i].GetComponent<Image>().sprite = statEmpty;
            }
        }

    }
}
