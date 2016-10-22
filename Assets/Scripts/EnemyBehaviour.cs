using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
    // STATS
    public float damage = 2.5f;
    public float health = 10f;
    public float speed = 50.0f;

    public int moneyValue = 10;
    public Transform target;                    // Target position should be that of the player's base.
    public string targetTagName = "home";       // Player tag. 

    // Collision management.
    void OnTriggerEnter(Collider other)
    {
        // Beware! Entity is only sensitive to collision whith the player's home and projectiles.
        if (other.gameObject.tag == targetTagName)
        {
            SelfDestroy();
        }
        if (other.gameObject.tag == "projectile")
        {
            ProjectileBehaviour pb = (ProjectileBehaviour) other.gameObject.GetComponent("ProjectileBehaviour");
            TakeDamage(pb.damage);
        }
    }

    // Use this for initialization
    void Start ()
    {
        // Get the Scene's tower
        target = GameObject.FindGameObjectWithTag(targetTagName).transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (target != null)
        {
            /* Enemy just advances towards the player's base. */
            TransformEnemy();
        }
    }

    protected virtual void TransformEnemy()
    {
        // Get direction vector to tower
        Vector3 dir = target.transform.position - this.transform.position;
        // Dont move on Y axis (Vertical)
        dir.y = 0;
        // Move X units per second
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }

    // Can be modified to add cool effects when the entity takes damage.
    protected virtual void TakeDamage(float damage)
    {
        health = Mathf.Max(0, health - damage);
        if (health == 0)
        {
            LogicConnector.getInstance().testCredit += moneyValue;
            SelfDestroy();
        }
    }

    // Can be modified to add cool effects when the entity is destroyed.
    protected virtual void SelfDestroy()
    {
        
        Destroy(gameObject);
    }



}