using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This class is used to control the ranger (player) ships
//Boundary is set so that the ships don't get out of border
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class RangerShips : MonoBehaviour
{
	public Boundary boundary;
	
	public float playerNumber;
	public float playerSpeed;
	public float playerLean;

	public AudioClip audCrates;
	public AudioClip audCrates2;
	public AudioClip audWeapon;
	private AudioSource audioSource;
	private GameController2 gameController2;

	public GameObject laser;
	public Transform laserLoc;
	public float laserCD;
	private float laserDelay;

	private Rigidbody rigid;

	//Every frame, we check which player has pressed the fire button and instantiate a laser object
	//Plays sound and adds cooldown (so you don't shoot too many too fast)
	void Update ()
	{
		if (playerNumber == 1) {
			if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > laserDelay) {
				laserDelay = Time.time + laserCD;
				Instantiate (laser, laserLoc.position, laserLoc.rotation);
				audioSource.PlayOneShot (audWeapon, 0.7F);
			}
		}

		if (playerNumber == 2) {
			if (Input.GetKeyDown(KeyCode.LeftControl) && !Input.GetKeyDown(KeyCode.RightAlt)  && Time.time > laserDelay) {
				laserDelay = Time.time + laserCD;
				Instantiate (laser, laserLoc.position, laserLoc.rotation);
				audioSource.PlayOneShot (audWeapon, 0.7F);
			}
		}
	}

	//Instances of rigidbody, audio source and controller set
	void Start ()
	{
		audioSource = GetComponent<AudioSource >();
		rigid = GetComponent<Rigidbody>();

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

	//Every fixed frame, player moves within boundaries and at a set speed using input axis
	//Each player has his own movement statement
	void FixedUpdate ()
	{
		if (playerNumber == 1) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rigid.velocity = movement * playerSpeed;

			rigid.position = new Vector3 (
				Mathf.Clamp (rigid.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigid.position.z, boundary.zMin, boundary.zMax)
			);

			rigid.rotation = Quaternion.Euler (0.0f, 0.0f, rigid.velocity.x * -playerLean);
		}

		if (playerNumber == 2) {
			float moveHorizontal = Input.GetAxis ("Horizontal2");
			float moveVertical = Input.GetAxis ("Vertical2");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rigid.velocity = movement * playerSpeed;

			rigid.position = new Vector3 (
				Mathf.Clamp (rigid.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigid.position.z, boundary.zMin, boundary.zMax)
			);

			rigid.rotation = Quaternion.Euler (0.0f, 0.0f, rigid.velocity.x * -playerLean);
		}
	}

	//Used as a collision detector between the player ships and the resource objects
	//When crate or barrel, we check if they match the type from objective
	//Adds or removes points and plays audio clip
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Crate"))
		{
			if (other.gameObject.GetComponent <ResourceText> ().type == gameController2.op) 
			{
				gameController2.AddScore (100);
				audioSource.PlayOneShot (audCrates, 0.7F);
				Destroy (other.gameObject);
			} else {
					gameController2.RemoveScore (100);
					audioSource.PlayOneShot (audCrates2, 1.35F);
					Destroy (other.gameObject);
				}
		}

		if (other.gameObject.CompareTag ( "Barrel"))
		{
			if (other.gameObject.GetComponent <ResourceText> ().type == gameController2.op) 
			{
				gameController2.AddScore (250);
				audioSource.PlayOneShot (audCrates, 0.7F);
				Destroy (other.gameObject);
			} else {
				gameController2.RemoveScore (250);
				audioSource.PlayOneShot (audCrates2, 1.35F);
				Destroy (other.gameObject);
			}
		}
	}
		
}