﻿using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour
{

    // Stats
    public float range = 200f;
    public float damage = 5f;
    public float health = 100f;
    public float fireRate = 1f;     // fire rate in seconds
    public float spread = 0f;       // Spread of the gun (non laser-like aim)
    public float projectileSpeed = 100f;
    public float scanRate = 0.5f;   // target scanning rate, should be smaller than firerate

    private GameObject target = null;

    // Projectile
    public Transform projectile;

    // Use this for initialization
    public void StartTower()
    {
        InvokeRepeating("LaunchProjectile", fireRate, fireRate);
        InvokeRepeating("SearchTarget", scanRate, scanRate);
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

                //Instantiate(projectile, transform.position, Quaternion.identity);
            }
        }
    }

    // Search for the closest target.
    private void SearchTarget()
    {
        // Look for nearest GameObject with tag "enemy", within range
        GameObject[] targetList = GameObject.FindGameObjectsWithTag("enemy");

        float dist = range * range;
        foreach (GameObject t in targetList)
        {
            if (t == null)
                continue;
            // Calculate distance squared
            float dist2 = (transform.position - t.transform.position).sqrMagnitude;
            if (dist2 <= dist)
                target = t;
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

