using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Master
{
	//Class in charge of the movement of the tank vehicle (player only)
	//Applies dust particle effects when moving
    public class TankMovement : MonoBehaviour
    {
		
        public int playerNr = 1;
		private float verticalInput;         
		private float horizontalInput;

        public float speed;                 
        public float tiltRate;

		private Rigidbody rb; 
		private ParticleSystem[] particles; 

        public AudioSource audioMotorTank;         
        public AudioClip audEngine1;            
        public AudioClip audEngine2;

		public float pitchSlider;           
                         
		private float pitchSource;             

		private string verticalName;          
		private string horizontalName;

		//Create instance of rigidbody of the object
        private void Awake ()
        {
            rb = GetComponent<Rigidbody> ();
        }

		//Modify rigidbody, set input numbers for each player
		//Plays particle effects attached to tank object
        private void OnEnable ()
        {    
            rb.isKinematic = false;

			horizontalInput = 0f;
            verticalInput = 0f;

            particles = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < particles.Length; ++i)
            {
				
                particles[i].Play();

            }
        }

		//Disables tank and stops particles
        private void OnDisable ()
        {
            rb.isKinematic = true;

            for(int i = 0; i < particles.Length; ++i)
            {
				
                particles[i].Stop();

            }
        }

		//Sets input axis names for each player
        private void Start ()
        {
            verticalName = "Vertical" + playerNr;
            horizontalName = "Horizontal" + playerNr;

            pitchSource = audioMotorTank.pitch;
        }

		//Sets input axis for each player axis name and plays sound Engine
        private void Update ()
        {
			if (playerNr == 1 || playerNr == 2) 
			{
				verticalInput = Input.GetAxis (verticalName);
				horizontalInput = Input.GetAxis (horizontalName);
			}
			SoundEngine ();
        }

		//Engine sound clips are played at random pitch through the motor Audio Source
        private void SoundEngine ()
        {
			
            if (Mathf.Abs (verticalInput) < 0.1f && Mathf.Abs (horizontalInput) < 0.1f)
            {
                
                if (audioMotorTank.clip == audEngine2)
                {
                    audioMotorTank.clip = audEngine1;
                    audioMotorTank.pitch = Random.Range (pitchSource - pitchSlider, pitchSource + pitchSlider);
                    audioMotorTank.Play ();
                }
            }
            else
            { 
                if (audioMotorTank.clip == audEngine1)
                {
                    audioMotorTank.clip = audEngine2;
                    audioMotorTank.pitch = Random.Range(pitchSource - pitchSlider, pitchSource + pitchSlider);

                    audioMotorTank.Play();
                }
            }
        }

		//Each fixed frame, player moves and turns based on inputs
        private void FixedUpdate ()
        {
			if (playerNr == 1 || playerNr == 2) 
			{
				Movement ();
				Turning ();
			}
        }

		//Tank movement based on speed and vertical input (player only)
        private void Movement ()
        {
            Vector3 movement = transform.forward * verticalInput * speed * Time.deltaTime;

            rb.MovePosition(rb.position + movement);
        }

		//Tank movement based on speed and horizontal input (player only)
        private void Turning ()
        {
			float turning = horizontalInput * tiltRate * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler (0f, turning, 0f);

            rb.MoveRotation (rb.rotation * turnRotation);

        }
    }
}