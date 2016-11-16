using UnityEngine;
using System.Collections;
using System.Xml;
using System;



public class ControlWaves : MonoBehaviour
{

    public GameObject SpawningZones;
    public GameObject SetSpawns;
    private GameObject nextEnemy;
    private XmlNodeList nodeList;
    private int totalRounds, currentRound;

    void Start()
    {

        // Read xml document and get rounds info
        XmlDocument newXml = new XmlDocument();
        newXml.Load(Application.dataPath + "/Resources/xml/rounds.xml");

        XmlNode root = newXml.DocumentElement;
        
        nodeList = root.SelectNodes("(Rounds/Round)");

        totalRounds = nodeList.Count;
        currentRound = 1;
        LogicConnector.setTime(30.0f);
       

    }

    void NextRound()
    {

        LogicConnector.setTime(30.0f);      
        SetSpawns = Instantiate(SpawningZones);

        // Put enemy into spawn zones
        nextEnemy = (GameObject)Resources.Load("Prefabs/Enemies/" + nodeList[currentRound - 1]["name"].InnerText, typeof(GameObject));
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("spawn"))
        {
            // Initialize round stats
            EnemySpawn Enemy = spawn.GetComponent<EnemySpawn>();
            Enemy.setEnemy(nextEnemy);
            Enemy.setTotalUnits(Int32.Parse(nodeList[currentRound - 1]["amount"].InnerText));
            Enemy.setSpawnTime(float.Parse(nodeList[currentRound - 1]["spawnTime"].InnerText));
            Enemy.setDamage(float.Parse(nodeList[currentRound - 1]["damage"].InnerText));
            Enemy.setHealth(float.Parse(nodeList[currentRound - 1]["health"].InnerText));
            Enemy.setSpeed(float.Parse(nodeList[currentRound - 1]["speed"].InnerText));
            Enemy.setMoney(Int32.Parse(nodeList[currentRound - 1]["money"].InnerText));
            

        }
        
        currentRound += 1;

    }

    void Update()
    {

        // After kill the wave, destroy spawn zones and start a count
        if (SetSpawns)
        {
            if (!GameObject.FindGameObjectWithTag("spawn"))
            {
                if (!GameObject.FindGameObjectWithTag("enemy"))
                {
                    Destroy(SetSpawns);

                }
            }
        }

        // Check if you completed all rounds
        else if (currentRound > totalRounds)
            Debug.Log("YOU WIN!");

        // After 60 seconds, start a new wave
        else
        {

			LogicConnector.decreaseTime(Time.deltaTime);
			if (LogicConnector.getTime () < 0)
            {
                
                NextRound();

            }
        }

    }

}
