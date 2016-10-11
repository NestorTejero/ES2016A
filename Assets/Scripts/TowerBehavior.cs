using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {
    
    public float range = 10f;

    private float idleTime = 0f;
    private float idleTimeout = 2f;
    private GameObject target = null;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {

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

            return;
        }

        // TODO shoot code here


	}
}
