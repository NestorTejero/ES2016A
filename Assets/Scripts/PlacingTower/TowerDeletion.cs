using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerDeletion : MonoBehaviour
{
    // Tower deletion stats
    public float paybackRate = 0.25f;
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
                tower = hit.transform.gameObject;
            }
            if (Input.GetMouseButtonDown(0) && tower != null)
                RemoveTower();
        }   
    }

    private void RemoveTower()
    {
        TowerBehavior tb = tower.GetComponent<TowerBehavior>();
        if (tb != null)
        {
            LogicConnector.increaseCredit( (int) (tb.cost * paybackRate));
        }
        Destroy(tower);
        tower = null;
        enabled = false;
    }
}

