using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

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
    private float waveTime = 5f; //time limit for each wave
    private float currentWaveTime; //current wave time
    public string currentLevel; //current level to check with json
    private int currentWaveNo;
    private int maxWaveNo = 3; //find from json


    //====Display Stuff=====
    [SerializeField] GameObject waveOverlay;
    LabOverlay overlay;

    private void Awake()
    {
        overlay = waveOverlay.GetComponent<LabOverlay>();
        currentLevel = GameClass.GetCurrentLevelId(); //get current level index
    }

    // Start is called before the first frame update
    void Start()
    {
        waveId = 101;
        currentWaveNo = 1;
        FakeJson();
        currentWaveTime = waveTime;
    }

    // Update is called once per frame
    void Update()
    {
        FakeJson();
        currentWaveTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        SetOverlayDisplay();

        if(currentWaveNo > maxWaveNo)
        {
            WinRound();
        }
    }

    private void SetOverlayDisplay()
    {
        overlay.waveTextBox.text = "WAVE " + currentWaveNo.ToString() + "/" + maxWaveNo.ToString();
        overlay.timerBar.fillAmount = currentWaveTime/waveTime;

        float time = Time.time;
        int min = (int)time/60;
        int sec = (int)time%60;
        overlay.timeElapsed.text = min.ToString() + " : " + sec.ToString();

    }

    //populate with necessary details. This function is to check the levelId from gameclass. 
    void CheckNextWave()
    {
        if(currentWaveTime <= 0)
        {
            currentWaveNo += 1;
            //CheckLevel()
            currentWaveTime = waveTime;
            waveId += 1;
            //move to next wave
            SpawnEnemyWave();
        }

    }
    void CheckLevel()
    {
        if(levelId == currentLevel) 
        {
            //Get details for that level only
            GetFromJson();
        }
    }

    void GetFromJson()
    {
        //foreach list
        //if currentWaveno == waveno(from Json)
        
        //get details
        SplitEnemyId(enemyId);
    }
    

    void SpawnEnemyWave()
    {
        //get which wave
        GetFromJson();

        if (currentWaveNo != waveNo)
        {
            foreach (EnemyWaveList e in enemyList)
            {
                Debug.Log("Spawning Wave " + currentWaveNo + ". Spawn " + e.qnty + " of enemy type " + e.enemyWaveId);
            }

            CheckNextWave();
        }

    }

    //innitiate win sequence
    void WinRound()
    {
        overlay.waveTextBox.text = "YOU WIN!";
        Time.timeScale = 0;
    }

    //well this works. but need to be called only once. If maybe id != previous Id or something or else it will keep adding exponentionally
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


    void FakeJson()
    {
        switch (waveId)
        {
            case 101: levelId = "grey";
                waveNo = 1;
                enemyId = "E01#5";
                break;

            case 102:
                levelId = "grey";
                waveNo = 2;
                enemyId = "E01#10";
                break;
            case 103:
                levelId = "grey";
                waveNo = 3;
                enemyId = "E01#20@E02#2";
                break;
        }
    }
}
