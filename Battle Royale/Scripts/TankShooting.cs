using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Master
{
	//Instantiates missile and shoots it in a direction at a different speed and load
	//Activates world UI arrow for better aim
    public class TankShooting : MonoBehaviour
    {
		
        public int playerNr = 1;

		public Transform shootPos;
		public Rigidbody missileShell;

		private string shootButton;                
		private float chargeLoad;         
		private float chargeSpeed;                 

        public AudioSource audioTank;         
        public AudioClip audCharge;            
        public AudioClip audShot;

        public float chargeMin;        
        public float chargeMax;        
        public float chargeTime;  

		public Slider aimArrow;
                       
		private bool isShot;  

		//Called when object is enabled and active
        private void OnEnable()
        {
            chargeLoad = chargeMin;
            aimArrow.value = chargeMin;
        }

		//Sets shoot button to input key per player
        private void Start ()
        {
			shootButton = "Fire" + playerNr;
            chargeSpeed = (chargeMax - chargeMin) / chargeTime;

        }

		//Shoots  missile and plays audio clip
		//Charge based on minmum charge
		private void Shoot ()
		{

			isShot = true;

			Rigidbody rbShell = Instantiate (missileShell, shootPos.position, shootPos.rotation) as Rigidbody;
			rbShell.velocity = chargeLoad * shootPos.forward; 

			audioTank.clip = audShot;
			audioTank.Play ();
			chargeLoad = chargeMin;

		}

		//Calculates the distance of the missile shot based on how long the key was pressed
		//Updates UI arrow and increases it's size by changing the value of the component
		//Only player 1 and 2 can shoot missiles
        private void Update ()
        {
			if (playerNr == 1 || playerNr == 2) 
			{
				
				aimArrow.value = chargeMin;

				if (chargeLoad >= chargeMax && !isShot) 
				{
					chargeLoad = chargeMax;
					Shoot ();

				}
				else if (Input.GetButtonDown (shootButton) && !Input.GetKeyDown (KeyCode.RightAlt)) 
				{
					
					isShot = false;
					chargeLoad = chargeMin;
					audioTank.clip = audCharge;
					audioTank.Play ();

				}
				else if (Input.GetButton (shootButton) && !Input.GetKeyDown (KeyCode.RightAlt) && !isShot) 
				{
					chargeLoad += chargeSpeed * Time.deltaTime;
					aimArrow.value = chargeLoad;

				}
	            
				else if (Input.GetButtonUp (shootButton) && !Input.GetKeyDown (KeyCode.RightAlt) && !isShot) 
				{
					Shoot ();

				}
			}
        }

    }
}