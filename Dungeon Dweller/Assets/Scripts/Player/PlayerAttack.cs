using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private Player_Master playerMaster;
	private GameObject attackZone;
	private EnemyHealth enemyHealth;
	private EnemyHurt enemyHurt;

	public float attackDamage = 25f;
	public float attackRange = 0.8f;
	public LayerMask enemyMask;

	void OnEnable() {
		SetInitialReferences ();
		playerMaster.eventPlayerDie += disableThis;
	}

	void OnDisable() {
		playerMaster.eventPlayerDie -= disableThis;
	}

	void SetInitialReferences() {
		playerMaster = GetComponent<Player_Master> ();
		attackZone = GameObject.FindWithTag ("AttackZone");
	}

	public void attackingEnemy() {
		playerMaster.callEventPlayerAttack ();
	}

	public void onPlayerAttack() {
		if (playerMaster.isFacingEnemy) {
			Collider[] colliders = Physics.OverlapSphere (attackZone.transform.position, attackRange, enemyMask);

			for (int i = 0; i < colliders.Length; i++) {
				Rigidbody targetRigidbody = colliders [i].GetComponent<Rigidbody> ();

				if (!targetRigidbody)
					continue;
				
				enemyHealth = targetRigidbody.transform.root.GetComponent<EnemyHealth> ();
				enemyHurt = targetRigidbody.GetComponent<EnemyHurt> ();

				if (!enemyHealth)
					continue;
				
				enemyHurt.processDamage(attackDamage);
			}
		}
	}

	void disableThis() {
		this.enabled = false;
	}
}
