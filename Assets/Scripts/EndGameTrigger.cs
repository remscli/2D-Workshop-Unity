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


	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "hero" && collision.contacts[0].normal.y < -0.8) {
			Invoke ("showEndGameUI", 1.0f);
		}
	}

	void showEndGameUI () {
		Hero hero = GameObject.Find("Hero").GetComponent<Hero> ();
		hero.IsPlaying = false;
		EndGameUI.Show (true);
	}
}