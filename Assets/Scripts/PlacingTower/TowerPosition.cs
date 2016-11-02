using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPosition : MonoBehaviour
{

    public List<Collider> colliders = new List<Collider>();

    // If tower collides with another object add it to the colliders list
    // (now only with towers)
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "tower" || c.tag == "home" || c.tag == "enemy")
        {
            colliders.Add(c);
        }
    }


    void OnTriggerStay(Collider c)
    {

        if (c.tag == "tower" || c.tag == "home" || c.tag == "enemy")
        {
            gameObject.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 125);
         

        }

    }

    // If tower exits collision with an object remove it from the colliders list
    // (now only with towers)
    void OnTriggerExit(Collider c)
    {
        if (c.tag == "tower" || c.tag == "home" || c.tag == "enemy")
        {
            colliders.Remove(c);
            gameObject.GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 125);
        }
    }
}
