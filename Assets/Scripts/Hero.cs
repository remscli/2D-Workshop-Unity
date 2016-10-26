using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
public class Hero : MonoBehaviour {

	Character character;

	public int JumpingForce = 20000;
	public LayerMask platformsLayer;

	bool isDead = false;
	bool isOnAPlatform = false;

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
				character.rigidBody.AddForce (new Vector3(0, JumpingForce));
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
		if (isDead) return;
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
		if(collision.gameObject.tag == "ground" || 
			collision.gameObject.tag == "monsters" || 
			collision.gameObject.tag == "objects") {
			IsGrounded = true;
		} 

		foreach(ContactPoint2D contact in collision.contacts)
		{
			if (collision.collider.tag == "monsters" && (contact.normal.x > 0.8 || contact.normal.x < -0.8)) {
				// If the collisioned monster is on a platform and we are on the platform too
				if (collision.gameObject.layer == 8 && !isOnAPlatform) {
					return;
				}

				die();
				// Invoke ("gameOver", 0.3f);
			}
		}
			
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, 1, platformsLayer.value);
		// If user is on a platform
		if (hit) {
			isOnAPlatform = true;
			shouldIgnoreCollisionsWithPlatformMonsters (false);
		} else {
			isOnAPlatform = false;
			shouldIgnoreCollisionsWithPlatformMonsters (true);
		}
	}

	void die () {
		character.SetAnimatorValue ("isDead", true);
		isDead = true;
	}

	private void shouldIgnoreCollisionsWithPlatformMonsters (bool ignore) {
		Physics2D.IgnoreLayerCollision (10, 8, ignore);
	}
}
