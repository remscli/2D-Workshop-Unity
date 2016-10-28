using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	public float BoostForce = 150000;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "hero") {
			Rigidbody2D rigidbody = collider.gameObject.GetComponent<Rigidbody2D> ();
			rigidbody.velocity = new Vector2(0, 0);
			collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector3(0, BoostForce));
		}
	}
}
