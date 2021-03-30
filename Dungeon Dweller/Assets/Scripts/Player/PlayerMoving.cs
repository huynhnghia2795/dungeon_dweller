using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour {

	private Rigidbody myRigidbody;
	private Player_Master playerMaster;

	public LeftStickController myJoyStick;
	public float speed = 10f;

	void OnEnable() {
		SetInitialReferences ();
		playerMaster.eventPlayerDie += disableThis;
	}

	void OnDisable() {
		playerMaster.eventPlayerDie -= disableThis;
	}

	void Update() {
		Vector3 dir = Vector3.zero;
		dir.x = Input.GetAxis ("Horizontal");
		dir.z = Input.GetAxis ("Vertical");

		if (dir.magnitude > 1) {
			dir.Normalize ();
		}

		if (myJoyStick.inputDirection != Vector3.zero) {
			dir = myJoyStick.inputDirection;
		}

		movingPlayer (dir.x, dir.z);
	}

	void SetInitialReferences() {
		playerMaster = GetComponent<Player_Master> ();
		myRigidbody = GetComponent<Rigidbody> ();
	}

	private void movingPlayer(float x, float z) {
		Vector3 movement = new Vector3 (x, 0f, z);
		myRigidbody.transform.Translate (movement * speed * Time.deltaTime, Space.World);
		myRigidbody.velocity = movement * speed;

		if (x != 0 || z != 0) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (movement), 0.15F);
			if (x > speed * Time.deltaTime * 2 || z > speed * Time.deltaTime * 2 ||
				x < -(speed * Time.deltaTime) * 2 || z < -(speed * Time.deltaTime) * 2) {
				playerMaster.callEventPlayerRunning ();
				playerMaster.isRunning = true;
				playerMaster.isWalking = false;
				playerMaster.isBeingControlled = true;
			} else {
				playerMaster.callEventPlayerWalking ();
				playerMaster.isWalking = true;
				playerMaster.isRunning = false;
				playerMaster.isBeingControlled = true;
			}
		} else {
			playerMaster.callEventPlayerStanding ();
			playerMaster.isWalking = false;
			playerMaster.isRunning = false;
			playerMaster.isBeingControlled = false;
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
