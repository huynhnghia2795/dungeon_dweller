using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_BackToMainMenu : MonoBehaviour {

	private GameManager_Master gameManagerMaster;

	void OnEnable() {
		SetInitialReferences ();
		gameManagerMaster.eventBackToMainMenu += backToMainMenu;
	}

	void OnDisable() {
		gameManagerMaster.eventBackToMainMenu -= backToMainMenu;
	}

	void SetInitialReferences() {
		gameManagerMaster = GetComponent<GameManager_Master> ();
	}

	void backToMainMenu() {
		SceneManager.LoadScene (0, LoadSceneMode.Single);
		Time.timeScale = 1;
	}
}
