using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public float factor = 0.2f;
	public bool ShouldScaleOnJump = true;
	Transform cameraTransform;
	Vector3 originalPos;
	Vector3 originalCameraPos;
	Vector3 originalScale;

	void Start () {
		originalScale = transform.localScale;
		originalPos = transform.position;
		cameraTransform = Camera.main.transform;
		originalCameraPos = cameraTransform.position;
	}

	void LateUpdate () {
		bool isBehind = factor < 1;
		float newPosX = (cameraTransform.position.x - originalCameraPos.x) * (1 - factor);
		transform.position = new Vector3( originalPos.x + newPosX, originalPos.y );

		if (!ShouldScaleOnJump) return;
		transform.localScale = new Vector3(
			originalScale.x + cameraTransform.position.y * (isBehind ? 0.3f : -0.1f),
			originalScale.y + cameraTransform.position.y * (isBehind ? 0.3f : -0.1f)
		);
	}
}
