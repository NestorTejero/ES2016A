using UnityEngine;
using System.Collections;


public class ControlWaves : MonoBehaviour
{

    public GameObject SpawningZones;
    public GameObject SetSpawns;
    private int RandomEnemy;
    private string[] EnemiesList;
    public GameObject nextEnemy;


    void Start()
    {
        // Get random enemy
        EnemiesList = new string[2];
        EnemiesList[0] = "porky";
        EnemiesList[1] = "GordoAtacaYCamina";
        RandomEnemy = Random.Range(0, 2);
        nextEnemy = (GameObject)Resources.Load("Prefabs/Enemies/"+EnemiesList[RandomEnemy], typeof(GameObject));

        LogicConnector.setTime(60.0f);
        SetSpawns = Instantiate(SpawningZones);

        // Put enemy into spawn zones
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("spawn"))
        {
            spawn.GetComponent<EnemySpawn>().Enemy = nextEnemy;
        }


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
        // After 60 seconds, start a new wave
        else
        {

			LogicConnector.decreaseTime(Time.deltaTime);
			if (LogicConnector.getTime () < 0)
            {
                Start();
            }
        }

    }

}
