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
    //Need wave json
    //Need enemy json


    //=====JSON VAR=====
    private int waveId;
    private string levelId;
    private int waveNo;
    private string enemyId;

    public class EnemyWaveList
    {
        public string enemyWaveId;
        public int qnty;
    }

    public List<EnemyWaveList> enemyList = new List<EnemyWaveList>();

    //=====Other Variables=====
    private float waveTime = 15f; //time limit for each wave
    private float currentWaveTime; //current wave time
    public string currentLevel; //current level to check with json
    private int currentWaveNo;
    private int maxWaveNo = 3; //find from json
    private bool win = false;


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
        overlay = waveOverlay.GetComponent<LabOverlay>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waveId = 101; //TO DELETE for testing
        currentWaveNo = 1; //set first wave whenever entering to 1
        currentWaveTime = waveTime; //set the first wave time to the time limit for each wave

        FakeJson(); //populate current stuff for testing
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (!win)
        {
            SetOverlayDisplay();
            currentWaveTime -= Time.deltaTime;
            if(currentWaveNo < maxWaveNo)
            {
                NextWave();
            }

        }

        if ((GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) && (currentWaveNo >= maxWaveNo)) //win condition
        {
            WinRound();
        }
    }


    //Sets Overlay values
    private void SetOverlayDisplay()
    {
        overlay.waveTextBox.text = "WAVE " + currentWaveNo.ToString() + "/" + maxWaveNo.ToString();
        overlay.timerBar.fillAmount = currentWaveTime/waveTime;

        overlay.timeElapsed.text = GameObject.FindGameObjectsWithTag("Enemy").Length.ToString() + " enemies left";

    }

    void NextWave()
    {
        if (currentWaveTime < 0)
        {
            //restart wave counter
            currentWaveTime = waveTime;
            currentWaveNo++;
            //move to next wave
            //check if the next wave is the same as the current wave. if not, move on to next wave 
            if (currentWaveNo != waveNo)
            {
                waveId++; //move to next
                FakeJson(); //populate with new reading
                SpawnWave(); //spawn the next wave

            }
        }
    }

    void SpawnWave() //spawns the wave 
    {
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
    void WinRound()
    {
        overlay.waveTextBox.text = "YOU WIN!";
        win = true;
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

    //uhhhhhhhh
    void FakeJson()
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
    }
}
