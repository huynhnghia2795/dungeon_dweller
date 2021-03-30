using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Score : MonoBehaviour {

	private Text scoreText;
	private Text gameOverHighScoreText;
	private Text pauseHighScoreText;
	private Text winHighScoreText;

	public GameObject scoreUI;
	public GameObject pauseHighScoreUI;
	public GameObject gameOverHighScoreUI;
	public GameObject winHighScoreUI;
	public static int score;
	public static int highScore;

	void Start() {
		scoreText = scoreUI.GetComponent<Text> ();
		pauseHighScoreText = pauseHighScoreUI.GetComponent<Text> ();
		gameOverHighScoreText = gameOverHighScoreUI.GetComponent<Text> ();
		winHighScoreText = winHighScoreUI.GetComponent<Text> ();
		score = 0;
		highScore = PlayerPrefs.GetInt ("HighScore", 0);
	}

	void Update () {
		scoreText.text = "Score: " + score;
		pauseHighScoreText.text = "Score: " + score + "\n" + "High Score: " + highScore;
		gameOverHighScoreText.text = "Score: " + score + "\n" + "High Score: " + highScore;
		winHighScoreText.text = "Score: " + score + "\n" + "High Score: " + highScore;

		if (score >= highScore) {
			PlayerPrefs.SetInt ("HighScore", score);
			pauseHighScoreText.text = "Score: " + score + "\n" + "High Score: " + score;
			gameOverHighScoreText.text = "Score: " + score + "\n" + "High Score: " + score;
			winHighScoreText.text = "Score: " + score + "\n" + "High Score: " + highScore;
		}
	}
}
