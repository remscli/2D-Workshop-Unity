using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	Vector3 offset;
	Vector3 velocity = Vector3.zero;
	float maxCameraPosX;
	Vector3 originalCameraPosition;
	Vector3 newPosition;
	Camera camera;
	float originalCameraSize;
	string focus = "ground";

	public GameObject Target;
	public float smoothTime = 0.3F;

	// Use this for initialization
	void Start () {
		offset = transform.position - Target.transform.position;
		GameObject terrain = GameObject.Find ("terrain-collider");
		float sceneSize = terrain.GetComponent<PolygonCollider2D> ().bounds.size.x;

		camera = GetComponent<Camera> ();
		float cameraHeight = 2f * camera.orthographicSize;
		float cameraWidth = cameraHeight * camera.aspect;
		originalCameraSize = camera.orthographicSize;
		originalCameraPosition = camera.transform.position;

		maxCameraPosX = sceneSize - cameraWidth;
		newPosition = camera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPos = transform.position;
		targetPos.x = Target.transform.position.x + offset.x;
		targetPos.y = Target.transform.position.y + offset.y;

		if (targetPos.y < 2) focus = "ground"; 

		if (targetPos.x < 0) {
			newPosition.x = 0;
		} else if (targetPos.x > maxCameraPosX) {
			newPosition.x = maxCameraPosX;
		} else {
			newPosition.x = targetPos.x;
		}

		newPosition.y = targetPos.y;

		translate (newPosition);
		scale (newPosition);
	}

	private void translate (Vector3 newPosition) {
		Vector3 pos = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
		float newPosY = focus == "sky" ? newPosition.y : newPosition.y * 0.3f;
		transform.position = Vector3.SmoothDamp(pos, new Vector3(pos.x, newPosY), ref velocity, 0.2f);
	}

	private void scale (Vector3 position) {
		position.y = position.y < 10.0f ? position.y : 10;
		camera.orthographicSize = originalCameraSize + position.y * 0.3f;
	}

	public void FocusOn (string newFocus) {
		focus = newFocus;
	}
}
