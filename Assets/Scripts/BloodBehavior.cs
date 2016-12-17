using UnityEngine;
using System.Collections;

public class BloodBehavior : MonoBehaviour
{
    // PUBLIC STATS   
    public float prevalence = 2f;   // prevalence in seconds

    // Size bounds
    public float minScale = 0.1f;   // dictates minumum size
    public float maxScale = 0.5f;   // dictates maximum size
    // Position bounds
    public float height = 0.02f;    // distance of the stain over the terrain

    // Use this for initialization
    void Start()
    {
        // Bound position and scatter
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        // Bound scale
        transform.localScale = new Vector3(Mathf.Max(minScale, Mathf.Min(maxScale, transform.localScale.x)), 1,
            Mathf.Max(minScale, Mathf.Min(maxScale, transform.localScale.z)));

        Destroy(gameObject, prevalence);    // delete object after prevalence time
    }
}