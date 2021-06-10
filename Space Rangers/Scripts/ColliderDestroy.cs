using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Checks for collisions between the object with this script and another one
public class ColliderDestroy : MonoBehaviour
{
	public GameObject explosiveEffect;

	public GameObject playerExplosiveEffect;

	public int score;


	private GameController2 gameController2;

	//Creates gamecontroller object
	void Start ()
	{
		GameObject gameControllerObject2 = GameObject.FindWithTag ("GameController");
		if (gameControllerObject2 != null)
		{
			gameController2 = gameControllerObject2.GetComponent <GameController2>();
		}
		if (gameController2 == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	//Detects collision with other object and determines which type of object it is
	//If player, destroys both objects
	//If laser, adds score and destroys only current object
	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy"))
		{
			return;
		}

		if (explosiveEffect != null)
		{
			Instantiate (explosiveEffect, transform.position, transform.rotation);
		}
			
		if (other.tag == "Player")
		{
			gameController2.GameOver ();
			Instantiate(playerExplosiveEffect, other.transform.position, other.transform.rotation);
		}
			
		gameController2.AddScore (score);

		Destroy(other.gameObject);

		Destroy(gameObject);
	}
}
