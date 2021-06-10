using UnityEngine;
using System.Collections;

//Check if player enters the start region
public class StartRegion : MonoBehaviour
{

	private GameController gameController;

	//Instance of gameController script from controller object
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


	//Detect collision with player and calls function from gameController
	void OnTriggerEnter(Collider other) 
	{

		if (other.tag == "Player")
		{
			gameController.StartRegion ();
			Destroy(gameObject);
		}

	}
}
