﻿using UnityEngine;
using System.Collections;

public class MessageTrigger : MonoBehaviour {

	public Message message;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "hero") {
			message.ShowMessage ();		
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.gameObject.tag == "hero") {
			message.HideMessage ();
		}
	}
}
