using UnityEngine;
using System.Collections;

//Check if player enters the finish region
public class FinishRegion : MonoBehaviour
{

	private GameController gameController;

	//Instance of game controller script from the object
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


	//Triggers if collision is with a player, destroying itself and calling function from controller
	void OnTriggerEnter(Collider other) 
	{
		
		if (other.tag == "Player")
		{
			gameController.FinishRegion ();
			Destroy(gameObject);
		}

	}
}