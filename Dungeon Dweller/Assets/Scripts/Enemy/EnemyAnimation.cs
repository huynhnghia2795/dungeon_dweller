using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour {

	private Enemy_Master enemyMaster;
	private Animator myAnimator;
	private NavMeshAgent myNavMeshAgent;
	private EnemyAttack enemyAttack;

	void OnEnable() {
		SetInitialReferences ();
		enemyMaster.eventEnemyDie += setAnimationToDie;
		enemyMaster.eventEnemyWalking += setAnimationToWander;
		enemyMaster.eventEnemyRunning += setAnimationToPursue;
		enemyMaster.eventEnemyReachedNavTarget += setAnimationToIdle;
		enemyMaster.eventEnemyAttack += setAnimationToAttack;
		enemyMaster.eventEnemyDeductHealth += setAnimationToHurt;
	}

	void OnDisable() {
		enemyMaster.eventEnemyDie -= setAnimationToDie;
		enemyMaster.eventEnemyWalking -= setAnimationToWander;
		enemyMaster.eventEnemyRunning -= setAnimationToPursue;
		enemyMaster.eventEnemyReachedNavTarget -= setAnimationToIdle;
		enemyMaster.eventEnemyAttack -= setAnimationToAttack;
		enemyMaster.eventEnemyDeductHealth -= setAnimationToHurt;
	}

	void SetInitialReferences() {
		enemyMaster = GetComponent<Enemy_Master> ();
		enemyAttack = GetComponent<EnemyAttack> ();

		if (GetComponent<Animator> () != null) {
			myAnimator = GetComponent<Animator> ();
		}

		if (GetComponent<NavMeshAgent> () != null) {
			myNavMeshAgent = GetComponent<NavMeshAgent> ();
		}
	}

	void setAnimationToWander() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.SetBool ("isWalking", true);
				myAnimator.SetBool ("isPursuing", false);
			}
		}
	}

	void setAnimationToIdle() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.SetBool ("isWalking", false);
				myAnimator.SetBool ("isPursuing", false);
			}
		}
	}

	void setAnimationToPursue() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.SetBool ("isPursuing", true);
				myAnimator.SetBool ("isWalking", false);
			}
		}
	}

	void setAnimationToAttack() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.SetTrigger ("isAttacking");
				myAnimator.SetBool ("isWalking", false);
				myAnimator.SetBool ("isPursuing", false);
			}
		}
	}

	void setAnimationToHurt(float dummy) {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.SetTrigger ("isHurt");
			}
		}
	}

	void setAnimationToDie() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				enemyAttack.enabled = false;
				myAnimator.SetBool ("isWalking", false);
				myAnimator.SetBool ("isPursuing", false);
				myAnimator.SetTrigger ("isDead");

				if (myNavMeshAgent != null) {
					if (myNavMeshAgent.enabled) {
						myNavMeshAgent.enabled = false;
					}
				}

				Destroy (gameObject, Random.Range (10, 20));
			}
		}
	}
}
