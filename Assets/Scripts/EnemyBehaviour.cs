using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
    // Public STATS
    public float damage;
    public float health;
    public float speed;
    public int moneyValue;
    public float attackRate = 1f;           // time between attacks (in seconds)
    public string targetTagName = "home";   // player tag 

    // Private STATS: By reducing the ammount of public stats we limint the cost of balancing the game.
    private float rotSpeed = 2.5f;          // rotation speed
    private NavMeshAgent agent;
    private Animator anim;
    private float time = 0;
    private Vector3 position;

    // Targets: Enemy moves towards target and attacks it if in contact
    public GameObject target;               // object the enemy is currently attacking 
    private GameObject primaryTarget;       // priority target

    // FLAGS
    public bool isAttacking = false;        // attacking mode flag
    public bool targetLocked = false;       // if false -> rotate to face target if attacking
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
                    SetTarget(collider.gameObject);             // target blocking tower           
                    StartAttack();                              // begin attack        
                    position = gameObject.transform.position;   // store current position
                    return;
                }
            }
            // Reset if enemy is frozen
            Reset();
        }
        position = gameObject.transform.position;   // store current position
    }


    // Collision management.
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            /* Moved to ProjectileBehavior
			case "projectile":  // Enemy gets hit by a projectile
                ProjectileBehaviour pb = (ProjectileBehaviour)other.gameObject.GetComponent("ProjectileBehaviour");
                TakeDamage(pb.damage);          // manage damage inflicted by the projectile
                break;
                */
            case "home":  // Enemy reaches home
                if (!isAttacking)
                {
                    SetTarget(other.gameObject); // set home as target
                    StartAttack();               // begin attacking home
                }
                break;
            default:
                return;
        }
    }

    // Use this for initialization
    void Start ()
    {
		anim = GetComponent<Animator> ();
   
        // Configure navigation agent
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        SetTarget(targetTagName);   // set primary target as current target


        position = gameObject.transform.position;
        InvokeRepeating("isBlocked", 2, 2);         // blockade checkout
    }

    // Temporary solution to some enemies not facing the tower when attacking
    void Update()
    {
        if (target == null)
            Reset();

        if (isAttacking)        // face target while attacking
        {
            if (targetLocked)   // if target is locked look forwards to it
                transform.LookAt(new Vector3(target.transform.position.x,
                    transform.position.y, target.transform.position.z));
            else
                FaceTarget(target.transform);  // rotate to face enemy if it's not locked
        }     
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

    // Start attacking current target
    private void StartAttack()
    {
        if (target == null)
            return;

        CancelInvoke();         // Cancel all invocations

        agent.Stop();         // stop nav agent
        isAttacking = true;
        InvokeRepeating("Attack", attackRate, attackRate);
    }

    // Stop current attack
    private void StopAttack()
    {
        CancelInvoke("Attack");     // Abort attack

        targetLocked = false;       // reset target locked tag
        isAttacking = false;        // reset attack flag
        SetTarget(targetTagName);   // reset target

        agent.Resume();             // resume nav agent
        InvokeRepeating("isBlocked", 2, 2); // resume blockade checkout
    }

    // Assign damage to the target.
    private void Attack()
    {
        if (target == null)
            StopAttack();       // stop if target has been destroyed

        if (!targetLocked)      // do not assign damage if target has not been locked
            return;

        // Assign damage to target.
        if (target.tag == "home")
            target.GetComponent<HomeBehavior>().takeDamage(damage);
        else if (target.tag == "tower")
        {
            TowerBehavior tb = target.GetComponent<TowerBehavior>();
            if (tb == null)     // search behavior in children if its not found in object
                tb = target.GetComponentInChildren<TowerBehavior>();
            tb.takeDamage(damage);
        }          
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

    // Assign current target from given tag.
    private void SetTarget(string tag)
    {
        if (tag.Equals(targetTagName))  // check if tag equals that of primary taget
        {
            if (primaryTarget == null)  // search for primary target if not assigned
                primaryTarget = GameObject.FindGameObjectWithTag(tag);
            target = primaryTarget;     // set target
        }   
        else
            target = GameObject.FindGameObjectWithTag(tag);     // search target

        agent.ResetPath();              // reset agent's current path
        SetDestination(target);         // set destination
    }

    // Assign current target from given object.
    private void SetTarget(GameObject go)
    {
        target = go;
        SetDestination(target);
    }

    // Set object's position as destination, such object defined by given tag.
    private void SetDestination(GameObject go)
    {
        if (agent == null)
            return;

        if (go != null)
        {
            Vector3 destination = go.transform.position;
            destination.y = 0;
            agent.destination = destination;
        }
        else
            agent.Stop();
    }

    // Resets enemy. Used as safety measure to ensure no enemy stays idle
    private void Reset()
    {
        CancelInvoke(); // cancel all invokes

        position = gameObject.transform.position;   // store current position

        isAttacking = false;        // reset attack flag
        targetLocked = false;       // reset target locked flag
        SetTarget(targetTagName);               // set primary target as target
        InvokeRepeating("isBlocked", 2, 2);     // restart blockade checkout
    }

}
