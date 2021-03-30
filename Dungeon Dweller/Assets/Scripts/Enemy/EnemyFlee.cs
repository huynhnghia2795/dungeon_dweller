using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlee : MonoBehaviour {

	private Enemy_Master enemyMaster;
	private NavMeshAgent myNavMeshAgent;
	private NavMeshHit navHit;
	private Transform myTransform;
	private Vector3 fleePosition;
	private Vector3 directionToPlayer;
	private float checkRate;
	private float nextCheck;

	public bool isFleeing;
	public float fleeRange = 50f;
	public Transform fleeTarget;

	void OnEnable() {
		SetInitialReferences ();
		enemyMaster.eventEnemyDie += disableThis;
		enemyMaster.eventEnemySetNavTarget += setFleeTarget;
		enemyMaster.eventEnemyHealthLow += decideToFlee;
		enemyMaster.eventEnemyHealthRecovered += decideToManUp;
	}

	void OnDisable() {
		enemyMaster.eventEnemyDie -= disableThis;
		enemyMaster.eventEnemySetNavTarget -= setFleeTarget;
		enemyMaster.eventEnemyHealthLow -= decideToFlee;
		enemyMaster.eventEnemyHealthRecovered -= decideToManUp;
	}

	void Update() {
		if (Time.time > nextCheck) {
			nextCheck = Time.time;
			shouldFleeOrNot ();
		}
	}

	void SetInitialReferences() {
		enemyMaster = GetComponent<Enemy_Master> ();
		myTransform = transform;

		if (GetComponent<NavMeshAgent> () != null) {
			myNavMeshAgent = GetComponent<NavMeshAgent> ();
		}

		checkRate = Random.Range (0.3f, 0.4f);
	}

	void setFleeTarget(Transform target) {
		fleeTarget = target;
	}

	void decideToFlee() {
		isFleeing = true;

		if (GetComponent<EnemyPursue> () != null) {
			GetComponent<EnemyPursue> ().enabled = false;
		}
	}

	void decideToManUp() {
		isFleeing = false;

		if (GetComponent<EnemyPursue> () != null) {
			GetComponent<EnemyPursue> ().enabled = true;
		}
	}

	void shouldFleeOrNot() {
		if (isFleeing) {
			if (fleeTarget != null && !enemyMaster.isOnRoute && !enemyMaster.isNavPause) {
				if (fleeingTarget (out fleePosition) && Vector3.Distance (myTransform.position, fleeTarget.position) < fleeRange) {
					myNavMeshAgent.SetDestination (fleePosition);
					enemyMaster.callEventEnemyRunning ();
					enemyMaster.isOnRoute = true;
				}
			}
		}
	}

	bool fleeingTarget(out Vector3 result) {
		directionToPlayer = myTransform.position - fleeTarget.position;
		Vector3 checkPosition = myTransform.position + directionToPlayer;

		if (NavMesh.SamplePosition (checkPosition, out navHit, 1f, NavMesh.AllAreas)) {
			result = navHit.position;
			return true;
		} else {
			result = myTransform.position;
			return false;
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
