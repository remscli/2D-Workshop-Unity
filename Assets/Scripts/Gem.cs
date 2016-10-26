using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {

	GemsManager gemsManager;
	AudioSource audio;
	Renderer renderer;
	BoxCollider2D collider;

	// Use this for initialization
	void Start () {
		gemsManager = GameObject.Find ("gems-manager").GetComponent<GemsManager> ();
		renderer = GetComponent<Renderer> ();
		audio = GetComponent<AudioSource>();
		collider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "hero") {
			collected ();
		}
	}

	void collected () {
		collider.enabled = false;
		gemsManager.NewGemCollected ();
		audio.Play();
		StartCoroutine (FadeTo(0.0f, 0.3f));
		Invoke ("destroy", 1.0f);
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

	void destroy () {
		Destroy (gameObject);
	}
}
