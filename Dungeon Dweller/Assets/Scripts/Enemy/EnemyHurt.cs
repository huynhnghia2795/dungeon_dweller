using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour {

	private Enemy_Master enemyMaster;

	public float damageMultiplier = 1f;
	public bool shouldRemoveCollider;

	void OnEnable() {
		SetInitialReferences ();
		enemyMaster.eventEnemyDie += removeThis;
	}

	void OnDisable() {
		enemyMaster.eventEnemyDie -= removeThis;
	}

	void SetInitialReferences() {
		enemyMaster = transform.root.GetComponent<Enemy_Master> ();
	}

	public void processDamage(float damage) {
		float damageToApply = damage * damageMultiplier;
		enemyMaster.callEventEnemyDeductHealth (damageToApply);
	}

	void removeThis() {
		if (shouldRemoveCollider) {
			if (GetComponent<Collider> () != null) {
				Destroy (GetComponent<Collider> ());
			}

			if (GetComponent<Rigidbody> () != null) {
				Destroy (GetComponent<Rigidbody> ());
			}
		}

		Destroy (this);
	}
}
