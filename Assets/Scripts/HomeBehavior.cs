using UnityEngine;
using System.Collections;

public class HomeBehavior : MonoBehaviour {

    public float health = 100f;

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
	
	}

    private void TakeDamage (float damage)
    {
        health = Mathf.Max(0, health - damage);
        if (health == 0)
        {
            SelfDestroy();
        }
    }

    // Can be modified to add cool effects when the entity is destroyed.
    protected virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
