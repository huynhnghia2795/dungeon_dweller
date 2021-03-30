using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Master : MonoBehaviour {

	public delegate void gameManagerEventHandler();
	public event gameManagerEventHandler eventMenuToggle;
	public event gameManagerEventHandler eventInventoryUIToggle;
	public event gameManagerEventHandler eventRestartLevel;
	public event gameManagerEventHandler eventBackToMainMenu;
	public event gameManagerEventHandler eventGameOver;
	public event gameManagerEventHandler eventGameWin;

	public bool isGameOver;
	public bool isInventoryUIOn;
	public bool isMenuOn;

	public void callEventMenuToggle() {
		if (eventMenuToggle != null) {
			eventMenuToggle ();
		}
	}

	public void callEventInventoryUIToggle() {
		if (eventInventoryUIToggle != null) {
			eventInventoryUIToggle ();
		}
	}

	public void callEventRestartLevel() {
		if (eventRestartLevel != null) {
			eventRestartLevel ();
		}
	}

	public void callEventBackToMainMenu() {
		if (eventBackToMainMenu != null) {
			eventBackToMainMenu ();
		}
	}

	public void callEventGameOver() {
		if (eventGameOver != null) {
			isGameOver = true;
			eventGameOver ();
		}
	}

	public void callEventGameWin() {
		if (eventGameWin != null) {
			eventGameWin ();
		}
	}
}
