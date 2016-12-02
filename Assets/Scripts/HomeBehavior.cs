using UnityEngine;
using System.Collections;

public class HomeBehavior : MonoBehaviour {

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
	}

    public void takeDamage (float damage)
    {
        health = Mathf.Max(0, health - damage);
		LogicConnector.setHealth( (int)(health*lifes/ maxHealth)+1 );

        if (health == 0)
        {
			LogicConnector.setHealth(0);
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
