using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	private Player_Master playerMaster;

	public float playerheal = 50f;

	void OnEnable() {
		SetInitialReferences ();
		playerMaster.eventPlayerDie += disableThis;
	}

	void OnDisable() {
		playerMaster.eventPlayerDie -= disableThis;
	}

	void SetInitialReferences() {
		playerMaster = GameObject.Find ("PlayerHitBox").GetComponent<Player_Master> ();
	}

	void OnTriggerEnter(Collider victimCollider) {
		if (victimCollider.gameObject.name == "PlayerHitBox") {
			playerMaster.callEventPlayerHealthIncrease (playerheal);
			Destroy (gameObject);
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
