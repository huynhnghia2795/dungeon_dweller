using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	private float checkRate;
	private float nextCheck;
	private Transform myTransform;
	private Transform playerTransform;
	private Vector3 spawnPosition;

	public GameObject enemy;
	public int spawnNumber;
	public float proximity;

	void Start () {
		SetInitialReferences ();
	}

	void Update () {
		checkDistance ();
	}

	void SetInitialReferences() {
		myTransform = transform;
		playerTransform = GameManager_References._player.transform;
		checkRate = Random.Range (0.8f, 1.2f);
	}

	void checkDistance() {
		if (Time.time > nextCheck) {
			nextCheck = Time.time + checkRate;

			if (Vector3.Distance (myTransform.position, playerTransform.position) < proximity) {
				spawnObject ();
				this.enabled = false;
			}
		}
	}

	void spawnObject() {
		for (int i = 0; i < spawnNumber; i++) {
			spawnPosition = myTransform.position + Random.insideUnitSphere * 5;
			Instantiate (enemy, spawnPosition, myTransform.rotation);
		}
	}
}
