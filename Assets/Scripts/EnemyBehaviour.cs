using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
    // Public STATS
    public float damage;
    public float health;
    public float speed;
    public int moneyValue;
    public float attackRate = 1f; // time between attacks (in seconds)

    // Private STATS: By reducing the ammount of public stats we limint the cost of balancing the game.
    private float rotSpeed = 2.5f;  // rotation speed

    public GameObject target;                   // target position should be that of the player's base
    public string targetTagName = "home";       // player tag. 

    private NavMeshAgent agent;
	private Animator anim;
    private float time = 0;
    private Vector3 position;

    // FLAGS
    private bool isAttacking = false;       // attacking mode flag
    private bool targetLocked = false;      // if false -> rotate to face target if attacking
                                            // if true  -> look at target
    private bool isDead = false;  

    // Checks if enemy is unable to move, and manages such situation.
    void isBlocked()
    {
        // Check if blocked
        if (position.x < gameObject.transform.position.x+2 && position.x > gameObject.transform.position.x-2 &&
            position.z < gameObject.transform.position.z + 2 && position.z > gameObject.transform.position.z - 2 && !isAttacking)
        {
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 3);
            foreach (Collider collider in hitColliders)
            {
                // Enemy is blocked by a tower
                if (collider.tag == "tower")
                {
                    CancelInvoke("isBlocked");          // abort isBlocked checking

                    target = collider.gameObject;       // target blocking tower
                    isAttacking = true;
                    Stop();                                             // stop moving
                    InvokeRepeating("Attack", attackRate, attackRate);  // begin attack on new target
                    break;
                }
            }
        }
        position = gameObject.transform.position;
    }


    // Collision management.
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "projectile":  // Enemy gets hit by a projectile
                ProjectileBehaviour pb = (ProjectileBehaviour)other.gameObject.GetComponent("ProjectileBehaviour");
                TakeDamage(pb.damage);      // manage damage inflicted by the projectile
                break;

            case "home": // Enemy reaches home
                if (isAttacking)
                    return;     // do nothing if attacking
                
                target = other.gameObject;                          // set target
                isAttacking = true;
                Stop();                                             // stop moving
                InvokeRepeating("Attack", attackRate, attackRate);  // begin attack
                break;

            default:
                return;
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

        position = gameObject.transform.position;
        InvokeRepeating("isBlocked", 3, 3);         // blockade checkout
    }

    // Temporary solution to some enemies not facing the tower when attacking
    void Update()
    {
        // TODO: Implement rotation
        if (!isAttacking || target == null)
        {
            targetLocked = false;   // reset lock flag
            Resume();               // move on
            return;
        }

        if (targetLocked)   // If target is locked look forwards to it
            transform.LookAt(new Vector3(target.transform.position.x, 
                transform.position.y, target.transform.position.z));
        else
            FaceTarget(target.transform);        
    }

    // Can be modified to add cool effects when the entity takes damage.
    protected virtual void TakeDamage(float damage)
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

    // Assign damage to the target.
    private void Attack()
    {
        // Disable attacking mode if target has been destroyed.
        if (target == null)
        {
            isAttacking = false;
            CancelInvoke("Attack"); // cancel all invokes
            //Resume();               // resume movement
            return;
        }
        if (!targetLocked)  // do not assign damage if target is not locked 
            return;

        // Assign damage to target.
        if (target.tag == "home")
            target.GetComponent<HomeBehavior>().takeDamage(damage);
        else if (target.tag == "tower")
        {
            TowerBehavior tb = target.GetComponent<TowerBehavior>();
            if (tb == null)
                tb = target.GetComponentInChildren<TowerBehavior>();
            tb.takeDamage(damage);
        }          
    }

    // Stops navigation agent movement.
    private void Stop()
    {
        if (agent != null)
            agent.Stop();
    }

    // Resumes navigation agent movement.
    private void Resume()
    {
        if (agent != null)
            agent.Resume();
    }

    // Check if enemy is facing given transform. Rotate towards it if not facing.
    private void FaceTarget(Transform other)
    {    
        if (Vector3.Dot(transform.forward, (other.position - transform.position).normalized) > 0.9)
        {
            targetLocked = true;  // set flag if enemy is reasonably facing its target
            return;
        }

        // Rotate towards target direction
        Vector3 targetDir = target.transform.position - transform.position;
        targetDir.y = transform.position.y;                     // prevent enemy from staring at the ground
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotSpeed * Time.deltaTime, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);   // apply rotation
    }

}
