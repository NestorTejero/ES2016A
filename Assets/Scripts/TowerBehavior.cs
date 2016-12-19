using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour
{

    // Stats
    public int level = 1;
    public float range = 30f;			// Range in meters
	public float minRange = 0f;			// Minimun distance to target (for some turrets, shooting enemies too close is impossible
	public float health = 100f;
	public int cost = 100;				// tower value

	public float damage = 5f;
    public float fireRate = 1f;    		// fire rate in seconds

	public float turnSpeed = Mathf.PI / 2f;		// Radians per second of the turret
	public bool canRotate = true;
	public bool canRotatePitch = false;

    public float projectileSpeed = 20f;// Speed in m/s

	private float timeLastFired = 0f;	// Last time we fired a projectile
	private float timeLastSearch = 0f;	// Last time we searched for enemies 
	private float searchTimeout = 1f;	// Time in seconds in between searching for enemies

	public bool isPlaced = false;		// Toggle this in editor to test turrets without playing
    public bool isWall = false;         // Wall flag. Object will not shoot if enabled.

    private GameObject target = null;
	private float targetSpeed = 0;

    // Projectile
	public GameObject projectilePrefab;

	public void Start(){
		//StartTower ();
	}

	// Use this for initialization
	public void StartTower(){
		isPlaced = true;
        if (isWall)
            return;

		// Delete markers once placed
		GameObject rangeEnabledMarker = transform.parent.Find("range-enabled").gameObject;
		GameObject rangeDisabledMarker = transform.parent.Find("range-disabled").gameObject;
		if(rangeEnabledMarker)
			Destroy(rangeEnabledMarker);
		if(rangeDisabledMarker)
			Destroy(rangeDisabledMarker);
	}

	public void Update(){
		// If the tower has not been placed, or is a wall, do nothing
		if (!isPlaced || isWall)
			return;

		// Recheck if our target has gone out of range
		if (target && IsTooFar(target))
			target = null;

		// Find a new target in case we don't have one
		if (target == null) {
			target = FindNextTarget ();		
		}

        if (target != null && canRotate){

			Vector3 enemyDir = target.transform.position - transform.position;
			enemyDir.Normalize ();

			// If the turret does not rotate on the X axis, set enemy co-planar
			// TODO this is not working¿?
			if(!canRotatePitch)
				enemyDir.y = transform.position.y;
			float alignFactor = Vector3.Dot(transform.forward, enemyDir);

			// If we're somewhat facing the target, fire at it
			if (alignFactor > 0.95) {
				//FireProjectile (transform.forward);
				FireProjectile();
			}

			// If We're not aligned with the target, rotate towards it
			if (alignFactor < 1){
				// Target slightly ahead of target
				Vector3 targetDir = (target.transform.position + (targetSpeed * .5f * target.transform.forward) ) - transform.position;
				// prevent Tower from staring at the ground
				if(!canRotatePitch)
					targetDir.y = 0f; //transform.position.y;

				// Calculate new direction and apply
				Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * Time.deltaTime, 0.0F);
				transform.rotation = Quaternion.LookRotation(newDir, Vector3.up);   
			}
        }

		return;
    }

	// Determine if target is in range
	private bool IsTooFar(GameObject obj){
		float dist2 = (transform.position - obj.transform.position).sqrMagnitude;
		// Return true if target is too close OR too far
		return ((dist2 < minRange*minRange) || (dist2 > range * range));
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
			if (t == null || IsTooFar(t))
				continue;			
			// Calculate distance squared, and if less than current min dist, aquire target
			float dist2 = (transform.position - t.transform.position).sqrMagnitude;
			if (dist2 <= ran2 && dist2 < minDist) {
				foundTarget = t;
				minDist = dist2;
			}
		}

		// Return found target, or null if none in range
		if (foundTarget != null) {
            NavMeshAgent agent = foundTarget.GetComponent<NavMeshAgent>(); // target's navigation agent
            // Check if target is static
            if (agent.velocity.x == 0 && agent.velocity.z == 0)
                targetSpeed = 0;            // this assignation avoids computing square rooted vector magnitudes
            else
                targetSpeed = agent.speed;  // use speed stat normally if target is not static
			return foundTarget;
		}
		return null;
	}
		

    // Instantiates and places in the world a projectile directed towards the target.
    private void FireProjectile()
    {
		if ( timeLastFired + fireRate > Time.time)
			return;
		timeLastFired = Time.time;

        // Shoot ony if there is a target.
        if (target != null)
        {
            //Vector3 aim = TakeAim();

			// Instantiate new Projectile Prefab, set it's position
			// to the muzzle of the turret and looking forward
			GameObject proj = Instantiate(projectilePrefab,
				transform.Find("muzzle").transform.position,
				Quaternion.LookRotation(transform.forward)
			) as GameObject;

			// Edit Projectile parametres after instance
			ProjectileBehaviour pb = proj.GetComponent<ProjectileBehaviour>();
			pb.damage = damage;   // tower damage transferred to the projectile
			pb.reach = 2 * range;     // projectile reach set as twice the tower's range
			pb.speed = projectileSpeed;
			// Set tags
			pb.parentTagName = gameObject.tag;
			pb.targetTag = target.tag;
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

    // TODO: implement dispersion
    private Vector3 Disperse(Vector3 aiming)
    {
        return aiming;
    }

    // Take damage.
    public void takeDamage(float damage)
    {
        health = Mathf.Max(0, health - damage);
        if (health == 0)
            SelfDestroy();     
    }

    // Can be modified to add cool effects when the entity is destroyed.
	protected virtual void SelfDestroy()
	{
		// Delete parent object if it exists
		if (transform.parent == null)
			Destroy(gameObject);
		else
			Destroy(transform.parent.gameObject);
	}


    // SETTERS for upgrading towers
    public void setLevel(int l)
    {
        this.level = l;
    }
    public void setRange(float r)
    {
        this.range = r;
    }

    public void setHealth(float h)
    {
        this.health = h;
    }

    public void addCost(int c)
    {
        this.cost += c;
    }

    public void setDamage(float d)
    {
        this.damage = d;
    }

    public void setFireRate(float fr)
    {
        this.fireRate = fr;
    }

    public void setTurnSpeed(float ts)
    {
        this.turnSpeed = ts;
    }

    public void setProjectileSpeed(float ps)
    {
        this.projectileSpeed = ps;
    }

}

