using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrickScript : MonoBehaviour
{
	private int nr;
	private int lives;
	private int scoreBrick;

	private GameController gameController;
	public Material BlackM;
	public Material PinkM;
	public Material GreenM;

	private int NrLives;

	void Awake ()
	{
		GameObject gameControllerObject2 = GameObject.FindWithTag ("GameController");
		if (gameControllerObject2 != null)
		{
			gameController = gameControllerObject2.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		nr = Random.Range (1, 4);

		if (nr == 1) 
		{
			gameObject.GetComponent<MeshRenderer>().material = BlackM;
			lives = 3;
			scoreBrick = 0;
		}
		if (nr == 2) 
		{
			gameController.AddBreakable();
			gameObject.GetComponent<MeshRenderer>().material = GreenM;
			lives = 2;
			scoreBrick = 15;
		}
		if (nr == 3) 
		{
			gameController.AddBreakable();
			gameObject.GetComponent<MeshRenderer>().material = PinkM;
			lives = 1;
			scoreBrick = 10;
		}
	}

	void Start ()
	{
		NrLives = lives;
		GameObject gameControllerObject2 = GameObject.FindWithTag ("GameController");
		if (gameControllerObject2 != null)
		{
			gameController = gameControllerObject2.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnCollisionEnter(Collision other) 
	{
		if (NrLives == 1) 
		{
			gameController.AddScore (scoreBrick);
			Destroy (gameObject);
		} 
		else 
		{
			if (NrLives == 2)
			{
					NrLives--;
			} 
		}
	}
}