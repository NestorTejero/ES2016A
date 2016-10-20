using UnityEngine;
using System.Collections;


public class EnemyBehavior : MonoBehaviour{

    // Stats
    public float moveSpeed = 5f;
    public float damage = 2.5f;
    public float health = 10f;

    GameObject home;

	// Use this for initialization
	void Start () {
        // Get the Scene's tower
        home = GameObject.FindGameObjectWithTag("home");
        
	}
	
	// Update is called once per frame
	void Update () {
        if (home == null)
            return;

        // Get direction vector to tower
        Vector3 dir = home.transform.position - this.transform.position;
        // Dont move on Y axis (Vertical)
        dir.y = 0;

        if (dir.sqrMagnitude < Mathf.Pow(3,2))
            return;
        // Move X units per second
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);

	}
}
