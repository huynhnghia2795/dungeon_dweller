using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Master : MonoBehaviour {

	public Transform myTarget;
	public bool isOnRoute;
	public bool isNavPause;

	public delegate void generalEventHandler();
	public event generalEventHandler eventEnemyDie;
	public event generalEventHandler eventEnemyWalking;
	public event generalEventHandler eventEnemyRunning;
	public event generalEventHandler eventEnemyReachedNavTarget;
	public event generalEventHandler eventEnemyAttack;
	public event generalEventHandler eventEnemyLostTarget;
	public event generalEventHandler eventEnemyHealthLow;
	public event generalEventHandler eventEnemyHealthRecovered;

	public delegate void healthEventHandler(float health);
	public event healthEventHandler eventEnemyDeductHealth;
	public event healthEventHandler eventEnemyIncreaseHealth;

	public delegate void navTargetEventHandler(Transform targetTransform);
	public event navTargetEventHandler eventEnemySetNavTarget;

	public void callEventEnemyDeductHealth(float health) {
		if (eventEnemyDeductHealth != null) {
			eventEnemyDeductHealth (health);
		}
	}

	public void callEventEnemyIncreaseHealth(float health) {
		if (eventEnemyIncreaseHealth != null) {
			eventEnemyIncreaseHealth (health);
		}
	}

	public void callEventEnemySetNavTarget(Transform targetTransform) {
		if (eventEnemySetNavTarget != null) {
			eventEnemySetNavTarget (targetTransform);
		}

		myTarget = targetTransform;
	}

	public void callEventEnemyDie() {
		if (eventEnemyDie != null) {
			eventEnemyDie ();
		}
	}

	public void callEventEnemyWalking() {
		if (eventEnemyWalking != null) {
			eventEnemyWalking ();
		}
	}

	public void callEventEnemyRunning() {
		if (eventEnemyRunning != null) {
			eventEnemyRunning ();
		}
	}

	public void callEventEnemyReachedNavTarget() {
		if (eventEnemyReachedNavTarget != null) {
			eventEnemyReachedNavTarget ();
		}
	}

	public void callEventEnemyAttack() {
		if (eventEnemyAttack != null) {
			eventEnemyAttack ();
		}
	}

	public void callEventEnemyLostTarget() {
		if (eventEnemyLostTarget != null) {
			eventEnemyLostTarget ();
		}

		myTarget = null;
	}

	public void callEventEnemyHealthLow() {
		if (eventEnemyHealthLow != null) {
			eventEnemyHealthLow ();
		}
	}

	public void callEventEnemyHealthRecovered() {
		if (eventEnemyHealthRecovered != null) {
			eventEnemyHealthRecovered ();
		}
	}
}
