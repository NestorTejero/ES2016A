using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {
    private Vector3 startPosition;   // Projectile's starting position;

    // STATS
    public float damage = 1f;
    public float speed = 100.0f;

    public Transform target;         // Target position.
    public string targetTagName;     // Target tag.
    public string parentTagName;     // Shooting entity's tag.
    public float reach = 0.0f;       // Projectile is destroyed after exceeding its reach. Using reach=0 disables such feature.

    // Collision management.
    void OnTriggerEnter(Collider other)
    {
        // Projectile self destructs when colliding with any entity other than that which shot it.
        if (other.gameObject.tag != parentTagName)
        {
            SelfDestroy();
        }
    }

    //First called function. Main assignations
    void Awake()
    {
        //Initializing the enemy position
        startPosition = transform.position;
    }

    // Use this for initialization
    void Start ()
    {
        if (target == null)
        {
            // Check if we have a target.
            Debug.LogWarning("Projectile has not a target.");
        }
        else
        {
            transform.LookAt(target);
            Update();
        }
    }
	
	// Update is called once per frame
	void Update () {
        // The projectile keeps moving even if its target has been already destroyed.
        TransformProjectile();
    }

    // Returns true if the distance traversed by the projectile is equal or exceeds its reach.
    private bool ReachExceeded()
    {
        float traversedDist = Vector3.Distance(startPosition, transform.position);
        return traversedDist >= reach;
    }

    // Manages projecile's movement through the world.
    private void TransformProjectile()
    {
        /*
         * The projectile describes a linear trajectory towards its target. On subsequent sprints, it could be
         * interesting to implement some parabolic movement.
         */
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // Manages the destruction of the projectile. Can be overriten to add cool effects.
    protected virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
