using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWander : MonoBehaviour {

	private Enemy_Master enemyMaster;
	private NavMeshAgent myNavMeshAgent;
	private float checkRate;
	private float nextCheck;
	private float wanderRange = 10f;
	private Transform myTransform;
	private NavMeshHit navHit;
	private Vector3 wanderTarget;

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
			toWanderOrNot ();
		}
	}

	void SetInitialReferences() {
		enemyMaster = GetComponent<Enemy_Master> ();

		if (GetComponent<NavMeshAgent> () != null) {
			myNavMeshAgent = GetComponent<NavMeshAgent> ();
		}

		checkRate = Random.Range (0.3f, 0.4f);
		myTransform = transform;
	}

	void toWanderOrNot() {
		if (enemyMaster.myTarget == null && !enemyMaster.isOnRoute && !enemyMaster.isNavPause) {
			if (randomWanderTarget (myTransform.position, wanderRange, out wanderTarget)) {
				myNavMeshAgent.SetDestination (wanderTarget);
				enemyMaster.isOnRoute = true;
				enemyMaster.callEventEnemyWalking ();
			}
		}
	}

	bool randomWanderTarget(Vector3 centre, float range, out Vector3 result) {
		Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;

		if (NavMesh.SamplePosition (randomPoint, out navHit, 1.0f, NavMesh.AllAreas)) {
			result = navHit.position;
			return true;
		} else {
			result = centre;
			return false;
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
