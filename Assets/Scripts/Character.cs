using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
public class Character : MonoBehaviour {
	
	public int WalkSpeed = 50;

	[HideInInspector] public Animator animator;
	[HideInInspector] public Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetAnimatorValue (string label, bool value) {
		animator.SetBool (label, value);
	}

	public void Walk (int direction) {
		Vector2 tmpVelocity = rigidBody.velocity;

		if (direction != 0) {
			tmpVelocity.x = WalkSpeed * direction * Time.fixedDeltaTime;
		}

		rigidBody.velocity = tmpVelocity;
	}
}
