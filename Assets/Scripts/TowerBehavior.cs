using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour
{

    // Stats
    public float range = 30f;			// Range in meters
	public float health = 100f;
	public int cost = 100;				// tower value

	public float damage = 5f;
    public float fireRate = 1f;    		// fire rate in seconds

	public float turnSpeed = 2f;		// Angles per second of the turret
	public bool canRotate = true;

    public float projectileSpeed = 100f;// Speed in m/s

	private float timeLastFired = 0f;	// Last time we fired a projectile
	private float timeLastSearch = 0f;	// Last time we searched for enemies 
	private float searchTimeout = 1f;	// Time in seconds in between searching for enemies

	private bool isPlaced = false;

    private GameObject target = null;
	private float targetSpeed = 0;

    // Projectile
    public Transform projectile;
	public GameObject projectilePrefab;

	public void Start(){
		StartTower ();
	}

	// Use this for initialization
	public void StartTower(){
		isPlaced = true;
	}

	public void Update(){
		// If the tower has not been placed, do nothing
		if (!isPlaced)
			return;

		// Recheck if our target has gone out of range
		if (target && IsTooFar(target))
			target = null;

		// Find a new target in case we don't have one
		if (target == null) {
			target = FindNextTarget ();		
		}

        if (target != null && canRotate){
			
			float alignFactor = Vector3.Dot(transform.forward, (target.transform.position - transform.position).normalized);

			// If we're somewhat facing the target, fire at it
			if (alignFactor > 0.95) {
				FireProjectile (transform.forward);
			}

			// If We're not aligned with the target, rotate towards it
			if (alignFactor < 1){
				// Target slightly ahead of target
				Vector3 targetDir = (target.transform.position + (targetSpeed * .5f * target.transform.forward) )- transform.position;
				// prevent Tower from staring at the ground
				targetDir.y = transform.position.y;

				// Calculate new direction and apply
				Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * Time.deltaTime, 0.0F);
				transform.rotation = Quaternion.LookRotation(newDir, Vector3.up);   
			}
        }

		return;
    }

	// Quick method for finding stray targets
	private bool IsTooFar(GameObject obj){
		return (transform.position - obj.transform.position).sqrMagnitude > range * range;
	}

	// Finds the closest target that's in range of the turret
	private GameObject FindNextTarget(){
		// Return if the timeout time has not passed since the last search
		if ( timeLastSearch + searchTimeout > Time.time)
			return null;
		timeLastSearch = Time.time;

		// Look for nearest GameObject with tag "enemy", within range
		GameObject[] targetList = GameObject.FindGameObjectsWithTag("enemy");

		float ran2 = range * range;
		float minDist = float.PositiveInfinity;
		GameObject foundTarget = null;

		foreach (GameObject t in targetList)
		{
			if (t == null)
				continue;			
			// Calculate distance squared, and if less than current min dist, aquire target
			float dist2 = (transform.position - t.transform.position).sqrMagnitude;
			if (dist2 <= ran2 && dist2 < minDist)
				foundTarget = t;
		}

		// Return found target, or null if none in range
		if (foundTarget != null) {
			targetSpeed = foundTarget.GetComponent<NavMeshAgent> ().speed;
			return foundTarget;
		}
		return null;
	}
		

	// Fire a projectile in the given direction
	private void FireProjectile(Vector3 direction){
		// Return if the timeout time has not passed since we last fired
		if ( timeLastFired + fireRate > Time.time)
			return;
		timeLastFired = Time.time;

		GameObject proj = (GameObject) Instantiate (projectilePrefab, transform.position+transform.forward, Quaternion.identity);

		//proj.transform.position = transform.position + Vector3.up + transform.forward;

		ProjectileBehaviour pb = proj.GetComponent<ProjectileBehaviour>();
		if (pb != null)
		{
			pb.damage = damage;   // tower damage transferred to the projectile
			pb.reach = 2 * range;     // projectile reach set as twice the tower's range
			pb.speed = projectileSpeed;
			pb.target = target.transform;
			pb.parentTagName = gameObject.tag;
		}

	}

    // Instantiates and places in the world a projectile directed towards the target.
    private void LaunchProjectile()
    {
        // Shoot ony if there is a target.
        if (target != null)
        {
            Vector3 aim = TakeAim();

            //transform.LookAt(aim);
            ProjectileBehaviour pb = (ProjectileBehaviour)projectile.GetComponent("ProjectileBehaviour");
            if (pb != null)
            {
                pb.damage = damage;   // tower damage transferred to the projectile
                pb.reach = 2 * range;     // projectile reach set as twice the tower's range
                pb.speed = projectileSpeed;
                pb.target = target.transform;
                pb.parentTagName = gameObject.tag;
                Instantiate(projectile, transform.position, Quaternion.LookRotation(Disperse(aim)));
            }
        }
    }

    // Computes and returns an interception position taking into account target position and projectile speed.
    private Vector3 TakeAim()
    {
        /*
         * It's dubious if all this mess helps improving hit rate or not...
         * */
        Vector3 targetVel = target.GetComponent<NavMeshAgent>().velocity;
        Vector3 towerPos = transform.position;
        Vector3 targetPos = target.transform.position;
        Vector3 targetRelPos = targetPos - towerPos; // target position relative to the tower
        float h = targetPos.y - towerPos.y;

        // Compute X component of the interception coord.
        float w = (projectileSpeed / targetVel.x) * (projectileSpeed / targetVel.x);
        float phi = (targetVel.z / targetVel.x);

        float a = w - phi * phi - 1;
        float b = 2 * (targetPos.x * (phi * phi - w) - phi * targetRelPos.z + towerPos.x);
        float c = targetPos.x * (targetPos.x * (w - phi * phi) + 2 * phi * targetRelPos.z)
            - h * h - targetRelPos.z * targetRelPos.z;

        if (b * b < 4 * a * c)
        {
            return targetPos;  // No possible solution!   
        }
        // 2 possible solutions
        float x_1 = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        float x_2 = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        // Z component is much easily computed once we know X. Again, 2 possible solutions
        Vector3 intercept_1 = new Vector3(x_1, targetPos.y, (targetVel.z / targetVel.x) * (x_1 - targetPos.x) + targetPos.z);
        Vector3 intercept_2 = new Vector3(x_2, targetPos.y, (targetVel.z / targetVel.x) * (x_2 - targetPos.x) + targetPos.z);

        // Return best interception coord.
        if (Vector3.Distance(intercept_1, transform.position) > Vector3.Distance(intercept_2, transform.position))
            return intercept_1;
        else
            return intercept_2;
    }

    private Vector3 Disperse(Vector3 aiming)
    {
        return aiming;
    }
}

