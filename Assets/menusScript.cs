using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menusScript : MonoBehaviour {

    public Canvas quitMenu;
    public Button startText;
    public Button endText;

	// Use this for initialization
	void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        endText = endText.GetComponent<Button>();
        quitMenu.enabled = false;
        
	}

    public void ExitPress() {
        quitMenu.enabled = true;
        startText.enabled = false;
        endText.enabled = false;
    }

    public void NoPress() {
        quitMenu.enabled = false;
        startText.enabled = true;
        endText.enabled = true;
    }

    public void startLevel() {
        Application.LoadLevel(1);
        //SceneManager.LoadScene(1);
    }

    public void exitGame() {
        Application.Quit();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
