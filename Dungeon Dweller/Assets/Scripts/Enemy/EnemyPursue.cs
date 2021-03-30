using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPursue : MonoBehaviour {

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
			chasingTarget ();
		}
	}

	void SetInitialReferences() {
		enemyMaster = GetComponent<Enemy_Master> ();

		if (GetComponent<NavMeshAgent> () != null) {
			myNavMeshAgent = GetComponent<NavMeshAgent> ();
		}

		checkRate = Random.Range (0.1f, 0.2f);
	}

	void chasingTarget() {
		if (enemyMaster.myTarget != null && myNavMeshAgent != null && !enemyMaster.isNavPause) {
			myNavMeshAgent.SetDestination (enemyMaster.myTarget.position);

			if (myNavMeshAgent.remainingDistance > myNavMeshAgent.stoppingDistance) {
				enemyMaster.callEventEnemyRunning ();
				enemyMaster.isOnRoute = true;
			}
		}
	}

	void disableThis() {
		if (myNavMeshAgent != null) {
			myNavMeshAgent.enabled = false;
		}

		this.enabled = false;
	}
}
