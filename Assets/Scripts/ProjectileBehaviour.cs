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
	public float upAccel = 0.0f;

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
        if (ReachExceeded())
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
        /*
         * The projectile describes a linear trajectory towards its target. On subsequent sprints, it could be
         * interesting to implement some parabolic movement.
         */
        transform.position += transform.forward * speed * Time.deltaTime;

		// subtract gravity from upAccel, for parabolic projectiles
		upAccel -= gravity * Time.deltaTime;
		transform.position += transform.up * upAccel * Time.deltaTime;

		// Calculate delta position for pitch rotation
		Vector3 deltaPos = transform.position - prevPos;
		Vector3 lookAtPos = transform.position + deltaPos;

		transform.LookAt (lookAtPos);
    }

    // Manages the destruction of the projectile. Can be overriten to add cool effects.
    protected virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
