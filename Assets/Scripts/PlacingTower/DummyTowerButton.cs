using UnityEngine;
using System.Collections;

public class DummyTowerButton : MonoBehaviour {

    public GameObject[] towers; // list of different types of towers (we have only one)
    private TowerPlacement placement;

    // Use this for initialization
    void Start()
    {
        placement = GetComponent<TowerPlacement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Creating the button and giving it functionality
    void OnGUI()
    {
        // Calling method from TowerPlacement, where we give the functionality
        if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 10, 100, 30), towers[0].name))
        {
            placement.SetItem(towers[0], 0);
        }

        // DESTROYER BUTTON!!!
        if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 10 + 50, 100, 30), "DESTROY"))
        {
            DestroyAllTowers();
        }
    }

    private void DestroyAllTowers()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("tower"))
        {
            Destroy(go);
        }
    }
}
