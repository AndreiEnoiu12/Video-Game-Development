using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Master
{
	//Class used to set player health, effects and destroy object if fatal damage is dealt
    public class TankHealth : MonoBehaviour
    {
		public int playerNr = 1;
        public float health;
		private float healthRemaining; 

        public Slider healthBar;                             
        public Image healthRemainingBar;

        public Color healthFull;       
        public Color healthLow;

		private ParticleSystem explosionParticles;
		private AudioSource audioParticles;               
        public GameObject deathExplosion;                

		[HideInInspector] public MasterManager gameManager; 

		private bool dead;                                

		public GameObject chest;
		private GameObject instance;

		private int ld;

		//Instantiate effects and load settings to see if tank should be hidden or not
        private void Awake ()
        {
            
            explosionParticles = Instantiate (deathExplosion).GetComponent<ParticleSystem> ();
            audioParticles = explosionParticles.GetComponent<AudioSource> ();
            explosionParticles.gameObject.SetActive (false);


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

			ld = settingsData.battleSet;


        }

		//Hides tank based on scene loaded and player controler selected
		void Start () 
		{
			
			GameObject gameManagerObject = GameObject.FindWithTag ("GameManager");
			if (gameManagerObject != null)
			{
				gameManager = gameManagerObject.GetComponent <MasterManager>();
			}
			if (gameManager == null)
			{
				Debug.Log ("Cannot find 'GameManager' script");
			}

			if (SceneManager.GetActiveScene ().name == "BattleRoyale2" || SceneManager.GetActiveScene ().name == "BattleRoyale3") 
			{
				if (ld == 0 && gameObject.GetComponent<TankShooting> ().playerNr == 2) 
				{
					gameObject.SetActive (false);
				} 
				if (ld == 1 && gameObject.GetComponent<TankShooting> ().playerNr == 1) 
				{
					gameObject.SetActive (false);
				} 
			}
				

		}

		//Check if game is over and destroy the object if it's AI controlled
		void Update ()
		{
			
			if (gameManager.winner == true) 
			{
				if (gameObject.GetComponent<TankShooting> ().playerNr == 3 || gameObject.GetComponent<TankShooting> ().playerNr == 4 || gameObject.GetComponent<TankShooting> ().playerNr == 5) 
				{
					Death ();
				}
			}
		}

		//Enable health, health bar UI and set death bool
        private void OnEnable()
        {
            healthRemaining = health;
            dead = false;
            HealthBarCanvas();
        }

		//Deals damage to the objct
		//If fatal damage, call Death function
        public void TakeDamage (float dmg)
        {
            healthRemaining -= dmg;
            HealthBarCanvas ();

            if (healthRemaining <= 0f && !dead)
            {
                Death ();
            }

        }

		//Fatal damage is dealt, tank is hidden and effects are played
		//Instantiates treasure, set player score to 0 and add it to treasure
		//Starts respawn timer from gameManager
		private void Death ()
		{
			dead = true;

			explosionParticles.transform.position = transform.position;
			explosionParticles.gameObject.SetActive (true);
			explosionParticles.Play ();
			audioParticles.Play();

			gameManager.Respawn (gameObject.GetComponent<TankShooting>().playerNr);

			gameObject.SetActive (false);
			instance = (GameObject)Instantiate (chest, gameObject.transform.position, gameObject.transform.rotation);
			instance.gameObject.GetComponent<Rotate> ().calories = gameObject.GetComponent<PlayerCalories> ().playerScore;
			gameObject.GetComponent<PlayerCalories> ().playerScore = 0;
			gameManager.ScorePanel (gameObject.GetComponent<TankShooting>().playerNr, gameObject.GetComponent<PlayerCalories> ().playerScore, gameObject.GetComponent<PlayerCalories> ().targetScore);
			if (gameObject.GetComponent<TankShooting> ().playerNr == 3 || gameObject.GetComponent<TankShooting> ().playerNr == 4 || gameObject.GetComponent<TankShooting> ().playerNr == 5) 
			{
				gameObject.GetComponent<PatrolController>().PositionOrigin();
			}

		}

		//Updates health bar UI when damage is received
        private void HealthBarCanvas ()
        {
           
            healthBar.value = healthRemaining;
            healthRemainingBar.color = Color.Lerp (healthLow, healthFull, healthRemaining / health);

        }

    }
}