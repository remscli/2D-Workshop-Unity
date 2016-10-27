using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour {

	public GemsManager gemsManager;
	public Sprite SuccessBackground;
	public Sprite FailureBackground;
	Image bgImage;
	int collectedGemsCount = 0;
	int totalGemsCount;
	bool isCompleted = false;

	// Use this for initialization
	void Start () {
		totalGemsCount = GameObject.Find ("Gems").transform.childCount;
		Transform backgroundTransform = transform.FindChild ("Background");
		bgImage = backgroundTransform.GetComponent<Image> ();
		Hide ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			OnRetryButtonClick ();
		}
	
	}

	public void Show (bool state) {
		isCompleted = state;
		updateCollectedGemsInformations ();
		displayBackground ();
		gameObject.SetActive (true);
		gemsManager.gameObject.SetActive (false);
	}

	public void Hide () {
		gameObject.SetActive (false);
		gemsManager.gameObject.SetActive (true);
	}

	public void OnRetryButtonClick () {
		SceneManager.LoadScene ("Scene 1");
	}

	void updateCollectedGemsInformations () {
		collectedGemsCount = gemsManager.GetCollectedGemsCount ();

		if (collectedGemsCount.Equals(totalGemsCount)) {
			Transform label = transform.FindChild ("CollectedGemsLabel");
			label.GetComponent<Text> ().text = "You've collected all the gems !";
		}

		Transform collectedItems = transform.FindChild ("CollectedGems").transform;

		int i = 0;
		foreach (Transform child in collectedItems){
			Image image = child.GetComponent<Image> ();
			Color color = image.color;
			 // Set image opacity to 1 if this gems has been collected, 0.4 if not
			if (i < collectedGemsCount) {
				color.a = 1.0f;
			} else {
				color.a = 0.4f;
			}

			image.color = color;

			i++;
		}
	}

	void displayBackground () {
		if (isCompleted) {
			bgImage.sprite = SuccessBackground;
		} else {
			bgImage.sprite = FailureBackground;
		}
	}
}
