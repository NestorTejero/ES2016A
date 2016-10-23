using UnityEngine;
using System.Collections;


public class ControlWaves : MonoBehaviour
{

    public GameObject SpawningZones;
    public GameObject SetSpawns;

    void Start()
    {
		LogicConnector.setTime(60.0f);
        SetSpawns = Instantiate(SpawningZones);

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
