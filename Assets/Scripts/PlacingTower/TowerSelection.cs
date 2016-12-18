using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class TowerSelection : MonoBehaviour
{

    public Score score;

    // Tower deletion stats
    public float paybackRate = 0.66f;   // controls how much credit is obtained from deleting a tower

    private GameObject tower;           // Tower selected by the user
    private TowerBehavior towerBehavior;
    private string towerName;
    private int towerLevel;
    private int upgradeCost;
    private Color32 colorInicial;       // Renderer color of the selected tower

    // XML read variables
    private XmlNode root;
    private XmlNodeList towerList;
    private XmlNodeList towerNode;

    // Use this for initialization
    void Start()
    {
        // Define buttons size and position

        

        // Read xml document and get towers stats
        TextAsset textAsset = (TextAsset)Resources.Load("Xml/towers");
        XmlDocument newXml = new XmlDocument();
        newXml.LoadXml(textAsset.text);
        root = newXml.DocumentElement;

        // Score variable to show the end-of-game stats (towers sold, upgraded...)
        score = GameObject.Find("GameScripts").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check click
        if (Input.GetMouseButtonDown(0) && LogicConnector.isInGame())
        {

            // Check if mouse pressed over sell/upgrade buttons
            RaycastHit hit = new RaycastHit();
            // Ray that goes from the screen (camera) to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                Vector3 v3Pos = Input.mousePosition;
                v3Pos.y = Screen.height - v3Pos.y;
                Rect rectUpgrade = LogicConnector.getRectUpgrade();
                Rect rectSell = LogicConnector.getRectSell();

                if (v3Pos.x >= rectUpgrade.x && v3Pos.x <= rectUpgrade.x + rectUpgrade.width && v3Pos.y >= rectUpgrade.y && v3Pos.y <= rectUpgrade.y + rectUpgrade.height && LogicConnector.getTowerSelected())
                {

                    LogicConnector.setUpgradeSelected(true);
                    upgradeTower();
                    setTowerVariables();

                }
                else if (hit.transform.gameObject.tag == "tower")
                {
                    if (tower != null)
                        tower.GetComponent<Renderer>().material.color = colorInicial;

                    // get the new selection
                    tower = hit.transform.gameObject;
                    // store the initial color
                    colorInicial = tower.GetComponent<Renderer>().material.color;
                    // set blue color for a selected tower
                    tower.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 125);

                    // means OnGUI method will draw the buttons and evaluate the click on them
                    LogicConnector.setTowerSelected(true);
                    setTowerVariables();
                }
                // Check if we clicked a tower: select a new one and draw the buttons from OnGUI
                
                else if (v3Pos.x >= rectSell.x && v3Pos.x <= rectSell.x+ rectSell.width && v3Pos.y >= rectSell.y && v3Pos.y <= rectSell.y+ rectSell.height && LogicConnector.getTowerSelected())
                {
                    LogicConnector.setSellSelected(true);
                    sellTower();
                    setTowerVariables();


                }

                // If we already have a tower selected, reset the color and unselect it
                else
                {
                    if (LogicConnector.getTowerSelected())
                    {
                        // means OnGUI method will do nothing 
                        tower.GetComponent<Renderer>().material.color = colorInicial;
                        // unselect the tower
                        tower = null;
                        LogicConnector.setTowerSelected(false);
                    }

                }              

            }
        }     
           
    }

    // Set tower variables: name, level, towerNode, upgrade cost and upgrade button text
    public void setTowerVariables()
    {

        // Try to obtain behavior from tower (behavior script has the level!)
        towerBehavior = tower.GetComponent<TowerBehavior>();
        if (towerBehavior == null) // Try to obtain behavior from tower's children (aka rotative parts)
            towerBehavior = tower.GetComponentInChildren<TowerBehavior>();

        if (towerBehavior != null)
        {
            // Set variables
            towerName = tower.name;
            towerLevel = towerBehavior.level;

			if (towerName == "torre-piedra")
				LogicConnector.setTowerName ("Stone Tower");
			else if (towerName == "torre-tanque")
				LogicConnector.setTowerName ("Dinotank");
			else if (towerName == "torre-avion")
				LogicConnector.setTowerName ("Triumphal Plane");
			else if (towerName == "torre-muro")
				LogicConnector.setTowerName ("Great wall");
			
            try {
                towerNode = root.SelectNodes("(Towers/Tower[@name='" + towerName + "']/Level)");
                LogicConnector.setTowerDamage(Double.Parse(towerNode[towerLevel-1]["damage"].InnerText));
                LogicConnector.setTowerRange(Double.Parse(towerNode[towerLevel-1]["range"].InnerText));
                LogicConnector.setTowerFirerate(Double.Parse(towerNode[towerLevel-1]["firerate"].InnerText));
                LogicConnector.setTowerHealth(Double.Parse(towerNode[towerLevel-1]["health"].InnerText));
            }
            catch
            {
                towerNode = null;
                Debug.Log("ERROR: could not read the xml element with name " + towerName);
            }

            // Store the upgradeCost if there are possible upgrades and modify upgrade button text
			if (towerNode != null && canUpgrade ()) {
				// towerLevel index works as a list 0,1,2... and not as the xml is done with id 1,2,3...
				// To acces to the level 2 tower stats we use index 1, instead of using a 2 that is the level 2 id, so be careful!
				upgradeCost = Int32.Parse (towerNode [towerLevel] ["cost"].InnerText);

				LogicConnector.setTowerCostUpgrade (upgradeCost);
				LogicConnector.setTowerDamageUpgrade (Double.Parse (towerNode [towerLevel] ["damage"].InnerText));
				LogicConnector.setTowerRangeUpgrade (Double.Parse (towerNode [towerLevel] ["range"].InnerText));
				LogicConnector.setTowerFirerateUpgrade (Double.Parse (towerNode [towerLevel] ["firerate"].InnerText));
                LogicConnector.setTowerHealthUpgrade(Double.Parse(towerNode[towerLevel]["health"].InnerText));

            } else if (towerNode != null && towerName == "torre-muro") {
				// Tower is a WALL
				LogicConnector.setTowerCostUpgrade (0);
				LogicConnector.setTowerDamageUpgrade (0);
				LogicConnector.setTowerRangeUpgrade (0);
				LogicConnector.setTowerFirerateUpgrade (0);
                LogicConnector.setTowerHealthUpgrade(5);
            }
            else
            {

                LogicConnector.setTowerCostUpgrade(0);
            }
        }
    }

    // Sell the selected tower. Give some credit back.
    public void sellTower()
    {
        if (towerBehavior != null) {
            // Increase credit and remove the selected tower
            LogicConnector.increaseCredit((int)(towerBehavior.cost * paybackRate));
            Destroy(tower);

            // Increment towers sold (for the final score)
            score.incTowersSold();
            LogicConnector.setTowerSelected(false);
            LogicConnector.setSellSelected(false);
        }
    }

    // Upgrade the selected tower. Costs some credit.
    public void upgradeTower()
    {
        // Check if we have a tower behavior and if the tower is upgradeable (check actual tower max level)
        if (towerBehavior != null && canUpgrade())
        {
            // Modify tower stats if we have enough credit (we have already checked if there are possible upgrades)
            if (upgradeCost <= LogicConnector.getCredit())
            {
                Debug.Log("SUCCESSFUL upgrade");
                // Decrease credit 
                LogicConnector.decreaseCredit(upgradeCost);

                // Update stats
                towerBehavior.setRange(float.Parse(towerNode[towerLevel]["range"].InnerText));
                towerBehavior.setHealth(float.Parse(towerNode[towerLevel]["health"].InnerText));
                towerBehavior.setDamage(float.Parse(towerNode[towerLevel]["damage"].InnerText));
                towerBehavior.setFireRate(float.Parse(towerNode[towerLevel]["firerate"].InnerText));
                towerBehavior.setTurnSpeed(float.Parse(towerNode[towerLevel]["turnspeed"].InnerText));
                towerBehavior.setProjectileSpeed(float.Parse(towerNode[towerLevel]["projectilespeed"].InnerText));
                // add total cost, for selling purposes (returning a proportion of the total cost: initial + upgrades)
                towerBehavior.addCost(Int32.Parse(towerNode[towerLevel]["cost"].InnerText));

                // Update tower level
                towerLevel += 1;
                towerBehavior.setLevel(towerLevel);
            }
            else
            {
                Debug.Log("ERROR no money for upgrade");
            }
        }
        LogicConnector.setUpgradeSelected(false);
    }

    // Return whether the selected tower can be upgraded
    public Boolean canUpgrade()
    {
        // True if level is lower than maximum levels of the selected tower
        return towerLevel < towerNode.Count;
    }
}

