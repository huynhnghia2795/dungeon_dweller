using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_InventoryUI : MonoBehaviour {

	private GameManager_Master gameManagerMaster;

	public GameObject userInterface;
	public GameObject inventoryUI;

	void Start () {
		SetInitialReferences ();
	}

	void SetInitialReferences() {
		gameManagerMaster = GetComponent<GameManager_Master> ();
	}

	public void checkInventoryUIRequest() {
		if (!gameManagerMaster.isMenuOn && !gameManagerMaster.isGameOver) {
			toggleInventoryUI ();
		}
	}

	public void toggleInventoryUI() {
		if (inventoryUI != null) {
			//userInterface.SetActive (!userInterface.activeSelf);
			inventoryUI.SetActive (!inventoryUI.activeSelf);
			gameManagerMaster.isInventoryUIOn = !gameManagerMaster.isInventoryUIOn;
			gameManagerMaster.callEventInventoryUIToggle ();
		}
	}
}
