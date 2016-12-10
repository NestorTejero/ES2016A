using UnityEngine;
using System.Collections;

public class HomeBehavior : MonoBehaviour {

    public Score score;

    public float maxHealth = 100f;
    public float lifes = 3f;
    public float health = 100f;
    public int initCredit = 300;        // Initial ammount of credit

    // Collision management.
    void OnTriggerEnter(Collider other)
    {
        // This function could prove itself useful if we want to implement shooter enemies.
    }

    // Use this for initialization
    void Start () {
        LogicConnector.setCredit(initCredit);
        score = GameObject.Find("GameScripts").GetComponent<Score>();
    }

    // Take damage.
    public void takeDamage (float damage)
    {
        health = Mathf.Max(0, health - damage);
		LogicConnector.setHealth( (int)(health*lifes/ maxHealth)+1 );

        if (health == 0)
        {

            LogicConnector.setHealth(0);

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

            LogicConnector.GameOver();
            SelfDestroy();
        }
    }

    // Can be modified to add cool effects when the entity is destroyed.
    protected virtual void SelfDestroy()
    {

        Destroy(gameObject);
    }
}
