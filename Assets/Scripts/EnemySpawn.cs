using UnityEngine;
using System.Collections;


public class EnemySpawn : MonoBehaviour
{

    public GameObject Enemy;
    public float spawnTime = 2f;
    public int totalUnits = 20;

    void Start()
    {

        // Call Spawn method every "spawnTime" seconds
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        
    }

    void Spawn()
    {
        
        // Get spawn zone position and create enemies
        Enemy.transform.position = this.transform.position;      
        Instantiate(Enemy);
        totalUnits -= 1;

      
    }

    void Update()
    {

        // Cancel all Invoke calls if the wave is spawned
        if (totalUnits == 0)
            CancelInvoke();

    }

}
