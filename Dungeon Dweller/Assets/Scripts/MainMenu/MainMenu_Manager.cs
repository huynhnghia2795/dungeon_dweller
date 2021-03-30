using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject playMenu;
	public GameObject groupMenu;
	public GameObject exitMenu;

	public void playButton() {
		if (playMenu != null && mainMenu != null) {
			mainMenu.SetActive (!mainMenu.activeSelf);
			playMenu.SetActive (!playMenu.activeSelf);
			groupMenu.SetActive (false);
			exitMenu.SetActive (false);
		}
	}

	public void groupButton() {
		if (groupMenu != null && mainMenu != null) {
			mainMenu.SetActive (!mainMenu.activeSelf);
			groupMenu.SetActive (!groupMenu.activeSelf);
			playMenu.SetActive (false);
			exitMenu.SetActive (false);
		}
	}

	public void exitButton() {
		if (exitMenu != null && mainMenu != null) {
			mainMenu.SetActive (!mainMenu.activeSelf);
			exitMenu.SetActive (!exitMenu.activeSelf);
			playMenu.SetActive (false);
			groupMenu.SetActive (false);
		}
	}

	public void returnButton() {
		if (mainMenu != null && playMenu != null && groupMenu != null && exitMenu != null) {
			mainMenu.SetActive (true);
			playMenu.SetActive (false);
			groupMenu.SetActive (false);
			exitMenu.SetActive (false);
		}
	}

	public void loadMap01() {
		SceneManager.LoadScene (1, LoadSceneMode.Single);
		Time.timeScale = 1;
	}

	public void loadMap02() {
		SceneManager.LoadScene (2, LoadSceneMode.Single);
		Time.timeScale = 1;
	}

	public void exitConfirm() {
		Application.Quit ();
	}
}
