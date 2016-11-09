﻿using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

    // Stats
    public float range = 200f;
    public float damage = 5f;
    public float health = 100f;
    public float firerate = 1f;     // fire rate in seconds
    public float spread = 0f;       // Spread of the gun (non laser-like aim)
    public float projectileSpeed = 100f;

    private float idleTime = 0f;
    private float idleTimeout = 2f;
    private GameObject target = null;

    // Projectile
    public Transform projectile;

    // Use this for initialization (called when tower is placed)
    public void StartTower () {
        InvokeRepeating("LaunchProjectile", firerate, firerate);
	}
	
	// Update is called once per frame
	void Update () {
        // The sole purpose of this function is to get a target. Shooting is managed outside.
	    if(target == null){

            // Idle out if we just searched
            idleTime -= Time.deltaTime;
            if (idleTime > 0f)
                return;

            // Look for nearest GameObject with tag "enemy", within range
            GameObject[] targetList = GameObject.FindGameObjectsWithTag("enemy");

            foreach(GameObject t in targetList) {
                // Calculate distance squared
                float dist2 = (transform.position - t.transform.position).sqrMagnitude;
                if(dist2 <= range * range) {
                    target = t;
                    break;
                }
            }

            if (target != null) {
                Debug.Log("Found enemy:" + target.ToString());
            }else{
                // Set an idle timeout to prevent performance impact of checking all enemies every frame
                idleTime += idleTimeout;
            }

            // TODO: Take aim

            return;
        }
	}

    // Instantiates and places in the world a projectile directed towards the target.
    private void LaunchProjectile()
    {
        // Shoot ony if there is a target.
        if (target != null)
        {
            ProjectileBehaviour pb = (ProjectileBehaviour)projectile.GetComponent("ProjectileBehaviour");
            if (pb != null)
            {
                pb.damage = damage;   // tower damage transferred to the projectile
                pb.reach = 2 * range;     // projectile reach set as twice the tower's range
                pb.speed = projectileSpeed;
                pb.target = target.transform;
                pb.parentTagName = gameObject.tag;      
                Instantiate(projectile, transform.position, Quaternion.identity);
            }
        }
    }
}