using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	private float originalY;

	public float floatStrength = 0.5f;

	void Start() {
		this.originalY = this.transform.position.y;
	}

	void Update () {
		transform.Rotate (new Vector3 (30f, 30f, 30f) * Time.deltaTime);
		transform.position = new Vector3 (transform.position.x,
			originalY + ((float)Mathf.Sin (Time.time) * floatStrength), transform.position.z);
	}
}
