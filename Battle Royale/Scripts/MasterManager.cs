using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Master
{
	//Controls the entire level and scene logic
	//Tanks are enabled, disabled and win / lose conditions are verified
    public class MasterManager : MonoBehaviour
    {
         
        public float beginTime;             
        public float finalTime;                      
                
        public GameObject[] Models;

        public TankManager[] m_Tanks;               
		         
		public CameraPositioning cameraRig;

		public int deathTime1 = 10;
		public int deathTime2 = 10;
		public int deathTime3 = 45;
		public int deathTime4 = 45;
		public int deathTime5 = 45;
		                
		private WaitForSeconds beginSeconds;         
		private WaitForSeconds endSeconds;    

		public Text deathTimerText1;
		public Text deathTimerText2;
		public GameObject respawnPanel1;
		public GameObject respawnPanel2;
		  

		public Text scoreText1;
		public Text scoreText2;

		public GameObject gamePanel;
		public Text gameText;

		public GameObject winPanel1;
		public GameObject winPanel2;
		public bool winner;
		public int gameTimer;
		public Text gameTimerText;

		public GameObject hidePanel1;
		public GameObject hidePanel2;
		public GameObject hidePanel3;
		public GameObject hidePanel4;

		private int levelCounter; 

		//Load settings file and enable mouse
		//If single player map, hides respective panels
		void Awake ()
		{
			Cursor.lockState = CursorLockMode.None;

			string destination = Application.persistentDataPath + "/settings.dat";
			FileStream file;

			if (File.Exists (destination))
				file = File.OpenRead (destination);
			else {
				Debug.Log ("File not found");
				return;
			}

			BinaryFormatter bf = new BinaryFormatter ();
			GameData settingsData = (GameData)bf.Deserialize (file);
			file.Close ();

			int ld = settingsData.battleSet;

			if (SceneManager.GetActiveScene ().name == "BattleRoyale2" || SceneManager.GetActiveScene ().name == "BattleRoyale3") 
			{
				if (ld == 0) 
				{
					hidePanel3.SetActive (false);
					hidePanel4.SetActive (false);
				} 
				if (ld == 1) 
				{
					hidePanel1.SetActive (false);
					hidePanel2.SetActive (false);
				}
			}


		}

		//Instantiates the tanks and sets up the camera at the start of the level
		//Timers and UI text set
        private void Start()
        {
            
            beginSeconds = new WaitForSeconds (beginTime);
            endSeconds = new WaitForSeconds (finalTime);
            InstantiateTanks();
            CameraTanks();
			gameTimerText.text = (gameTimer + " seconds");
			StartCoroutine ("GameTimer");
			winner = false;

            
            StartCoroutine (Match ());

        }

		//Spawns all tanks and their start location
		//Calls setup function from TankManager
        private void InstantiateTanks()
        {
            
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                
                m_Tanks[i].self = Instantiate(Models[i], m_Tanks[i].spawnLoc.position, m_Tanks[i].spawnLoc.rotation) as GameObject;

                m_Tanks[i].playerNr = i + 1;

                m_Tanks[i].Setup();

            }
        }

		//Updates each player's UI score if they picked fruit
		public void ScorePanel (int playerNr, int playerScore, int targetScore)
		{
			if (playerNr == 1) 
			{
				scoreText1.text = (playerScore + "/" + targetScore);
			}
			if (playerNr == 2) 
			{
				scoreText2.text = (playerScore + "/" + targetScore);
			}
		}


        //Starts the level after the wait time
        private IEnumerator Match ()
        {
           
            yield return StartCoroutine (MatchStart ());

            
			ActivateTank ();
			gamePanel.SetActive (false);


        }

		//Makes sure tha tanks can't move during the beginning phase
		//Initial values for camera position set
        private IEnumerator MatchStart ()
        {
           
            RestartTanks ();
            DeactivateTank ();
            cameraRig.InitialSetup ();

            
            levelCounter++;
            yield return beginSeconds;

        }

		//Function called when a player has achieved objective (can be 1 or 2)
		public void WinPanel (int playerNr)
		{
			if (playerNr == 1 && winner == false) 
			{
				winner = true;
				StopCoroutine ("GameTimer");
				winPanel1.SetActive (true);
				DeactivateTank ();
			}
			if (playerNr == 2 && winner == false) 
			{
				winner = true;
				StopCoroutine ("GameTimer");
				winPanel2.SetActive (true);
				DeactivateTank ();
			}
		}

		//Checks all respawn timers and main countdown timer
		//Lose condition if timer ends
		//If tank respawns, enable controls and reactive at start point
		void Update ()
		{

			if (gameTimer == 0 && winner == false) 
			{
				winner = true;
				StopCoroutine ("GameTimer");
				gameTimerText.text = ("Out of Time!");
				DeactivateTank ();
				gamePanel.SetActive (true);
				gameText.text = ("Time is over! Please try again!");
			}

			if (deathTime1 == 0) 
			{
				deathTime1 = 10;
				StopCoroutine ("DeathTimer1");
				m_Tanks[0].Respawner();
				m_Tanks[0].ActivateCommands();
				respawnPanel1.SetActive (false);
			}
			if (deathTime2 == 0) 
			{
				deathTime2 = 10;
				StopCoroutine ("DeathTimer2");
				m_Tanks[1].Respawner();
				m_Tanks[1].ActivateCommands();
				respawnPanel2.SetActive (false);
			}
			if (deathTime3 == 0) 
			{
				deathTime3 = 45;
				StopCoroutine ("DeathTimer3");
				m_Tanks[2].Respawner();
				m_Tanks[2].ActivateCommands();
			}
			if (deathTime4 == 0) 
			{
				deathTime4 = 45;
				StopCoroutine ("DeathTimer4");
				m_Tanks[3].Respawner();
				m_Tanks[3].ActivateCommands();
			}
			if (deathTime5 == 0) 
			{
				deathTime5 = 45;
				StopCoroutine ("DeathTimer5");
				m_Tanks[4].Respawner();
				m_Tanks[4].ActivateCommands();
			}


		}

		//Sets respawn UI panels active and begins respawn timer for all tanks
		public void Respawn (int playerNr)
		{
			if (playerNr == 1) 
			{
				deathTimerText1.text = ("" + deathTime1);
				StartCoroutine ("DeathTimer1");
				respawnPanel1.SetActive (true);
			}
			if (playerNr == 2) 
			{
				deathTimerText2.text = ("" + deathTime2);
				StartCoroutine ("DeathTimer2");
				respawnPanel2.SetActive (true);
			}
			if (playerNr == 3) 
			{
				StartCoroutine ("DeathTimer3");
			}
			if (playerNr == 4) 
			{
				StartCoroutine ("DeathTimer4");
			}
			if (playerNr == 5) 
			{
				StartCoroutine ("DeathTimer5");
			}
		}

		//Activates the control of all tanks availble
		private void ActivateTank()
		{

			for (int i = 0; i < m_Tanks.Length; i++)
			{
				m_Tanks[i].ActivateCommands();
			}

		}

		//Deactivates the control of all tanks available
		//Used when win / lose condition achieved
		private void DeactivateTank()
		{

			for (int i = 0; i < m_Tanks.Length; i++)
			{
				m_Tanks[i].DeactivateCommands();
			}

		}

		//Respawn time for tank
		IEnumerator DeathTimer1()
		{
			while (true && winner == false)
			{
				yield return new WaitForSeconds(1);
				deathTime1--;
				deathTimerText1.text = ("" + deathTime1);
			}
		}

		//Respawn time for tank
		IEnumerator DeathTimer2()
		{
			while (true && winner == false)
			{
				yield return new WaitForSeconds(1);
				deathTime2--;
				deathTimerText2.text = ("" + deathTime2);
			}
		}

		//Respawn time for tank
		IEnumerator DeathTimer3()
		{
			while (true && winner == false)
			{
				yield return new WaitForSeconds(1);
				deathTime3--;
			}
		}

		//Respawn time for tank
		IEnumerator DeathTimer4()
		{
			while (true && winner == false)
			{
				yield return new WaitForSeconds(1);
				deathTime4--;
			}
		}

		//Respawn time for tank
		IEnumerator DeathTimer5()
		{
			while (true && winner == false)
			{
				yield return new WaitForSeconds(1);
				deathTime5--;
			}
		}


		//Re-enable tank control after respawn
        private void RestartTanks()
        {
			
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].Respawner();
            }

        }
			
		//Countdown timer for objective
		IEnumerator GameTimer()
		{
			while (true)
			{
				yield return new WaitForSeconds(1);
				gameTimer--;
				gameTimerText.text = (gameTimer + " seconds");
			}
		}

		//Sets all tanks for the camera object to follow from the list
		private void CameraTanks()
		{

			Transform[] targets = new Transform[m_Tanks.Length];


			for (int i = 0; i < targets.Length; i++)
			{

				targets[i] = m_Tanks[i].self.transform;
			}


			cameraRig.targets = targets;

		}

    }
}