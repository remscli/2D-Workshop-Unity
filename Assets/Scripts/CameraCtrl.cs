using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {

	public GameObject Hero;

	Vector3 offset;
	float maxCameraPosX;

	// Use this for initialization
	void Start () {
		offset = transform.position - Hero.transform.position;
		GameObject terrain = GameObject.Find ("terrain-collider");
		float sceneSize = terrain.GetComponent<PolygonCollider2D> ().bounds.size.x;

		Camera camera = GetComponent<Camera> ();
		float cameraHeight = 2f * camera.orthographicSize;
		float cameraWidth = cameraHeight * camera.aspect;

		maxCameraPosX = sceneSize - cameraWidth;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmpPosition = transform.position;
		tmpPosition.x = Hero.transform.position.x + offset.x;

		if (tmpPosition.x < 0 || tmpPosition.x > maxCameraPosX) return;
		transform.position = tmpPosition;
	}
}
