using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	private Enemy_Master enemyMaster;
	private GameManager_Master gameManagerMaster;
	private float maximumEnemyHealth;
	private float lowHealth;

	public float enemyHealth = 100f;
	public int point = 1;

	void OnEnable() {
		SetInitialReferences ();
		enemyMaster.eventEnemyDeductHealth += deductHealth;
		enemyMaster.eventEnemyIncreaseHealth += increaseHealth;
	}

	void OnDisable() {
		enemyMaster.eventEnemyDeductHealth -= deductHealth;
		enemyMaster.eventEnemyIncreaseHealth -= increaseHealth;
	}

	void SetInitialReferences() {
		enemyMaster = GetComponent<Enemy_Master> ();
		gameManagerMaster = GameObject.Find ("GameManager").GetComponent<GameManager_Master> ();
		maximumEnemyHealth = enemyHealth;
		lowHealth = maximumEnemyHealth * 0.3f;
	}

	void deductHealth(float healthChange) {
		enemyHealth -= healthChange;

		if (enemyHealth <= 0) {
			enemyHealth = 0;
			GameManager_Score.score += point;
			enemyMaster.callEventEnemyDie ();

			if (gameObject.name == "Troll") {
				StartCoroutine (WaitToDie ());
			}

			Destroy (gameObject, Random.Range (3, 5));
		}

		checkHealthFraction ();
	}

	void checkHealthFraction() {
		if (enemyHealth <= lowHealth && enemyHealth > 0) {
			enemyMaster.callEventEnemyHealthLow ();
		} else if (enemyHealth > lowHealth) {
			enemyMaster.callEventEnemyHealthRecovered ();
		}
	}

	void increaseHealth(float healthChange) {
		enemyHealth += healthChange;

		if (enemyHealth > maximumEnemyHealth) {
			enemyHealth = maximumEnemyHealth;
		}

		checkHealthFraction ();
	}

	IEnumerator WaitToDie() {
		yield return new WaitForSeconds (3);
		requestGameWin ();
	}

	void requestGameWin() {
		gameManagerMaster.callEventGameWin ();
	}
}
