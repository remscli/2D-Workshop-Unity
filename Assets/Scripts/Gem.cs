using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {

	GemsManager gemsManager;

	// Use this for initialization
	void Start () {
		gemsManager = GameObject.Find ("gems-manager").GetComponent<GemsManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "hero") {
			Destroy (gameObject);
			gemsManager.NewGemCollected ();
		}
	}
}
