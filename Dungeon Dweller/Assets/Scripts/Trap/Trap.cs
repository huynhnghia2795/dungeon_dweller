using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	private Player_Master playerMaster;

	public float playerVictimDamage = 1f;

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
			playerMaster.callEventPlayerHealthDeduction (playerVictimDamage);
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
