using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPosition : MonoBehaviour
{
    private string[] detectionList = {"tower", "home", "enemy", "spawn", "wasteland"};

    public List<Collider> colliders = new List<Collider>();

    // If tower collides with another object in the detection list add it to the colliders list
    void OnTriggerEnter(Collider c)
    {   
        foreach(string tag in detectionList)
        {
            if (c.tag == tag)
                colliders.Add(c);
        }
    }


    void OnTriggerStay(Collider c)
    {
        foreach (string tag in detectionList)
        {
            if (c.tag == tag)
                gameObject.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 125);
        }
    }

    // If tower exits collision with an object remove it from the colliders list
    // (now only with towers)
    void OnTriggerExit(Collider c)
    {
        foreach (string tag in detectionList)
        {
            if (c.tag == tag)
            {
                colliders.Remove(c);
                gameObject.GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 125);
            }    
        }
    }
}
