using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	Vector3 offset;
	Vector3 velocity = Vector3.zero;
	float maxCameraPosX;

	public GameObject Target;
	public float smoothTime = 0.3F;

	// Use this for initialization
	void Start () {
		offset = transform.position - Target.transform.position;
		GameObject terrain = GameObject.Find ("terrain-collider");
		float sceneSize = terrain.GetComponent<PolygonCollider2D> ().bounds.size.x;

		Camera camera = GetComponent<Camera> ();
		float cameraHeight = 2f * camera.orthographicSize;
		float cameraWidth = cameraHeight * camera.aspect;

		maxCameraPosX = sceneSize - cameraWidth;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		newPosition.x = Target.transform.position.x + offset.x;

		if (newPosition.x < 0 || newPosition.x > maxCameraPosX) return;
		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
	}
}
