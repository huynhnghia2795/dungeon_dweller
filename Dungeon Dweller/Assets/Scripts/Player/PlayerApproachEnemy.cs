using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerApproachEnemy : MonoBehaviour {

	private Player_Master playerMaster;

	void OnEnable() {
		SetInitialReferences ();
		playerMaster.eventPlayerDie += disableThis;
	}

	void OnDisable() {
		playerMaster.eventPlayerDie -= disableThis;
	}

	void SetInitialReferences() {
		playerMaster = transform.root.GetComponent<Player_Master> ();
	}

	void OnTriggerEnter(Collider enemyCollider) {
		if (enemyCollider.gameObject.name == "HitBox") {
			playerMaster.isFacingEnemy = true;
		}
	}

	void OnTriggerExit(Collider enemyCollider) {
		if (enemyCollider.gameObject.name == "HitBox") {
			playerMaster.isFacingEnemy = false;
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
