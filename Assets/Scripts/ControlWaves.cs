using UnityEngine;
using System.Collections;
using System.Xml;
using System;



public class ControlWaves : MonoBehaviour
{

    public GameObject SpawningZones;
    public GameObject SetSpawns;
    private GameObject nextEnemy;
    private XmlNode root;
    private XmlNodeList roundList,enemyList;
    private int totalRounds, currentRound, totalEnemies, currentEnemy;


    void Start()
    {

        // Read xml document and get rounds info
        XmlDocument newXml = new XmlDocument();
        newXml.Load(Application.dataPath + "/Resources/Xml/rounds.xml");

        root = newXml.DocumentElement;
        
        roundList = root.SelectNodes("(Rounds/Round)");       
    
        totalRounds = roundList.Count;
        currentRound = 0;

        LogicConnector.setTime(30.0f);

    }

    void NextRound()
    {

        // Initialize round stats
        // Uncomment in dev integration (Team C did a new logic connector)
        //InterfaceState.Battling();
        LogicConnector.setTime(30.0f);
        currentRound += 1;                                  
        enemyList = root.SelectNodes("(Rounds/Round["+currentRound+"]/Enemy)");
        totalEnemies = 0;
        currentEnemy = 0;
        for (int i = 0; i < enemyList.Count; i++)
            totalEnemies += Int32.Parse(enemyList[i]["amount"].InnerText);
        totalEnemies *= ((GameObject)Resources.Load("Prefabs/SpawningZones", typeof(GameObject))).transform.childCount;
        // Uncomment in dev integration (Team C did a new logic connector)
        //LogicConnector.setTotalEnemies(totalEnemies);
        //LogicConnector.setEnemiesLeft(totalEnemies);
        //LogicConnector.setRoundNumber(currentRound); <-- CHECK IF METHOD NAME IS CORRECT  


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

            // Condition to win
            else if (currentRound == totalRounds)
                Debug.Log("YOU WIN!");
          
            // Uncomment in dev integration (Team C did a new logic connector)
            //else if (InterfaceState.isInBattling())
            //    InterfaceState.Break();

            // After N seconds, start a new wave
            else if (!GameObject.FindGameObjectWithTag("enemy"))
                LogicConnector.decreaseTime(Time.deltaTime);

        }

    }

}
