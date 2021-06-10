using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that makes the ball jump when button is pressed
//Adds force to player object and plays sound
public class JumpKey : MonoBehaviour {

	Rigidbody rb;
	public AudioClip audJump;
	private AudioSource audioSource;

	//Get components of object
	void Start()
	{
		audioSource = GetComponent<AudioSource > ();
		rb = GetComponent<Rigidbody>();
	}

	//Detect button press and create jump with rigibody force
	void Update()
	{
		var player = GameObject.FindGameObjectWithTag("Player");

		if (player == null) 
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space) && player.transform.position.y <= 0.7)
		{
			audioSource.PlayOneShot (audJump, 0.22F);
			rb.AddForce(new Vector3(0, 8, 0), ForceMode.Impulse);
		}
	}

}
