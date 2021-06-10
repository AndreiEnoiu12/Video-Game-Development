using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Checks if button has been pressed
//Notifies Controller to take action
public class CardScript1 : MonoBehaviour {


	bool _cardButtonDown1;

	private GameController gameController;

	void Start ()
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

	void Update ()
	{

		if (_cardButtonDown1) 
		{
			gameController.CardSelected1 ();
		}

	}

	public void OnCardButtonDown1 (bool down)
	{
		_cardButtonDown1 = down;
	}

}