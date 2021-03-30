using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	private Player_Master playerMaster;
	private Animator myAnimator;

	void OnEnable() {
		SetInitialReferences ();
		playerMaster.eventPlayerDie += setAnimationToDie;
		playerMaster.eventPlayerStanding += setAnimationToIdle;
		playerMaster.eventPlayerWalking += setAnimationToWalk;
		playerMaster.eventPlayerRunning += setAnimationToRun;
		playerMaster.eventPlayerAttack += setAnimationToAttack;
	}

	void OnDisable() {
		playerMaster.eventPlayerDie -= setAnimationToDie;
		playerMaster.eventPlayerStanding -= setAnimationToIdle;
		playerMaster.eventPlayerWalking -= setAnimationToWalk;
		playerMaster.eventPlayerRunning -= setAnimationToRun;
		playerMaster.eventPlayerAttack -= setAnimationToAttack;
	}

	void SetInitialReferences() {
		playerMaster = GetComponent<Player_Master> ();

		if (GetComponent<Animator> () != null) {
			myAnimator = GetComponent<Animator> ();
		}
	}

	void setAnimationToIdle() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.SetBool ("isPlayerWalking", false);
				myAnimator.SetBool ("isPlayerRunning", false);
			}
		}
	}

	void setAnimationToWalk() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.SetBool ("isPlayerWalking", true);
				myAnimator.SetBool ("isPlayerRunning", false);
			}
		}
	}

	void setAnimationToRun() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.SetBool ("isPlayerRunning", true);
				myAnimator.SetBool ("isPlayerWalking", false);
			}
		}
	}

	void setAnimationToAttack() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.SetTrigger ("isPlayerAttacking");
				myAnimator.SetBool ("isPlayerWalking", false);
				myAnimator.SetBool ("isPlayerRunning", false);
			}
		}
	}

	void setAnimationToDie() {
		if (myAnimator != null) {
			if (myAnimator.enabled) {
				myAnimator.enabled = false;
			}
		}
	}
}
