using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
