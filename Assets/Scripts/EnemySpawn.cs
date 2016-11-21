using UnityEngine;
using System.Collections;


public class EnemySpawn : MonoBehaviour
{
   
    private GameObject Enemy;
    private int totalUnits;
    private float spawnTime;
    private float damage;
    private float health;
    private float speed;
    private int money;


    void Start()
    {
        // Call Spawn method every "spawnTime" seconds       
        InvokeRepeating("Spawn", 0, spawnTime);
    }

    void Spawn()
    {
        // Get spawn zone position and create enemies. Position on the y axis must be enough so that
        // the object lies over the terrain and not under it.
        Enemy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        EnemyBehaviour enemyStats = Enemy.GetComponent<EnemyBehaviour>();
        // Set stats to enemy
        enemyStats.damage = getDamage();
        enemyStats.health = getHealth();
        enemyStats.speed = getSpeed();
        enemyStats.moneyValue = getMoney();

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


    // Setters & Getters

    public void setEnemy(GameObject Enemy)
    {
        this.Enemy = Enemy;
    }

    public void setTotalUnits(int totalUnits)
    {
        this.totalUnits = totalUnits;
    }

    public void setSpawnTime(float spawnTime)
    {
        this.spawnTime = spawnTime;
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void setHealth(float health)
    {
        this.health = health;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setMoney(int money)
    {
        this.money = money;
    }



    public GameObject getEnemy()
    {
        return Enemy;
    }

    public int getTotalUnits()
    {
        return totalUnits;
    }

    public float getSpawnTime()
    {
        return spawnTime;
    }

    public float getDamage()
    {
        return damage;
    }

    public float getHealth()
    {
        return health;
    }

    public float getSpeed()
    {
        return speed;
    }

    public int getMoney()
    {
        return money;
    }




}
