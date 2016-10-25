using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
public class Monster : MonoBehaviour {

	Character character;

	// Use this for initialization
	void Start () {
		character = GetComponent<Character> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D collider) {
		character.SetAnimatorValue ("isDead", true);
		Invoke ("kill", 0.3f);
	}

	private void kill () {
		character.Destroy ();
	}
}
