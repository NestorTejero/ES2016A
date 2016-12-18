using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class finalOne : MonoBehaviour {

	private bool loadedScene = false;
	private bool complete = false;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;
	float timeCounter = 0;
	float speed2;
	float widht;
	float height;


	/*------------------------------------------ MAIN VARIABLES ------------------------------------------*/
	[Header("Visuals Components to the script")]
	public Image fadeOverlay;
	// The container for our progess bar
	public Image loadingBackgroundIcon;
	public Image loadingDoneIcon;
	public Image loadingDoneIcon2;
	public Image loadingDoneIcon3;
	// Remember to set it up as 'filled' mode ;)
	public Image progressBar; 
	public Text loadingText;
	public Text doneText;
	public Image LoadingImage;
	public Image doneImage;
	public Transform TextIndicator; 
	public Image circle1;
	public Image circle2;
	public Image circle3;

	[Header("Timing Settings")]
	public float waitOnLoadEnd = 0.50f;
	public float fadeDuration = 0.50f;

	[Header("Loading Settings")]
	public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
	public ThreadPriority loadThreadPriority;

	[Header("Other")]
	// If loading additive, link to the cameras audio listener, to avoid multiple active audio listeners
	public AudioListener audioListener;

	// Use this for initialization
	void Start () {
		speed2 = 2;
		widht = 2;
		height = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if (loadedScene == false) {
			loadedScene = true;
			StartCoroutine( HandleIt() );
			StartCoroutine (LoadNewScene ());
		} else {
			FadeIn ();
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
			rotateCircles ();
			//rotateCircles2 ();
			if (currentAmount < 100) {
				currentAmount += speed * Time.deltaTime;
				TextIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString () + "%";

			} else {
				TextIndicator.gameObject.SetActive (false);
				loadingText.gameObject.SetActive (false);
				LoadingImage.gameObject.SetActive (false);
				doneText.gameObject.SetActive (true);
				doneImage.gameObject.SetActive (true);
				circle1.gameObject.SetActive (false);
				circle2.gameObject.SetActive (false);
				circle3.gameObject.SetActive (false);
				//loadingDoneIcon.gameObject.SetActive (true);
				//loadingDoneIcon2.gameObject.SetActive (true);
				//loadingDoneIcon3.gameObject.SetActive (true);
				complete = true;
				FadeOut ();
			}

			progressBar.GetComponent<Image>().fillAmount = currentAmount / 100;
		}
	}

	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	IEnumerator LoadNewScene() {

		while (complete == false) {
			// This line waits for 3 seconds before executing the next line in the coroutine.
			// This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
			yield return new WaitForSeconds(1);
		}

		yield return new WaitForSeconds(0.5f);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(3);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}

	}

	void FadeOut() {
		
		fadeOverlay.CrossFadeAlpha(1, 0.4f, false);
	}

	/* I guess this starts the Fade Image at the beginning...xD */
	void FadeIn() {
		fadeOverlay.CrossFadeAlpha(0, 0.3f, true);
	}

	private IEnumerator HandleIt()
	{
		FadeIn ();
		yield return new WaitForSeconds( 0.3f );
	}


	void rotateCircles(){
		// Check and fix this for the next 


		float x1 = circle1.transform.position.x;
		float y1 = circle1.transform.position.y;

		float x2 = circle2.transform.position.x;
		float y2 = circle2.transform.position.x;

		float x3 = circle3.transform.position.x;
		float y3 = circle3.transform.position.x;

		//circle1.transform.position = new Vector3 (x3 * Time.deltaTime, y3 * Time.deltaTime, 0);
		//circle3.transform.position = new Vector3 (x2 * Time.deltaTime, y2 * Time.deltaTime, 0);
		//circle2.transform.position = new Vector3 (x1 * Time.deltaTime, y1 * Time.deltaTime, 0);
		//circle1.transform.Translate(x3, y3, 0, Space.Self);
		//circle3.transform.Translate(x2, y2, 0, Space.Self);
		//circle2.transform.Translate(x1, y1, 0, Space.Self);
		circle1.transform.Rotate (Vector3.up, 3f);
		circle2.transform.Rotate (Vector3.up, 3f);
		circle3.transform.Rotate (Vector3.up, 3f);


	}

	void rotateCircles2(){
		timeCounter += Time.deltaTime * speed2;

		float x = Mathf.Cos(timeCounter) * widht;
		float y = Mathf.Sin(timeCounter) * height;

		circle1.transform.position = new Vector3 (x, y, 0);
		//circle3.transform.position = new Vector3 (x, y, 0);
		//circle2.transform.position = new Vector3 (x, y, 0);


	}

}
