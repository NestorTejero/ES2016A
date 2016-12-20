using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {
    private Vector3 startPosition;   // Projectile's starting position;

    // STATS
    public float damage = 1f;
    public float speed = 100.0f;

    public string targetTag;		// Target tag.
    public string parentTagName;		// Shooting entity's tag.
    public float reach = 0.0f;			// Projectile is destroyed after exceeding its reach. Using reach=0 disables such feature.

	public float gravity = 0.0f;
	public float upSpeed = 0.0f;

    private Vector3 prevPos; // Used to rotate with pitch

    // Collision management.
    void OnTriggerEnter(Collider other)
    {

		// Destroy on contact with terrain
		if (other.gameObject.tag == "terrain") {
			SelfDestroy ();
		}

        // Projectile Exerts damage on target tag
		if (other.gameObject.tag == targetTag){
			
			// Check for enemy script, if so, cast damage
			EnemyBehaviour enemyB = other.gameObject.GetComponent<EnemyBehaviour>();
			if(enemyB != null)
				enemyB.TakeDamage(gameObject);
            SelfDestroy();
        }
    }

    // Use this for initialization
    void Start ()
    {
        //Initializing the enemy position
        startPosition = transform.position;
		prevPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (ReachExceeded() || transform.position.y < 0)
        {
            SelfDestroy();
        }
        // The projectile keeps moving even if its target has been already destroyed.
        TransformProjectile();
    }

    // Returns true if the distance traversed by the projectile is equal or exceeds its reach.
    private bool ReachExceeded()
    {
		float traversedDist = (transform.position - startPosition).sqrMagnitude;
		return traversedDist >= reach * reach;
    }

    // Manages projecile's movement through the world.
    private void TransformProjectile()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
		transform.position += transform.up * upSpeed * Time.deltaTime;

		// Calculate delta position for pitch rotation
		Vector3 deltaPos = transform.position - prevPos;
		Vector3 lookAtPos = transform.position + deltaPos;

        // Update vertical speed
        upSpeed -= gravity * Time.deltaTime;

        transform.LookAt (lookAtPos);
    }

    // Manages the destruction of the projectile. Can be overriten to add cool effects.
    protected virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }

    // Compute the needed vertical speed for the projectile to reach its target.
    public void setUpSpeed(Vector3 targetPosition)
    {
        if (gravity == 0)   // linear movement when there is no gravity
            upSpeed = 0;
        else
        {
            /* Relevant parabolic movement equations (2D):
             *      y = y_0 + v_y * t - (g * t**2) / 2
             *      x = x_0 + v_x * t
             *      
             * where:
             *      x - x_0 = distance to be traversed
             *      y - y_0 = height from ground to muzzle
             *      v_x = horizontal component of velocity
             *      v_y = vertical component of velocity
             *      g = gravity
             *      
             * Simplifications: v_x = speed
             *      
             *      y = 0
             *      t = speed / x
             *      v_y = g * (d / v) / 2 - y_0 * (v / d)
             * */
            
            Vector3 currentPosition = transform.position;
            currentPosition.y = 0;  // ignore vertical components
            targetPosition.y = 0;

            float distance = (targetPosition - currentPosition).magnitude;
            upSpeed = Mathf.Abs(0.5f * gravity * (distance / speed) - transform.position.y * (speed / distance)) + 0.004f;
        }
            
    }
}
