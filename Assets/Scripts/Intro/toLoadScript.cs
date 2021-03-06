﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class toLoadScript : MonoBehaviour {

	private bool loadedScene = false;

	[SerializeField]
	private Text loadingText;
	private Image welcomeTo;

	[Header("Visuals Components to the script")]
	public Text nameOfGame;
	public Image conquerorName;
	public Text producers;
	public Image producersImage;

	int count = 0;


	void start() {

	}

	// Update is called once per frame
	void Update () {
		if (loadedScene == false) {
			loadedScene = true;
			StartCoroutine (LoadNewScene ());
		} else {
			//loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
			float d = 0.0f;
		}
	}

	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	IEnumerator LoadNewScene() {

		// This line waits for 3 seconds before executing the next line in the coroutine.
		// This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.

		yield return new WaitForSeconds(1);
		nameOfGame.gameObject.SetActive (true);
		conquerorName.gameObject.SetActive(true);

		yield return new WaitForSeconds(1.5f);
		producers.gameObject.SetActive (true);
		producersImage.gameObject.SetActive (true);

		yield return new WaitForSeconds(1.2f);


		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		//AsyncOperation async = 
		SceneManager.LoadScene("Pantalla");

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		/* (!async.isDone) {
			yield return null;
		}*/

	}

}
