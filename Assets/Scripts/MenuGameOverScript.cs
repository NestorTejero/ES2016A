using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuGameOverScript : MonoBehaviour {

    public Button Return;
    public Button Continue;
    
    // Use this for initialization
    void Start()
    {
        Return = Return.GetComponent<Button>();
        Continue = Continue.GetComponent<Button>();
    }
    
    public void ReturnPress()
    {
        //Scene sc = SceneManager.GetActiveScene();
        //int numSceneActive = sc.buildIndex;
        SceneManager.LoadScene("Menu");
        //SceneManager.UnloadScene(numSceneActive);

    }

    public void ContinuePress()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
