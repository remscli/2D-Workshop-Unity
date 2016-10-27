using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
public class Hero : MonoBehaviour {

	Character character;

	public int JumpingForce = 20000;
	public LayerMask PlatformsLayer;
	public AudioClip DeathSound;
	public AudioClip JumpSound;
	public AudioClip WalkSound;
	public AudioClip SkidSound;
	public EndGameUI EndGameUI;

	bool isOnAPlatform = false;
	AudioSource audio;

	// Playing
	bool isPlaying = true;
	public bool IsPlaying {
		get { return isPlaying; }
		set { 
			if (isPlaying == value) return;

			isPlaying = value;

			if (!isPlaying) {
				Direction = 0;
				IsWalking = false;
			}
		}
	}

	// Grounded
	bool isGrounded;
	public bool IsGrounded {
		get { return isWalking; }
		set { 
			if (isGrounded == value) return;

			isGrounded = value;

			character.SetAnimatorValue ("isGrounded", isGrounded);

			playSound (JumpSound);
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
	int jumpsCount = 0;
	bool isJumping;
	public bool IsJumping {
		get { return isJumping; }
		set { 
			if (jumpsCount >= 2) return;

			character.SetAnimatorValue ("isJumping", isJumping = value);

			if (isJumping) {
				playSound (JumpSound);
				character.rigidBody.AddForce (new Vector3(0, JumpingForce));
				IsGrounded = false;
				jumpsCount++;
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
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsPlaying) return;
		IsJumping = Input.GetKeyDown (KeyCode.UpArrow);

		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (!IsWalking) playSound (WalkSound, true);
			Direction = -1;
			IsWalking = true;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			if (!IsWalking) playSound (WalkSound, true);
			Direction = 1;
			IsWalking = true;
		} else {
			if (IsWalking) stopSound ();
			Direction = 0;
			IsWalking = false;
		}
	}

	void FixedUpdate () {
		character.Walk (Direction);
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if(collision.gameObject.tag == "ground" || 
			collision.gameObject.tag == "monsters" || 
			collision.gameObject.tag == "objects") {
			IsGrounded = true;
			jumpsCount = 0;
		} 

		foreach(ContactPoint2D contact in collision.contacts)
		{
			// If we collied with a monster from left or right
			if (collision.collider.tag == "monsters" && (contact.normal.x > 0.8 || contact.normal.x < -0.8)) {
				// Return if the collisioned monster is on a platform and we aren't on this platform
				if (collision.gameObject.layer == 8 && !isOnAPlatform) {
					return;
				}

				die();
				// Invoke ("gameOver", 0.3f);
			}
		}
			
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, 1, PlatformsLayer.value);
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
		if (!IsPlaying) return;
		playSound (DeathSound);
		character.SetAnimatorValue ("isDead", true);
		IsPlaying = false;
		Invoke ("showGameOver", 1.0f);
	}

	void showGameOver () {
		EndGameUI.Show (false);
	}

	private void shouldIgnoreCollisionsWithPlatformMonsters (bool ignore) {
		Physics2D.IgnoreLayerCollision (10, 8, ignore);
	}

	private void playSound (AudioClip audioClip, bool loop = false) {
		audio.clip = audioClip;
		audio.loop = loop;
		audio.Play ();
	}

	private void stopSound () {
		audio.Stop ();
	}
}
