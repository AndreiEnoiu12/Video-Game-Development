using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject panelWin;
	public GameObject panelLose;
	public GameObject panelTime;

	public GameObject ball;
	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject restartButton;
	public GameObject quitButton;

	public Text scoreWin;
	public Text scoreLose;
	public Text scoreTimer;

	public Text timerText;
	public Text scoreText;

	private int total = 0;
	private int score;
	private int timer = 59;

	public GameObject line;

	void Start () 
	{
		score = 0;
		StartCoroutine ("GameTimer");
	}


	void Update () 
	{
		if (timer == 0) 
		{
			ball.gameObject.SetActive (false);
			leftButton.gameObject.SetActive (false);
			rightButton.gameObject.SetActive (false);
			StopCoroutine ("GameTimer");
			scoreTimer.text = ("Out of Time" + "\n Score:" + score + "");
			panelTime.gameObject.SetActive (true);
			restartButton.gameObject.SetActive (true);
			quitButton.gameObject.SetActive (true);
		}

		if (GameObject.FindWithTag("Line") == null)
		{
			StopCoroutine ("GameTimer");
			scoreLose.text = ("Game Over" + "\n Score:" + score + "");
			leftButton.gameObject.SetActive (false);
			rightButton.gameObject.SetActive (false);
			panelLose.gameObject.SetActive (true);
			restartButton.gameObject.SetActive (true);
			quitButton.gameObject.SetActive (true);
		}
		if (total == 0) 
		{
			ball.gameObject.SetActive (false);
			StopCoroutine ("GameTimer");
			scoreWin.text = ("Winner" + "\n Score:" + score + "");
			leftButton.gameObject.SetActive (false);
			rightButton.gameObject.SetActive (false);
			panelWin.gameObject.SetActive (true);
			restartButton.gameObject.SetActive (true);
			quitButton.gameObject.SetActive (true);
		}
	}

	public void AddScore (int newScore)
	{
		score += newScore;
		total--;
		scoreText.text = ("" + score + "");
	}

	public void AddBreakable()
	{
		total++;
	}



	IEnumerator GameTimer()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			timer--;
			timerText.text = ("" + timer + " seconds");
		}
	}
}
