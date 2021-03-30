using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Menu : MonoBehaviour {

	private GameManager_Master gameManagerMaster;

	public GameObject userInterface;
	public GameObject pauseMenu;
	public GameObject inventoryUI;
	public GameObject gameOverMenu;
	public GameObject gameWinMenu;

	void OnEnable() {
		SetInitialReferences ();
		gameManagerMaster.eventGameOver += toggleGameOverMenu;
		gameManagerMaster.eventGameWin += toggleWinMenu;
	}

	void OnDisable() {
		gameManagerMaster.eventGameOver -= toggleGameOverMenu;
		gameManagerMaster.eventGameWin -= toggleWinMenu;
	}

	void SetInitialReferences() {
		gameManagerMaster = GetComponent<GameManager_Master> ();
	}

	public void checkMenuRequest() {
		if (!gameManagerMaster.isGameOver && !gameManagerMaster.isInventoryUIOn) {
			togglePauseMenu ();
		}
	}

	void togglePauseMenu() {
		if (pauseMenu != null && userInterface != null) {
			userInterface.SetActive (!userInterface.activeSelf);
			pauseMenu.SetActive (!pauseMenu.activeSelf);
			gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
			gameManagerMaster.callEventMenuToggle ();
		}
	}

	void toggleGameOverMenu() {
		if (gameOverMenu != null && userInterface != null && inventoryUI != null) {
			userInterface.SetActive (!userInterface.activeSelf);
			inventoryUI.SetActive (false);
			gameManagerMaster.isInventoryUIOn = false;
			gameOverMenu.SetActive (!gameOverMenu.activeSelf);
			gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
			gameManagerMaster.callEventMenuToggle ();
		}
	}

	void toggleWinMenu() {
		if (gameWinMenu != null && userInterface != null && inventoryUI != null) {
			userInterface.SetActive (!userInterface.activeSelf);
			inventoryUI.SetActive (false);
			gameManagerMaster.isInventoryUIOn = false;
			gameWinMenu.SetActive (!gameOverMenu.activeSelf);
			gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
			gameManagerMaster.callEventMenuToggle ();
		}
	}
}
