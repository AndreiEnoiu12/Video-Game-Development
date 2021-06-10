using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Class in charge of game logic for win / loss condition
public class GameManager : MonoBehaviour 
{


	public GameObject panelLose;

	public static bool gameOver;

	//Game is not over at start
	void Start ()
	{
		gameOver = false;
	}

	//Check if game is over
	//Check if player has health above 0, if not, lose condition
	void Update () 
	{
		if (gameOver) return;

		if (GameplaySettings.healthTotal <= 0)
		{
			panelLose.SetActive (true);
			LoseCondition();
		}
	}

	//Win condition sets bool true to make update() return
	public void WinCondition ()
	{
		gameOver = true;
	}

	//Lose condition sets bool false to make update() return
	void LoseCondition ()
	{
		gameOver = true;
	}

}
