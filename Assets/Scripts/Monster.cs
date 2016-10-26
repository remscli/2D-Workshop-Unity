using System;
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
public class Monster : MonoBehaviour {

	Character character;

	bool isDead;
	float intialPosX;
	public float MaxWalkingDistance = 2;

	// Direction
	public int direction = 1;
	public int Direction {
		get { return direction; }
		set {
			if (direction == value)
				return;

			direction = value;

			GetComponent<SpriteRenderer> ().flipX = direction == -1;
		}
	}

	// Use this for initialization
	void Start () {
		character = GetComponent<Character> ();
		intialPosX = transform.position.x;
		changeDirection (1);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			float walkedDistance = transform.position.x - intialPosX;
			if (walkedDistance <= -MaxWalkingDistance) {
				changeDirection (1);
			} else if (walkedDistance >= MaxWalkingDistance) {
				changeDirection (-1);
			}

			character.Walk (Direction);
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		foreach(ContactPoint2D contact in collision.contacts)
		{
			if (collision.collider.tag == "hero" && contact.normal.y < -0.8) {
				character.SetAnimatorValue ("isDead", true);
				Invoke ("destroy", 0.3f);
			}
		}

		if (collision.gameObject.tag != "ground") {
			changeDirection (Direction == 1 ? -1 : 1);
		}
	}

	private void destroy () {
		character.Destroy ();
	}

	private void changeDirection (int val) {
		Direction = val;
	}
}
