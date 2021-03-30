using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPause : MonoBehaviour {

	private Enemy_Master enemyMaster;
	private NavMeshAgent myNavMeshAgent;
	private float pauseTime = 1f;

	void OnEnable() {
		SetInitialReferences ();
		enemyMaster.eventEnemyDie += disableThis;
		enemyMaster.eventEnemyDeductHealth += pauseNavMeshAgent;
	}

	void OnDisable() {
		enemyMaster.eventEnemyDie -= disableThis;
		enemyMaster.eventEnemyDeductHealth -= pauseNavMeshAgent;
	}

	void SetInitialReferences() {
		enemyMaster = GetComponent<Enemy_Master> ();

		if (GetComponent<NavMeshAgent> () != null) {
			myNavMeshAgent = GetComponent<NavMeshAgent> ();
		}
	}

	void pauseNavMeshAgent(float dummy) {
		if (myNavMeshAgent != null) {
			if (myNavMeshAgent.enabled) {
				myNavMeshAgent.ResetPath ();
				enemyMaster.isNavPause = true;
				StartCoroutine (restartNavMeshAgent ());
			}
		}
	}

	IEnumerator restartNavMeshAgent() {
		yield return new WaitForSeconds (pauseTime);
		enemyMaster.isNavPause = false;
	}

	void disableThis() {
		StopAllCoroutines ();
	}
}
