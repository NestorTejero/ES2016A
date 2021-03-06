﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPlacement : MonoBehaviour
{
    public Score score;
    private Transform newTower;   
    private GameObject tow;
    private TowerPosition towPos;
    private Color32 colorInicial;
    public bool isPlaced;
    private int towerCost;
    private float numHeightinf;
    private float numHeightsup;
    private float numWidth1;
    private float numWidth2;
    public int type_tower; // 0 -> dinosaur / 1 -> tank / 2 -> tower

    public Vector3 scale = new Vector3(10,10,10);
    public float scrollSpeed = 250f;       // tower placement rotation


    // Use this for initialization
    void Start()
    {
        score = GameObject.Find("GameScripts").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {

        // Updating the tower position (and placing it on click)
        if (newTower != null && !isPlaced)
        {
            // Check for mouse wheel. Rotate new tower if scroll
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                // Inversed Y axis
                newTower.transform.Rotate(new Vector3(0, -scroll * scrollSpeed * 10 * Time.deltaTime, 0));             
            }

            // Disable TowerSelection script cause we do not want to select towers while placing one
            GameObject.Find("GameScripts").GetComponent<TowerSelection>().enabled = false;
            // Disable Camera zoom, freeing scroll input
            GameObject.Find("Camera").GetComponent<CameraMovement>().zoomEnabled = false;

            RaycastHit hit = new RaycastHit();
            // Ray that goes from the screen (camera) to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Getting the position of the terrain we are pointing at
            // (= ray collision with the terrain, hit is the output)
            if (Terrain.activeTerrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                //Adaptacion de la pantalla al HUD inferior (definimos el HUD como un rectángulo)
                numHeightinf = 54 + (Screen.height - 330) / 6f;
                numWidth1 = 206 + (Screen.width - 289) / 1.3891f;
                numWidth2 = 82 + (Screen.width - 289) / 3.5697f;

                //Adaptacion de la pantalla al HUD superior
                numHeightsup = 502 + (Screen.height - 539) / 1.049f;

                // Point of the terrain where we are aiming at
                Vector3 point = hit.point;
                // Giving the position to the newTower (using the height of the object itself for y axis and mouse  for x and z axis)
                newTower.transform.position = new Vector3(point.x, newTower.position.y, point.z);
                // Cancell the tower placement by using right click and the Tower is not placed if clicking over the HUD
                if (Input.GetMouseButtonDown(1) || (Input.GetMouseButtonDown(0) && Input.mousePosition.y < numHeightinf
                      && Input.mousePosition.x < numWidth1 && Input.mousePosition.x > numWidth2) ||
                      (Input.GetMouseButtonDown(0) && Input.mousePosition.y > numHeightsup))
                {
                    Destroy(tow);
                }
                // Placing the tower on Click
                // (need to check newTower collision with other objects, not just other towers)
                else if (Input.GetMouseButtonDown(0) && CanPlace())
                {
                    // If it is placed no need to update its position again
                    newTower.GetComponent<Renderer>().material.color = colorInicial;
                    Destroy(newTower.GetComponent<TowerPosition>());
                    isPlaced = true;
                    newTower.GetComponent<NavMeshObstacle>().enabled = true;

                    if (newTower.GetComponent<TowerBehavior>() != null)     // One-object prefab
                    {
                        newTower.GetComponent<TowerBehavior>().cost = towerCost;    // set tower value
                        newTower.GetComponent<TowerBehavior>().StartTower();
                    }
                    else    // Prefab composed by multiple objects
                    {
                        // Beware! We are assuming that tower has only one child!
                        newTower.GetComponentInChildren<TowerBehavior>().cost = towerCost;    // set tower value
                        newTower.GetComponentInChildren<TowerBehavior>().StartTower();
                    }
                    
					LogicConnector.decreaseCredit(towerCost);
                    score.incTowersBuilt(); // Increment towers built

                }
            }

        }
        else
        {
            // already placed (or null)

            // Re enable TowerSelection script
            GameObject.Find("GameScripts").GetComponent<TowerSelection>().enabled = true;
            // Re enable Camera zoomt
            GameObject.Find("Camera").GetComponent<CameraMovement>().zoomEnabled = true;
        }
    }

    bool CanPlace()
    {
        // Check if colliding or not 
        // (at the moment only with other "towers")
        if (towPos.colliders.Count > 0)
        {
            //Debug.Log("COLLIDING with Tower");
            return false;
        }
        //Debug.Log("not COLLIDING with Tower");
        return true;
    }

    // Instantiating the new tower
    public void SetItem(GameObject go, int type, int cost)
    {
        if (LogicConnector.isInGame())
        {
            // Boolean to check if we have placed the new tower yet (initially false)
            isPlaced = false;
            // Instantiate the tower
            tow = Instantiate(go);
            tow.name = go.name;

            // Adding component to check collision and storing it in towPos variable
            tow.AddComponent<TowerPosition>();
            towPos = tow.GetComponent<TowerPosition>();


            // Tower transform variable
            newTower = tow.transform;
            // Scaling the tower dimensions (to match the initial towers)
            //newTower.transform.localScale = scale;

            // Selecting type of tower
            type_tower = type;

            towerCost = cost;
            colorInicial = newTower.GetComponent<Renderer>().material.color;
            newTower.GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 125);
            newTower.GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}
