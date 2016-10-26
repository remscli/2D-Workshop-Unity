using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

	public GemsManager gemsManager;
	int gemsCount = 0;

	// Use this for initialization
	void Start () {
		Hide ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show () {
		updateCollectedGemsInformations ();
		gameObject.SetActive (true);
	}

	public void Hide () {
		gameObject.SetActive (false);
	}

	public void OnRetryButtonClick () {
		SceneManager.LoadScene ("Scene 1");
	}

	void updateCollectedGemsInformations () {
		gemsCount = gemsManager.GetCollectedGemsCount ();

		Transform collectedItems = transform.FindChild ("CollectedGems").transform;

		int i = 0;
		foreach (Transform child in collectedItems){
			Image image = child.GetComponent<Image> ();
			Color color = image.color;

			if (i < gemsCount) {
				color.a = 1.0f;
			} else {
				color.a = 0.4f;
			}

			image.color = color;

			i++;
		}
	}
}
