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
			Hero hero = collision.gameObject.GetComponent<Hero> ();
			hero.IsPlaying = false;
			EndGameUI.Show (true);
		}
	}
}