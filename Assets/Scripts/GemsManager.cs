using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GemsManager : MonoBehaviour {

	Text valueText;
	int value = 0;

	// Use this for initialization
	void Start () {
		valueText = transform.Find ("value").gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGemCollected () {
		value++;
		valueText.text = value.ToString();
	}
}
