using UnityEngine;
using System.Collections;


public class ControlWaves : MonoBehaviour
{

    public GameObject SpawningZones;
    public GameObject SetSpawns;
    float timeLeft = 0.0f;

    void Start()
    {

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
                    timeLeft = 60.0f;

                                        
                }
            }
        }
        // After 60 seconds, start a new wave
        else
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Start();
            }
        }

    }

}
