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

        // Get the height of the object that is going to be spawned
        float height = Enemy.GetComponent<Renderer>().bounds.size.y;

        // Get spawn zone position and create enemies. Position on the y axis must be enough so that
        // the object lies over the terrain and not under it.
        /*Enemy.transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, 
            height/2 ), transform.position.z);*/
        Enemy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        Instantiate(Enemy);
        totalUnits -= 1;

      
    }

    void Update()
    {

        // Cancel all Invoke calls if the wave is spawned
        if (totalUnits == 0)
            SelfDestroy();

    }

    protected virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }

}
