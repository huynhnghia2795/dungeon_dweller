using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Restart : MonoBehaviour {

	private GameManager_Master gameManagerMaster;

	void OnEnable() {
		SetInitialReferences ();
		gameManagerMaster.eventRestartLevel += restartLevel;
	}

	void OnDisable() {
		gameManagerMaster.eventRestartLevel -= restartLevel;
	}

	void SetInitialReferences() {
		gameManagerMaster = GetComponent<GameManager_Master> ();
	}

	void restartLevel() {
		int scene = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (scene, LoadSceneMode.Single);
		Time.timeScale = 1;
	}
}
