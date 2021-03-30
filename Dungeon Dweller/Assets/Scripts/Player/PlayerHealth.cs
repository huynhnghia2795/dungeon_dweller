using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	private GameManager_Master gameManagerMaster;
	private Player_Master playerMaster;
	private bool isHurt;
	private float playerHealth;

	public float maximumHealthValue = 100f;
	public Slider healthSlider;
	public Image healthBar;
	public Color fullHealth = Color.green;
	public Color depletedHealth = Color.red;
	public Image myHurtFlash;
	public Color hurtFlashColor = new Color (1f, 0f, 0f, 0.1f);
	public float hurtFlashSpeed = 5f;

	void OnEnable() {
		SetInitialReferences ();
		setUI ();
		playerMaster.eventPlayerHealthDeduction += deductPlayerHealth;
		playerMaster.eventPlayerHealthIncrease += increasePlayerHealth;
	}

	void OnDisable() {
		playerMaster.eventPlayerHealthDeduction -= deductPlayerHealth;
		playerMaster.eventPlayerHealthIncrease -= increasePlayerHealth;
	}

	void Update() {
		if (isHurt) {
			myHurtFlash.color = hurtFlashColor;
		} else {
			myHurtFlash.color = Color.Lerp (myHurtFlash.color, Color.clear, hurtFlashSpeed * Time.deltaTime);
		}

		isHurt = false;
	}

	void SetInitialReferences() {
		gameManagerMaster = GameObject.Find ("GameManager").GetComponent<GameManager_Master> ();
		playerMaster = GetComponent<Player_Master> ();
		playerHealth = maximumHealthValue;
	}

	void deductPlayerHealth(float healthChange) {
		playerHealth -= healthChange;

		if (playerHealth <= 0) {
			playerHealth = 0;
			gameManagerMaster.callEventGameOver ();
		}

		isHurt = true;
		setUI ();
	}

	void increasePlayerHealth(float healthChange) {
		playerHealth += healthChange;

		if (playerHealth > maximumHealthValue) {
			playerHealth = maximumHealthValue;
		}

		setUI ();
	}

	void setUI() {
		if (healthSlider != null) {
			healthSlider.value = playerHealth;
			healthBar.color = Color.Lerp (depletedHealth, fullHealth, (playerHealth / maximumHealthValue));
		}
	}
}
