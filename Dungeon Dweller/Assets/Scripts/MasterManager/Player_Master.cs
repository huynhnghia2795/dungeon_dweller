using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Master : MonoBehaviour {

	public bool isRunning;
	public bool isWalking;
	public bool isBeingControlled;
	public bool isFacingEnemy;

	public delegate void generalEventHandler();
	public event generalEventHandler eventPlayerDie;
	public event generalEventHandler eventPlayerStanding;
	public event generalEventHandler eventPlayerWalking;
	public event generalEventHandler eventPlayerRunning;
	public event generalEventHandler eventPlayerAttack;
	public event generalEventHandler eventInventoryChanged;

	public delegate void playerHealthEventHandler(float healthChange);
	public event playerHealthEventHandler eventPlayerHealthDeduction;
	public event playerHealthEventHandler eventPlayerHealthIncrease;

	public void callEventPlayerDie() {
		if (eventPlayerDie != null) {
			eventPlayerDie ();
		}
	}

	public void callEventPlayerStanding() {
		if (eventPlayerStanding != null) {
			eventPlayerStanding ();
		}
	}

	public void callEventPlayerWalking() {
		if (eventPlayerWalking != null) {
			eventPlayerWalking ();
		}
	}

	public void callEventPlayerRunning() {
		if (eventPlayerRunning != null) {
			eventPlayerRunning ();
		}
	}

	public void callEventPlayerAttack() {
		if (eventPlayerAttack != null) {
			eventPlayerAttack ();
		}
	}

	public void callEventInventoryChanged() {
		if (eventInventoryChanged != null) {
			eventInventoryChanged ();
		}
	}

	public void callEventPlayerHealthDeduction(float damage) {
		if (eventPlayerHealthDeduction != null) {
			eventPlayerHealthDeduction (damage);
		}
	}

	public void callEventPlayerHealthIncrease(float restore) {
		if (eventPlayerHealthIncrease != null) {
			eventPlayerHealthIncrease (restore);
		}
	}
}
