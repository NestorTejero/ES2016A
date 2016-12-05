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
    private float time = 0;
    private Vector3 position;
    private bool isAttacking = false;
    private bool isDead = false;

    // Destroy nearby towers if enemy is unable to move.
    void isBlocked()
    {
        if (position.x < gameObject.transform.position.x+2 && position.x > gameObject.transform.position.x-2 &&
            position.z < gameObject.transform.position.z + 2 && position.z > gameObject.transform.position.z - 2 && !isAttacking)
        {
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 3);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "tower")
                {
                    Destroy(hitColliders[i].gameObject);
                }
                i++;
            }
        }
        position = gameObject.transform.position;
    }


    // Collision management.
    void OnTriggerEnter(Collider other)
    {
        // Beware! Entity is only sensitive to collision whith the player's home and projectiles.
        if (other.gameObject.tag == targetTagName)
        {
            isAttacking = true;
            if (other != null)
                other.GetComponent<HomeBehavior>().takeDamage(damage);
            SelfDestroy();
        }
		/*
        if (other.gameObject.tag == "projectile")
        {
            ProjectileBehaviour pb = (ProjectileBehaviour) other.gameObject.GetComponent("ProjectileBehaviour");
            TakeDamage(pb.damage);
        }
        */


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

        position = gameObject.transform.position;
        InvokeRepeating("isBlocked", 0, 3);

    }

    // Can be modified to add cool effects when the entity takes damage.
    public virtual void TakeDamage(float damage)
    {
        health = Mathf.Max(0, health - damage);
        if (health == 0)
        {
            if (!isDead)
            {
                isDead = true;
                LogicConnector.setEnemiesLeft(LogicConnector.getEnemiesLeft() - 1);
                LogicConnector.increaseCredit(moneyValue);
                SelfDestroy();
            }
        }
    }

    // Can be modified to add cool effects when the entity is destroyed.
    protected virtual void SelfDestroy()
    {       
        Destroy(gameObject);
    }



}
