using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
    // STATS
    public float damage;
    public float health;
    public float speed;
    public int moneyValue;

    public GameObject target;                    // Target position should be that of the player's base.
    public string targetTagName = "home";       // Player tag. 

    private NavMeshAgent agent;
	private Animator anim;


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

		anim = GetComponent<Animator> ();

        // Get the Scene's home
        if (target == null)
            target = GameObject.FindGameObjectWithTag(targetTagName);

        // If home has not been destroyed yet
        if (target != null)
        {
            Vector3 destination = target.transform.position;
            destination.y = 0;
            // Configure navigation agent
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            agent.destination = destination;
        }  

    }

    // Can be modified to add cool effects when the entity takes damage.
    protected virtual void TakeDamage(float damage)
    {
        health = Mathf.Max(0, health - damage);
        if (health == 0)
        {
            // Uncomment in dev integration (Team C did a new logic connector)
            //LogicConnector.setEnemiesLeft(LogicConnector.getEnemiesLeft() -= 1);
			LogicConnector.increaseCredit(moneyValue);
            SelfDestroy();
        }
    }

    // Can be modified to add cool effects when the entity is destroyed.
    protected virtual void SelfDestroy()
    {       
        Destroy(gameObject);
    }



}
