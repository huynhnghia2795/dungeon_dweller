using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReachedDestination : MonoBehaviour {

	private Enemy_Master enemyMaster;
	private NavMeshAgent myNavMeshAgent;
	private float checkRate;
	private float nextCheck;

	void OnEnable() {
		SetInitialReferences ();
		enemyMaster.eventEnemyDie += disableThis;
	}

	void OnDisable() {
		enemyMaster.eventEnemyDie -= disableThis;
	}

	void Update () {
		if (Time.time > nextCheck) {
			nextCheck = Time.time + checkRate;
			checkIfDestinationReached ();
		}
	}

	void SetInitialReferences() {
		enemyMaster = GetComponent<Enemy_Master> ();

		if (GetComponent<NavMeshAgent> () != null) {
			myNavMeshAgent = GetComponent<NavMeshAgent> ();
		}

		checkRate = Random.Range (0.3f, 0.4f);
	}

	void checkIfDestinationReached() {
		if (enemyMaster.isOnRoute) {
			if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance) {
				enemyMaster.isOnRoute = false;
				enemyMaster.callEventEnemyReachedNavTarget ();
			}
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
