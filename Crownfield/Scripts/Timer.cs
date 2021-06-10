using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Class that starts the timer coroutine when the user must return to the start of the maze
public class Timer : MonoBehaviour
{
	public int timeLeft;
	public Text countdownText;
	private GameController gameController;

	//Find GameController object and script
	void Start()
	{

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		if (gameControllerObject != null)

		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}

		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	//Each frame, check if timer is 0 to trigger lose condition
	void Update()
	{

		if (timeLeft <= 0)
		{
			StopCoroutine("LoseTime");
			countdownText.text = "Out of time!";
			countdownText.color = Color.red;
			gameController.TimerOver ();
		}
	}

	//Every second, the counter goes down.
	//UI Text is colored red
	IEnumerator LoseTime()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			countdownText.color = Color.red;
			timeLeft--;
			countdownText.text = ("Time Left: " + timeLeft + " sec");
		}
	}
}