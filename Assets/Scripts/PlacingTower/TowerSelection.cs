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
                    if (hit.transform.tag == "Button")
                    {

                    }
                    // Check if we clicked a tower
                    if (hit.transform.gameObject.tag == "tower")
                    {
                        // If we already have a tower selected, unselect it and reset the color
                        if (tower != null)
                        {
                            // set back the initial renderer color of the tower (if it is not a new tower)
                            if (!newTower)
                            {
                                tower.GetComponent<Renderer>().material.color = colorInicial;
                            }
                            // unselect the tower
                            tower = null;
                        }

                        // get the new selection
                        tower = hit.transform.gameObject;

                        // If we are selecting a tower we are placing (new tower) do not select it
                        if (tower.GetComponent<Renderer>().material.color == new Color32(0, 255, 0, 125) ||
                            tower.GetComponent<Renderer>().material.color == new Color32(255, 0, 0, 125))
                        {
                            tower = null;

                            // set boolean values
                            this.showSelected = false;
                            newTower = true;
                        }
                        // Selecting a tower we have already placed
                        else
                        {
                            // store the initial color
                            colorInicial = tower.GetComponent<Renderer>().material.color;
                            // set blue color for a selected tower
                            tower.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 125);

                            // set boolean values
                            this.showSelected = true;
                            newTower = false;
                        }
                    }
                    // If not, unselect the tower
                    else
                    {
                        if (tower != null)
                        {
                            // set back the initial renderer color of the tower (if it is not a new tower)
                            if (!newTower)
                            {
                                tower.GetComponent<Renderer>().material.color = colorInicial;
                            }
                            // unselect the tower
                            tower = null;
                        }
                        // set boolean values
                        this.showSelected = false;
                        newTower = false;
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
        TowerBehavior tb = go.GetComponent<TowerBehavior>();
        if (tb != null)
        {
            LogicConnector.increaseCredit( (int) (tb.cost * paybackRate));
            Destroy(tower);

            tower = null;

            // set boolean values
            this.showSelected = true;
            newTower = false;
        }    
    }
}

