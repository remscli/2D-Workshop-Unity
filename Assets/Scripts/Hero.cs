using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
public class Hero : MonoBehaviour {

	Character character;

	// Grounded
	bool isGrounded;
	public bool IsGrounded {
		get { return isWalking; }
		set { 
			if (isGrounded == value) return;

			isGrounded = value;

			character.SetAnimatorValue ("isGrounded", isGrounded);
		}
	}

	// Walk
	bool isWalking;
	public bool IsWalking {
		get { return isWalking; }
		set { 
			if (isWalking == value) return;

			isWalking = value;

			character.SetAnimatorValue ("isWalking", isWalking);
		}
	}

	// Jump
	bool isJumping;
	public bool IsJumping {
		get { return isJumping; }
		set { 
			if (isJumping == value) return;

			character.SetAnimatorValue ("isJumping", isJumping = value);

			if (isJumping && isGrounded) {
				character.rigidBody.AddForce (new Vector3(0, 250));
				IsGrounded = false;
			}
		}
	}
		
	// Direction
	int direction = 0;
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
	}
	
	// Update is called once per frame
	void Update () {
		IsJumping = Input.GetKey (KeyCode.UpArrow);

		if (Input.GetKey (KeyCode.LeftArrow)) {
			Direction = -1;
			IsWalking = true;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			Direction = 1;
			IsWalking = true;
		} else {
			Direction = 0;
			IsWalking = false;
		}
	}

	void FixedUpdate () {

		if (isGrounded) {
			if (character) {
				character.Walk (Direction);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if(collision.gameObject.tag == "ground" || collision.gameObject.tag == "monster") {
			IsGrounded = true;
		}
	}
}
