using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPosition : MonoBehaviour {

    public List<Collider> colliders = new List<Collider>();

    // If tower collides with another object add it to the colliders list
    // (now only with towers)
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "tower")
        {
            colliders.Add(c);
        }
    }

    // If tower exits collision with an object remove it from the colliders list
    // (now only with towers)
    void OnTriggerExit(Collider c)
    {
        if (c.tag == "tower")
        {
            colliders.Remove(c);
        }
    }
}
