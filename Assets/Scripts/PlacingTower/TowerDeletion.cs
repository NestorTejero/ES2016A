using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerDeletion : MonoBehaviour
{
    // Tower deletion stats
    public float paybackRate = 0.25f;   // controls how much credit is obtained from deleting a tower
    // Tower stats
    private int towerCost;
    private string tag = "tower";

    private GameObject tower;           // Tower selected by the user


    // Use this for initialization
    void Start()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();
        // Ray that goes from the screen (camera) to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "tower")
            {
                tower = hit.transform.gameObject;       // get selection if game object is a tower
            }
            if (Input.GetMouseButtonDown(0) && tower != null)
            {
                RemoveTower(tower);
                tower = null;           // clear selection
                enabled = false;        // disable button 
            }           
        }   
    }

    // Destroy given game object if it has tower behavior component. Credit increased.
    public void removeTower(GameObject go)
    {
        TowerBehavior tb = go.GetComponent<TowerBehavior>();
        if (tb != null)
        {
            LogicConnector.increaseCredit( (int) (tb.cost * paybackRate));
            Destroy(tower);
        }    
    }
}

