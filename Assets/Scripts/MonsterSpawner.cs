using UnityEngine;
using System;
using System.Collections;

public class MonsterSpawner : MonoBehaviour {

	public Transform Monster;
	bool isSwpanerStarted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (isSwpanerStarted || collider.gameObject.tag != "hero") return;

		StartCoroutine (SpawnMonsters(spawnMonster, 4, 4));

		isSwpanerStarted = true;
	}

	void spawnMonster () {
		Instantiate(Monster, transform.position, Quaternion.identity, transform.parent);
	}
		

	IEnumerator SpawnMonsters (Action method, int quantity, int intervalInSeconds) {
		for (float i = 0; i < quantity; i++) {
			method ();

			yield return new WaitForSeconds(intervalInSeconds);
		}
	}
}
