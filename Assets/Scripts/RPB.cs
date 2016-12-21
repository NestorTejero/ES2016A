using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPB : MonoBehaviour {

	public Transform ProgressBar;
	public Transform TextIndicator;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;
	
	// Update is called once per frame
	void Update () {
		if (currentAmount < 100) {
			currentAmount += speed * Time.deltaTime;
			TextIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString () + "%";

		} else {
			TextIndicator.gameObject.SetActive (false);
		}

		ProgressBar.GetComponent<Image>().fillAmount = currentAmount / 100;
	}
}
