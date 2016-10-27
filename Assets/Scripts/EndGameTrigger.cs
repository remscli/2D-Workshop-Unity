using UnityEngine;
using System.Collections;

public class EndGameTrigger : MonoBehaviour {

	public EndGameUI EndGameUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "hero") {
			Hero hero = collider.gameObject.GetComponent<Hero> ();
			hero.IsPlaying = false;
			EndGameUI.Show (true);
		}
	}
}