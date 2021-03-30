using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {

	private Enemy_Master enemyMaster;
	private Transform myTransform;
	private float checkRate;
	private float nextCheck;
	private RaycastHit hit;

	public Transform head;
	public LayerMask playerLayer;
	public LayerMask sightLayer;
	public float detectRadius = 50f;

	void OnEnable() {
		SetInitialReferences ();
		enemyMaster.eventEnemyDie += disableThis;
	}

	void OnDisable() {
		enemyMaster.eventEnemyDie -= disableThis;
	}

	void Update () {
		carryOutDetection ();
	}

	void SetInitialReferences() {
		enemyMaster = GetComponent<Enemy_Master> ();
		myTransform = transform;

		if (head == null) {
			head = myTransform;
		}

		checkRate = Random.Range (0.8f, 1.2f);
	}

	void carryOutDetection() {
		if (Time.time > nextCheck) {
			nextCheck = Time.time + checkRate;
			Collider[] colliders = Physics.OverlapSphere (myTransform.position, detectRadius, playerLayer);

			if (colliders.Length > 0) {
				foreach (Collider potTargetCollider in colliders) {
					if (potTargetCollider.CompareTag (GameManager_References._playerTag)) {
						if (canPotentialTargetBeSeen (potTargetCollider.transform)) {
							break;
						}
					}
				}
			} else {
				enemyMaster.callEventEnemyLostTarget ();
			}
		}
	}

	bool canPotentialTargetBeSeen(Transform potTarget) {
		if (Physics.Linecast (head.position, potTarget.position, out hit, sightLayer)) {
			if (hit.transform == potTarget) {
				enemyMaster.callEventEnemySetNavTarget (potTarget);
				return true;
			} else {
				enemyMaster.callEventEnemyLostTarget ();
				return false;
			}
		} else {
			enemyMaster.callEventEnemyLostTarget ();
			return false;
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
