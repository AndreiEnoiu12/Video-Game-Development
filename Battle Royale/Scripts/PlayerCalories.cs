using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Master
{
	//Keeps track of the player calories collected, calls the UI updater methods and checks if target is reached
	public class PlayerCalories : MonoBehaviour {

		public int playerScore;
		public int targetScore;

		[HideInInspector] public MasterManager gameManager;
		private bool checker;

		// Find GameMnager object and at score and update score
		void Start () {

			GameObject gameManagerObject = GameObject.FindWithTag ("GameManager");

			if (gameManagerObject != null)
			{
				gameManager = gameManagerObject.GetComponent <MasterManager>();
			}

			if (gameManager == null)
			{
				Debug.Log ("Cannot find 'GameManager' script");
			}

			playerScore = 0;
			gameManager.ScorePanel (gameObject.GetComponent<TankShooting>().playerNr, playerScore, targetScore);
			checker = true;

		}

		// Check if player has target score to trigger win condition
		void Update () 
		{
			if (playerScore >= targetScore && checker == true) 
			{
				checker = false;
				gameManager.WinPanel (gameObject.GetComponent<TankShooting> ().playerNr);
			}
			
		}

		//Detect collision and checks if its a food to collect
		//Destroys fruit object upon hit and plays sound
		void OnTriggerEnter(Collider other) 
		{
			if (other.gameObject.CompareTag ("Food"))
			{
				playerScore = playerScore + other.gameObject.GetComponent <Rotate> ().calories;
				gameManager.ScorePanel (gameObject.GetComponent<TankShooting>().playerNr, playerScore, targetScore);
				Destroy(other.gameObject);
				if(gameObject.GetComponent<TankShooting>().playerNr == 1 || gameObject.GetComponent<TankShooting>().playerNr == 2)
				{
					GameObject.FindWithTag ("Food2").GetComponent <FruitSpawner>().SoundPick();
				}
			}
		}

	}
}
