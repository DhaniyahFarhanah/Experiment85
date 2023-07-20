using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

//I finally got hit with the existential crisis...
public class WaveHandler : MonoBehaviour
{
    //=====Json Lists=====
    private List<WaveClass> waveJsonList;
    //Need enemy json


    //=====JSON VAR=====
    private int waveId;
    private int waveNo;
    private int nextWave;
    private string enemyId;

    //for split string
    public class EnemyWaveList
    {
        public string enemyWaveId;
        public int qnty;
    }

    public List<EnemyWaveList> enemyList = new List<EnemyWaveList>();

    //=====Other Variables=====
    private float waveTime = 15f; //time limit for each wave
    private float currentWaveTime; //current wave time
    private int currentLevel; //current level to check with json
    private int maxWaveNo; //find from json
    private bool end = false;


    //====Display Stuff=====
    [SerializeField] GameObject waveOverlay;
    LabOverlay overlay;

    //=====Enemy Spawning Stuff====
    [SerializeField] Transform[] spawnPoint;
    //is there a way to make this more scalable? MAYBE!! I DUNNO!!
    //okay maybe in...enemy script? can it even like- actually i dont think so...maybe...an array of gameobjects and then check enemyscript's id?
    //and instantiate the uhhhhhh the prefab with the enemyscript associated with the enemyId? MAN IDK 
    [SerializeField] GameObject[] enemyPrefabs;


    private void Awake()
    {
        waveId = 101; //set to first always. Can use dynamic 
        maxWaveNo = 1;
        waveJsonList = GameData.GetWaveList();
        overlay = waveOverlay.GetComponent<LabOverlay>();
        GetFromJson();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        currentWaveTime = waveTime; //set the first wave time to the time limit for each wave
        maxWaveNo = waveJsonList.Count;
        
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(!(waveNo > maxWaveNo))
        {
            currentWaveTime -= Time.deltaTime;

        }
        if (!end)
        {
            SetOverlayDisplay();

            NextWave();

        }

        if ((GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) && (nextWave == -2)) //win condition
        {
            EndRound();
            //set analytics
        }
    }


    //Sets Overlay values
    private void SetOverlayDisplay()
    {
        overlay.waveTextBox.text = "WAVE " + waveNo.ToString() + "/" + maxWaveNo.ToString();
        overlay.timerBar.fillAmount = currentWaveTime/waveTime;

        overlay.timeElapsed.text = GameObject.FindGameObjectsWithTag("Enemy").Length.ToString() + " enemies left";

    }

    void NextWave()
    {
        if (currentWaveTime < 0)
        {
            
            currentWaveTime = waveTime; //restart wave counter
            //move to next wave
            //check if the next wave is the same as the current wave. if not, move on to next wave 
            if (waveId != nextWave)
            {
                if(nextWave == -1)
                {
                    //WinRound();
                    //check if its condition to leave
                    //This code is made like this due to the possible future implementation if we pass lol
                    //-1 is supposed to stop and allow the character to move back to the safe zone and do some upgardes but with the cutting corners
                    //But scrapped due to time and the removal of inventory and upgrades :C
                    waveId = (waveId / 100) * 100 + 101;
                    GetFromJson();
                    SpawnWave();
                    Debug.Log("Stop Wave");
                }

                else
                {
                    waveId = nextWave;
                   
                    GetFromJson(); //populate with new reading
                    SpawnWave(); //spawn the next wave
                    

                }

            }
        }
    }


    void SpawnWave() //spawns the wave 
    {
        waveNo++;
        SplitEnemyId(enemyId); //splits the long string into arrays and populates through EnemyWaveList class

        foreach (EnemyWaveList e in enemyList)
        {
            StartCoroutine(waitSpawn(e));
            
        }
    }

    //this makes it so the spawner goes in intervals instead of everything spawning at once.
    IEnumerator waitSpawn(EnemyWaveList e)
    {
        for (int i = 1; i < e.qnty; i++)
        {
            yield return new WaitForSeconds(waveTime/(e.qnty*2));
            //spawn enemy
            Instantiate(SpawnEnemyFromPrefab(e.enemyWaveId), RandomizeSpawn());

        }
    }
    Transform RandomizeSpawn() //randomizes the spawnpoint chosen and returns the x y values to spawn
    {
        int random = Random.Range(0, 3);
        return spawnPoint[random];
    }

    GameObject SpawnEnemyFromPrefab(string enemyIdToRef) //searches through prefabs and spawns based on enemy Id
    {
        GameObject spawnPrefab = null;

        foreach(GameObject e in enemyPrefabs)
        {
            if(e.GetComponent<EnemyController>().enemyId == enemyIdToRef)
            {
                spawnPrefab = e;
                break;
            }
        }
        Debug.Log(spawnPrefab.name);

        return spawnPrefab;
    }

    //innitiate win sequence
    void EndRound()
    {
        overlay.waveTextBox.text = "YOU WIN!";
        end = true;
    }

    //well this works. but need to be called only once. If maybe id != previous Id or something or else it will keep adding exponentionally
    //this is to split the values. EnemyId & qnty and populate it into a list. This list will be used for Enemy JSON to instantiate the correct prefabs.
    void SplitEnemyId(string enemyId)
    {
        enemyList.Clear();

        string[] splitString = enemyId.Split('@');
        
        foreach (string s in splitString)
        {
            EnemyWaveList toAdd = new EnemyWaveList();

            string[] splitAgain = s.Split('#');
            int index = 0;

            foreach (string s2 in splitAgain)
            {
                if(index == 0)
                {
                    toAdd.enemyWaveId = s2;
                }
                else
                {
                    toAdd.qnty = int.Parse(s2);
                }
                index++;
            }

            enemyList.Add(toAdd);
        }

    }

    void GetFromJson()
    {

        foreach (WaveClass wave in waveJsonList)
        {
            if(waveId == wave.waveId)
            {
                waveId = wave.waveId;
                nextWave = wave.nextWave;
                enemyId = wave.enemyId;
                currentLevel = wave.keyCardId;
            }
        }
    }

    //uhhhhhhhh
    /*void FakeJson()
    {
        switch (waveId)
        {
            case 101: levelId = "grey";
                waveNo = 1;
                enemyId = "E01#10@E02#10";
                break;

            case 102:
                levelId = "grey";
                waveNo = 2;
                enemyId = "E01#20@E02#20@E03#5";
                break;
            case 103:
                levelId = "grey";
                waveNo = 3;
                enemyId = "E01#10@E02#14@E03#10@E04#16";
                break;
        }
    }*/
}