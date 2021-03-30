using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	private Enemy_Master enemyMaster;
	private Transform attackTarget;
	private Transform myTransform;
	private float nextAttack;

	public float attackRate = 1f;
	public float attackRange = 5f;
	public float attackDamage = 0.5f;

	void OnEnable() {
		SetInitialReferences ();
		enemyMaster.eventEnemyDie += disableThis;
		enemyMaster.eventEnemySetNavTarget += setAttackTarget;
	}

	void OnDisable() {
		enemyMaster.eventEnemyDie -= disableThis;
		enemyMaster.eventEnemySetNavTarget -= setAttackTarget;
	}

	void Update () {
		attackingTarget ();
	}

	void SetInitialReferences() {
		enemyMaster = GetComponent<Enemy_Master> ();
		myTransform = transform;
	}

	void setAttackTarget(Transform targetTransform) {
		attackTarget = targetTransform;
	}

	void attackingTarget() {
		if (attackTarget != null) {
			if (Time.time > nextAttack) {
				nextAttack = Time.time + attackRate;

				if (Vector3.Distance (myTransform.position, attackTarget.position) <= attackRange) {
					Vector3 lookAtVector = new Vector3 (attackTarget.position.x, myTransform.position.y, attackTarget.position.z);
					myTransform.LookAt (lookAtVector);
					enemyMaster.callEventEnemyAttack ();
					enemyMaster.isOnRoute = false;
				}
			}
		}
	}

	public void onEnemyAttack() {
		if (attackTarget != null) {
			if (Vector3.Distance (myTransform.position, attackTarget.position) <= attackRange &&
			    attackTarget.GetComponent<Player_Master> () != null) {
				Vector3 toOther = attackTarget.position - myTransform.position;

				if (Vector3.Dot (toOther, myTransform.forward) > 0.5f) {
					attackTarget.GetComponent<Player_Master> ().callEventPlayerHealthDeduction (attackDamage);
				}
			}
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
