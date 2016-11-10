using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPlacement : MonoBehaviour
{

    private Transform newTower;   
    private GameObject tow;
    private TowerPosition towPos;
    private Color32 colorInicial;
    private bool isPlaced;
    private int towerCost;
    public int type_tower; // 0 -> dinosaur / 1 -> tank / 2 -> tower
    public int height;
    public int height_dinosaur = 7; // dinosaur height placement
    public int height_tank = 0; // tank height placement
    public int height_tower = 4; // tower height placement
    public Vector3 scale = new Vector3(10,10,10);


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Updating the tower position (and placing it on click)
        if (newTower != null && !isPlaced)
        {

            RaycastHit hit = new RaycastHit();
            // Ray that goes from the screen (camera) to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Getting the position of the terrain we are pointing at
            // (= ray collision with the terrain, hit is the output)
            if (Terrain.activeTerrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                // Point of the terrain where we are aiming at
                Vector3 point = hit.point;
                // Giving the position to the newTower 
                // (height to 0 to place it on the "terrain height 0" and not on the "mountains")
                // (where we put towers is supposed to be a flat terrain of height 0)
                newTower.transform.position = new Vector3(point.x, height, point.z);
                
                // Placing the tower on Click
                // (need to check newTower collision with other objects, not just other towers)
                if (Input.GetMouseButtonDown(0) && CanPlace())
                {
                    // If it is placed no need to update its position again
                    newTower.GetComponent<Renderer>().material.color = colorInicial;
                    Destroy(newTower.GetComponent<TowerPosition>());
                    isPlaced = true;
                    newTower.GetComponent<TowerBehavior>().StartTower();
					LogicConnector.decreaseCredit(towerCost);


                }
            }

        }
        else
        {
            // already placed (or null)
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
        // Boolean to check if we have placed the new tower yet (initially false)
        isPlaced = false;
        // Instantiate the tower
        tow = Instantiate(go);

        // Adding component to check collision and storing it in towPos variable
        tow.AddComponent<TowerPosition>();
        towPos = tow.GetComponent<TowerPosition>();


        // Tower transform variable
        newTower = tow.transform;
        // Scaling the tower dimensions (to match the initial towers)
        //newTower.transform.localScale = scale;

        // Selecting type of tower
        type_tower = type;

        // tower height placement depends on the type of tower
        switch (type)
        {
            case 0:
                height = height_dinosaur;
                break;
            case 1:
                height = height_tank;
                break;
            case 2:
                height = height_tower;
                break;
        }


        towerCost = cost;
        colorInicial = newTower.GetComponent<Renderer>().material.color;
        newTower.GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 125);
        
    }
}
