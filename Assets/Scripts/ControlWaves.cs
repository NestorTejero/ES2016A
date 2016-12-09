using UnityEngine;
using System.Collections;
using System.Xml;
using System;



public class ControlWaves : MonoBehaviour
{

    public Score score;
    public GameObject SpawningZones;
    public GameObject SetSpawns;
    private GameObject nextEnemy;
    private XmlNode root;
    private XmlNodeList roundList,enemyList;
    private int totalRounds, currentRound, totalEnemies, currentEnemy;
    private String difficulty;


    void Start()
    {

        // GET DIFFICULTY FROM LOGICCONNECTOR
        difficulty = "hard"; // easy, medium or hard

        // Read xml document and get rounds info
        TextAsset textAsset = (TextAsset)Resources.Load("Xml/rounds_"+difficulty);
        XmlDocument newXml = new XmlDocument();
        newXml.LoadXml(textAsset.text);

        root = newXml.DocumentElement;
        
        roundList = root.SelectNodes("(Rounds/Round)");       
    
        totalRounds = roundList.Count;
        currentRound = 0;
        score = GameObject.Find("GameScripts").GetComponent<Score>();
        LogicConnector.setTime(30.0f);

    }

    void NextRound()
    {

        // Initialize round stats
        LogicConnector.Battling();
        LogicConnector.setTime(30.0f);
        if (currentRound > 0)
            score.setRound(currentRound);
        currentRound += 1;                                        
        enemyList = root.SelectNodes("(Rounds/Round["+currentRound+"]/Enemy)");
        totalEnemies = 0;
        currentEnemy = 0;
        for (int i = 0; i < enemyList.Count; i++)
            totalEnemies += Int32.Parse(enemyList[i]["amount"].InnerText);
        totalEnemies *= ((GameObject)Resources.Load("Prefabs/SpawningZones", typeof(GameObject))).transform.childCount;

        LogicConnector.setTotalEnemies(totalEnemies);
        LogicConnector.setEnemiesLeft(totalEnemies);
        //LogicConnector.setRoundNumber(currentRound); <-- CHECK IF METHOD NAME IS CORRECT (TEAM C!!)


    }

    void NextEnemy()
    {

        SetSpawns = Instantiate(SpawningZones);

        // Put enemies into spawn zones
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("spawn"))
        {
            // Initialize enemy stats
            EnemySpawn Enemy = spawn.GetComponent<EnemySpawn>();
            Enemy.setEnemy((GameObject)Resources.Load("Prefabs/Enemies/" + enemyList[currentEnemy]["name"].InnerText, typeof(GameObject)));
            Enemy.setTotalUnits(Int32.Parse(enemyList[currentEnemy]["amount"].InnerText));
            Enemy.setSpawnTime(float.Parse(enemyList[currentEnemy]["spawnTime"].InnerText));
            Enemy.setDamage(float.Parse(enemyList[currentEnemy]["damage"].InnerText));
            Enemy.setHealth(float.Parse(enemyList[currentEnemy]["health"].InnerText));
            Enemy.setSpeed(float.Parse(enemyList[currentEnemy]["speed"].InnerText));
            Enemy.setMoney(Int32.Parse(enemyList[currentEnemy]["money"].InnerText));

        }

        currentEnemy += 1;



    }

    void Update()
    {

        // After kill the wave, destroy spawn zones
        if (SetSpawns)
        {
            if (!GameObject.FindGameObjectWithTag("spawn"))                         
                Destroy(SetSpawns);

        }
        else
        {

            // Start next round
            if (LogicConnector.getTime() < 0)
                NextRound();

            // Check if round finished
            else if (enemyList != null && currentEnemy < enemyList.Count)
                NextEnemy();

        

            // After N seconds, start a new wave
            else if (!GameObject.FindGameObjectWithTag("enemy")){
                // Condition to win
                if (currentRound == totalRounds)
                {
                    score.setRound(currentRound);

                    /*
                    // DEBUG SCORE
                    Debug.Log(score.getRound());
                    Debug.Log(score.getEnemies());
                    Debug.Log(score.getTowersBuilt());
                    Debug.Log(score.getTowersSold());
                    Debug.Log(score.getGoldEarned());
                    Debug.Log(score.getTime());
                    Debug.Log(score.getScore());
                    */

                    // Send info score to LogicConnector (Team C)

                    // Call Win method (Team C)
                    Debug.Log("YOU WIN!"); 



                }
                    
                else
                {
                    if (LogicConnector.isInBattling())
                        LogicConnector.Break();
                    LogicConnector.decreaseTime(Time.deltaTime);
                }
                
			}

        }

    }

}
