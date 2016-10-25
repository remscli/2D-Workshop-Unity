using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public float factor = 0.2f;
	Transform cameraTransform;
	Vector3 originalPos;
	Vector3 originalCameraPos;

	void Start () {
		originalPos = transform.position;
		cameraTransform = Camera.main.transform;
		originalCameraPos = cameraTransform.position;
	}

	void LateUpdate () {
		float newPosX = (cameraTransform.position.x - originalCameraPos.x) * (1 - factor);
		transform.position = new Vector3( originalPos.x + newPosX, originalPos.y );
	}
}
