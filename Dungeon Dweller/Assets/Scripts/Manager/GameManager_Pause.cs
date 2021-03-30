using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Pause : MonoBehaviour {

	private GameManager_Master gameManagerMaster;
	private bool isPaused;
	private bool isSlowTime;

	void OnEnable() {
		SetInitialReferences ();
		gameManagerMaster.eventMenuToggle += togglePause;
		gameManagerMaster.eventInventoryUIToggle += toggleSlowTime;
	}

	void OnDisable() {
		gameManagerMaster.eventMenuToggle -= togglePause;
		gameManagerMaster.eventInventoryUIToggle -= toggleSlowTime;
	}

	void SetInitialReferences() {
		gameManagerMaster = GetComponent<GameManager_Master> ();
	}

	void togglePause() {
		if (isPaused) {
			Time.timeScale = 1;
			isPaused = false;
		} else {
			Time.timeScale = 0;
			isPaused = true;
		}
	}

	void toggleSlowTime() {
		if (isSlowTime) {
			Time.timeScale = 1;
			isSlowTime = false;
		} else {
			Time.timeScale = 0.25f;
			isSlowTime = true;
		}
	}

}
