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
        // Take damage after an enemy impact
        if (other.gameObject.tag == "enemy")
        {
            EnemyBehaviour pb = (EnemyBehaviour)other.gameObject.GetComponent("EnemyBehaviour");
            TakeDamage(pb.damage);
        }
    }

    // Use this for initialization
    void Start () {
        LogicConnector.setCredit(initCredit);
	}

    private void TakeDamage (float damage)
    {
        health = Mathf.Max(0, health - damage);
		LogicConnector.setHealth( (int)(health*lifes/ maxHealth)+1 );
        if (health == 0)
        {
			LogicConnector.setHealth(0);
            // TO DO: CALL TEAM C METHOD "GAME OVER" (doesnt exist)
            SelfDestroy();
        }
    }

    // Can be modified to add cool effects when the entity is destroyed.
    protected virtual void SelfDestroy()
    {

        Destroy(gameObject);
    }
}
