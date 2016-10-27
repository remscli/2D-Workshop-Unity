using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	Vector3 offset;
	Vector3 velocity = Vector3.zero;
	float maxCameraPosX;
	Vector3 newPosition;
	Camera camera;
	float originalCameraSize;

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

		maxCameraPosX = sceneSize - cameraWidth;
		newPosition = camera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPos = transform.position;
		targetPos.x = Target.transform.position.x + offset.x;
		targetPos.y = Target.transform.position.y + offset.y;

		if (targetPos.x < 0) {
			newPosition.x = 0;
		} else if (targetPos.x > maxCameraPosX) {
			newPosition.x = maxCameraPosX;
		} else {
			newPosition.x = targetPos.x;
		}


		if (targetPos.y > 2) {
			newPosition.y = targetPos.y - 2;
		} else {
			newPosition.y = 0;
		}


		translate (newPosition);
		scale (newPosition);
	}

	private void translate (Vector3 newPosition) {
		Vector3 pos = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
		pos.y = newPosition.y * 0.3f;
		transform.position = pos;
	}

	private void scale (Vector3 position) {
		camera.orthographicSize = originalCameraSize + position.y * 0.3f;
	}
}
