using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public string init;

    // Start is called before the first frame update
    void Start()
    {
        DataManager dataManager = GetComponent<DataManager>();
        dataManager.LoadRefData();

        Game.SetPlayer(new Player("1", init));
        Debug.Log(Game.GetPlayer().GetCharacterId());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
