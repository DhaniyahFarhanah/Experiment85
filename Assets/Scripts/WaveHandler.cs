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
    private int initialWaveID;
    private float waveTime = 15f; //time limit for each wave
    private float currentWaveTime; //current wave time
    private int currentLevel; //current level to check with json (not used due to reducing scope)
    //private int maxWaveNo; //find from json (Not needed for new scope)
    public bool end = false;
    public int enemyNeeded;
    [SerializeField] GameObject endGameDoor;
    [SerializeField] GameObject analytics;


    //====Display Stuff=====
    [SerializeField] GameObject waveOverlay;
    LabOverlay overlay;

    //=====Enemy Spawning Stuff====
    [SerializeField] Transform[] spawnPoint;
    //is there a way to make this more scalable? MAYBE!! I DUNNO!!
    //okay maybe in...enemy script? can it even like- actually i dont think so...maybe...an array of gameobjects and then check enemyscript's id?
    //and instantiate the uhhhhhh the prefab with the enemyscript associated with the enemyId? MAN IDK 
    [SerializeField] GameObject[] enemyPrefabs;

    //=====Game Analytics=====
    public bool win;
    private float time;
    private int hour;
    private int min;
    private int sec;

    private int numOfEnemiesSpawned;
    private int numOfEnemy1;
    private int numOfEnemy2;
    private int numOfEnemy3;
    private int numOfEnemy4;

    public int numOfBuffsDropped;
    public int numOfBuff1;
    public int numOfBuff2;
    public int numOfBuff3;
    public int numOfBuff4;

    private void Awake()
    {
        waveJsonList = GameData.GetWaveList();
        overlay = waveOverlay.GetComponent<LabOverlay>();
        initialWaveID = waveJsonList[0].waveId;
        waveId = initialWaveID;
        GetFromJson();
    }

    // Start is called before the first frame update
    void Start()
    {
        //reading restart just in case
        numOfEnemiesSpawned = 0;
        numOfEnemy1 = 0;
        numOfEnemy2 = 0;
        numOfEnemy3 = 0;
        numOfEnemy4 = 0;

        numOfBuffsDropped = 0;
        numOfBuff1 = 0;
        numOfBuff2 = 0;
        numOfBuff3 = 0;
        numOfBuff4 = 0;
        
        time = 0;
        win = false;

        time += Time.deltaTime;
        currentWaveTime = waveTime; //set the first wave time to the time limit for each wave
        //maxWaveNo = waveJsonList.Count; (Not needed for new scope)
        
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {

        if (!end)
        {
            time += Time.deltaTime;
            currentWaveTime -= Time.deltaTime;
            SetOverlayDisplay();

            NextWave();

        }

        else if (end)
        {
            //End game & set analytics
            EndRound();
        }

        //Change winning condition to interacting with door.
        

        /*if ((GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) && (nextWave == -2)) //win condition
        {
            EndRound();
            //set analytics
        }*/
    }


    //Sets Overlay values
    private void SetOverlayDisplay()
    {
        overlay.waveTextBox.text = "WAVE " + waveNo.ToString();
        overlay.timerBar.fillAmount = currentWaveTime / waveTime;
        overlay.enemiesInSceneTextBox.text = GameObject.FindGameObjectsWithTag("Enemy").Length.ToString() + " enemies alive";

        hour = (int) time / 3600;
        min = (int) (time% 3600) / 60;
        sec = (int) time % 60;

        overlay.timeElapsedTextBox.text = min.ToString("00") + ":" + sec.ToString("00");

        if (enemyNeeded <= 0)
        {
            endGameDoor.SetActive(true);
            overlay.enemiesLeftTextBox.text = "Door is opened!! Leave to WIN!";
        }
        else
        {
            overlay.enemiesLeftTextBox.text = enemyNeeded.ToString() + " left to KILL";
        }

    }

    void NextWave()
    {
        if (currentWaveTime < 0)
        {
            
            currentWaveTime = waveTime; //restart wave counter
            //move to next wave
            //check if the next wave is the same as the current wave. if not, move on to next wave 
            
                if(nextWave == -1)
                {
                    //WinRound();
                    //check if its condition to leave
                    //This code is made like this due to the possible future implementation if we pass this mod lol
                    //-1 is supposed to stop and allow the character to move back to the safe zone and do some upgardes but with the cutting corners no need
                    //But scrapped due to time and the removal of inventory and upgrades :C
                    waveId = (waveId / 100) * 100 + 101;
                    GetFromJson();
                    SpawnWave();
                    Debug.Log("Stop Wave");
                }
                else if(nextWave == -2)
                {
                    waveId = initialWaveID;
                    GetFromJson();
                    SpawnWave();
                }

                else //continue wave
                {
                    waveId = nextWave;
                   
                    GetFromJson(); //populate with new reading
                    SpawnWave(); //spawn the next wave
                    

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
        float timeToSpawn = waveTime / enemyList.Count;

        for (int i = 1; i < e.qnty; i++)
        {
            yield return new WaitForSeconds(timeToSpawn/(e.qnty));
            //spawn enemy
            Instantiate(SpawnEnemyFromPrefab(e.enemyWaveId), RandomizeSpawn());
            numOfEnemiesSpawned++;

            switch(e.enemyWaveId)
            {
                case "E01": numOfEnemy1++; break;
                case "E02": numOfEnemy2++; break;
                case "E03": numOfEnemy3++; break;
                case "E04": numOfEnemy4++; break;
            }

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

        //disable character
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().disabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLab>().disabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().rb.velocity = Vector3.zero;

        //set analytics here
        analytics.SetActive(true);
        SetWaveAnalytics();
        DisplayAnalytics();

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
        //waveId = initialWaveID;

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

    void SetWaveAnalytics()
    {
        AnalyticsHolder.Instance.win = win;
        AnalyticsHolder.Instance.timeTaken = hour.ToString("00") + ":" + min.ToString("00") + ":" + sec.ToString("00");
        AnalyticsHolder.Instance.waveEnd = waveNo;
        AnalyticsHolder.Instance.waveEndId = waveId;

        AnalyticsHolder.Instance.numEnemiesSpawned = numOfEnemiesSpawned;
        AnalyticsHolder.Instance.numOfEnemy1Spawned = numOfEnemy1;
        AnalyticsHolder.Instance.numOfEnemy2Spawned = numOfEnemy2;
        AnalyticsHolder.Instance.numOfEnemy3Spawned = numOfEnemy3;
        AnalyticsHolder.Instance.numOfEnemy4Spawned = numOfEnemy4;

        AnalyticsHolder.Instance.buffsDropped = numOfBuffsDropped;
        AnalyticsHolder.Instance.numOfBuff1Spawned = numOfBuff1;
        AnalyticsHolder.Instance.numOfBuff2Spawned = numOfBuff2;
        AnalyticsHolder.Instance.numOfBuff3Spawned = numOfBuff3;
        AnalyticsHolder.Instance.numOfBuff4Spawned = numOfBuff4;
        AnalyticsHolder.Instance.mostTakenBuff = FindMaxBuff();
    }
    string FindMaxBuff()
    {
        int max = 0;
        string mostTakenId = "";
        int[] buffsTaken = { numOfBuff1, numOfBuff2, numOfBuff3, numOfBuff4 };

        for(int i = 0; i < buffsTaken.Length; i++)
        {
            if(max < buffsTaken[i])
            {
                max = buffsTaken[i];

                switch (i)
                {
                    case 0: mostTakenId = "buff1"; break;
                    case 1: mostTakenId = "buff2"; break;
                    case 3: mostTakenId = "buff3"; break;
                    case 4: mostTakenId = "buff4"; break;
                }
            }
        }

        return mostTakenId;

    }
    void DisplayAnalytics()
    {
       

    }

}
