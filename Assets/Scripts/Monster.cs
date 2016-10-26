using System;
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
public class Monster : MonoBehaviour {

	Character character;
	Renderer renderer;
	AudioSource audio;

	bool isDead;
	float intialPosX;
	public float MaxWalkingDistance = 2;

	// Direction
	int direction = 1;
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
		renderer = GetComponent<Renderer> ();
		audio = GetComponent<AudioSource>();
		intialPosX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead)	return;
		
		float walkedDistance = transform.position.x - intialPosX;
		if (walkedDistance <= -MaxWalkingDistance) {
			Direction = 1;
		} else if (walkedDistance >= MaxWalkingDistance) {
			Direction = -1;
		}

		character.Walk (Direction);
	}

	void OnCollisionEnter2D (Collision2D collision) {
		foreach(ContactPoint2D contact in collision.contacts)
		{
			if (collision.collider.tag == "hero" && contact.normal.y < -0.8) {
				die ();
			}
		}

		if (collision.gameObject.tag != "ground") {
			Direction = Direction == 1 ? -1 : 1;
		}
	}

	private void die () {
		audio.Play ();
		character.SetAnimatorValue ("isDead", true);
		StartCoroutine(FadeTo(0.0f, 0.5f));
		Invoke ("destroy", 0.3f);
	}

	private void destroy () {
		character.Destroy ();
	}

	IEnumerator FadeTo (float value, float time) {
		float alpha = renderer.material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,value,t));
			renderer.material.color = newColor;
			yield return null;
		}
	}
}
