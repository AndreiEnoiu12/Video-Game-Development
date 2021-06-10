using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player controls the ball through this script
//Can use both WASD and arrow key controls
public class KeyboardController : MonoBehaviour {
	
	public float speed;

	private Rigidbody rb;

	private GameObject des1;

	//find wall on awake
	void Awake ()
	{
 		des1 = GameObject.Find ("Wall Horizontal (256)");
	}

	//Instance of rigidbody created
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	//Moves the player using the axis each fixed frame
	//Adds moving force based on speed
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal2 = Input.GetAxis ("Horizontal2");
		float moveVertical2 = Input.GetAxis ("Vertical2");

		if(des1 == null || des1.activeSelf == false)
		{
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
		}
		if(des1 == null || des1.activeSelf == false)
		{
			Vector3 movement = new Vector3 (moveHorizontal2, 0.0f, moveVertical2);
			rb.AddForce (movement * speed);
		}
	}

}
