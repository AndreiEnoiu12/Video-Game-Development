using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//Has the purpose of setting the objective, instantiating hostile objects and check for game conditions
//Inputs for leaving and restarting are assigned here
public class GameController2 : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 startZone;
	public int objectsNr;
	public float spawnCD;
	public float startCD;
	public float cycleCD;

	public Text scoreText;
	public Text gameOverText;
	public GameObject gameOverPanel;
	private bool gameOverChecker;
	private int score;
	public int target;

	public int op;
	public Text objectiveText;
	public GameObject startPanel;
	public Text gameText;

	//First we set an objective randomly
	void Awake ()
	{
		Cursor.lockState = CursorLockMode.None;
		op = Random.Range (1, 6);

		if (op == 1) 
		{
			objectiveText.text = "Objective: Collect operators from C/C++ that are Arithmetic type";
			gameText.text = objectiveText.text;
		}
		if (op == 2) 
		{
			objectiveText.text = "Objective: Collect operators from C/C++ that are Relational type";
			gameText.text = objectiveText.text;
		}
		if (op == 3) 
		{
			objectiveText.text = "Objective: Collect operators from C/C++ that are Logical type";
			gameText.text = objectiveText.text;
		}
		if (op == 4) 
		{
			objectiveText.text = "Objective: Collect operators from C/C++ that are Assignment type";
			gameText.text = objectiveText.text;
		}
		if (op == 5) 
		{
			objectiveText.text = "Objective: Collect operators from C/C++ that are Bitwise type";
			gameText.text = objectiveText.text;
		}
	}

	//Starting values for game controller and updates score
	//Starts the SpawnWaves() function
	void Start ()
	{
		gameOverChecker = false;
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	//When there is an input from keyboard, player restarts level or is sent to Main Menu
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.End))
		{
			Scene loadedLevel = SceneManager.GetActiveScene ();
			SceneManager.LoadScene (loadedLevel.buildIndex);
		}
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			SceneManager.LoadScene ("MainMenu");
		}

	}

	//Used for spawning waves at an interval in random locations
	//Each wave is different since objects instantiated are chosen by random from the array of objects
	//Stops if game is over
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startCD);
		startPanel.SetActive (false);

		while (true)
		{
			for (int i = 0; i < objectsNr; i++)
			{
				GameObject type = hazards[Random.Range (0, hazards.Length)];
				Vector3 location = new Vector3 (Random.Range (-startZone.x, startZone.x), startZone.y, startZone.z);
				Quaternion rotation = Quaternion.identity;

				Instantiate (type, location, rotation);
				yield return new WaitForSeconds (spawnCD);
			}

			yield return new WaitForSeconds (cycleCD);
			if (gameOverChecker)
			{
				break;
			}
		}
	}

	//Adds score each time player collects a right resource
	public void AddScore (int newScore)
	{
		score += newScore;
		UpdateScore ();
	}

	//Removes score each time player collects a wrong resource
	public void RemoveScore (int newScore)
	{
		score -= newScore;
		if (score < 0) 
		{
			score = 0;
		}
		UpdateScore ();
	}

	//Updates the UI text for resource score and checks if score meets target
	//If level is unlimited, no game over is shown
	void UpdateScore ()
	{
		if (SceneManager.GetActiveScene ().name == "SpaceRangers3") {
			scoreText.text = score + "";
		} else {
			scoreText.text = score + "/" + target;
			if (score >= target) 
			{
				/*if (SceneManager.GetActiveScene ().buildIndex != 2)
				{
					SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
				}*/
				gameOverPanel.SetActive (true);
				gameOverText.text = "Success!";
				gameOverChecker = true;
			}
		}
	}

	//The GameOver function is called when a player object is destroyed
	//If there are no players left, lose condition triggers and game ends
	public void GameOver ()
	{	
		
		if (GameObject.FindGameObjectsWithTag("Player").Length == 1 && gameOverChecker == false) 
		{
			gameOverPanel.SetActive(true);
			gameOverText.text = "Game Over!";
			gameOverChecker = true;
		}
	}
}