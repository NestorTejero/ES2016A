using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSelection : MonoBehaviour
{
    // Tower deletion stats
    public float paybackRate = 0.66f;   // controls how much credit is obtained from deleting a tower

    private GameObject tower;           // Tower selected by the user
    private bool showSelected = false;  // Variable to check if a tower is selected (used on the OnGUI method to show/hide the buttons)
    private bool newTower = false;      // variable to control the case we are placing a tower
    private Color32 colorInicial;       // Renderer color of the selected tower

    // Buttons
    private Rect upgrade_button;
    private Rect sell_button;


    // Use this for initialization
    void Start()
    {
        upgrade_button = new Rect(Screen.width / 10, Screen.height / 10, 100, 30);
        sell_button = new Rect(Screen.width / 10, Screen.height / 10 + 50, 100, 30);
    }

    // Update is called once per frame
    void Update()
    {
        // Check click
        if (Input.GetMouseButtonDown(0))
        {
            // conversion from GUI to Screen position
            Vector3 v3Pos = Input.mousePosition;
            v3Pos.y = Screen.height - v3Pos.y;

            // Check if mouse pressed over sell/upgrade buttons
            if (upgrade_button.Contains(v3Pos) || sell_button.Contains(v3Pos))
            {
                // NOTHING: 
                // Just wait for the OnGUI method to do its functionalities
            }
            else
            {
                RaycastHit hit = new RaycastHit();
                // Ray that goes from the screen (camera) to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // If we already have a tower selected, reset the color and unselect it
                    if (tower != null)
                    {
                        // set back the initial renderer color of the tower
                        tower.GetComponent<Renderer>().material.color = colorInicial;
                        // unselect the tower
                        tower = null;
                    }

                    // Check if we clicked a tower: select a new one and draw the buttons from OnGUI
                    if (hit.transform.gameObject.tag == "tower")
                    {
                        // get the new selection
                        tower = hit.transform.gameObject;
                        // store the initial color
                        colorInicial = tower.GetComponent<Renderer>().material.color;
                        // set blue color for a selected tower
                        tower.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 125);

                        // means OnGUI method will draw the buttons and evaluate the click on them
                        this.showSelected = true;
                    }
                    // If not, clear the buttons from OnGUI
                    else
                    {
                        // means OnGUI method will do nothing 
                        this.showSelected = false;
                    }
                }
            }
        }   
    }

    // NOT YET (Open the tower combat stats and)
    // display 2 buttons for selling and upgrading the tower 
    void OnGUI()
    {
        // If we have a selected tower display the buttons and give the functionality
        if (showSelected)
        {
            
            // Upgrade button
            if (GUI.Button(upgrade_button, "UPGRADE"))
            {
                //UPGRADE functionality
            }

            // Sell button
            if (GUI.Button(sell_button, "SELL"))
            {
                // SELL functionality
                sellTower(tower);
            }
        }
    }

    // Open the tower combat stats and display 2 buttons for selling and upgrading the tower 
    void OnMouseDown()
    {
        //this.showSelected = true;
        
    }

    // Sell the given game object if it has tower behavior component. Give some credit back.
    public void sellTower(GameObject go)
    {
        // Try to obtain behavior from tower
        TowerBehavior tb = go.GetComponent<TowerBehavior>();
        if (tb == null) // Try to obtain behavior from tower's children (aka rotative parts)
            tb = go.GetComponentInChildren<TowerBehavior>();   

        if (tb != null)
        {
            // Increase credit and remove game object
            LogicConnector.increaseCredit((int)(tb.cost * paybackRate));
            Destroy(tower);
        }
    }
}

