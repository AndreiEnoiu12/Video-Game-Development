using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ability used to Teleport the player to the starting position in the level
//Can be used only in the first objective of the game
public class Teleport : MonoBehaviour {

	private Vector3 spawnPos;
	private Quaternion spawnRot;
	public float tp;
	public Text tpText;
	private GameObject wall;

	public AudioClip audUsed;
	public AudioClip audTP;
	private AudioSource audioSource;

	//Save position and rotation at start
	void Awake ()
	{
		spawnPos = transform.position;
		spawnRot = transform.rotation;
		wall = GameObject.Find ("Wall Horizontal (255)");
	}

	//Audio source instance
	void Start()
	{
		audioSource = GetComponent<AudioSource > ();
	}

	//Check if hotkey was pressed and move player object at the start of the maze
	//Play sound if charges left or none left
	void Update ()
	{
		tpText.text = ("" + tp);
		if (Input.GetKeyDown(KeyCode.Backspace)) 
		{
			if (tp != 0) 
			{
				//GameObject wall = GameObject.Find ("Wall Horizontal (255)");
				if (wall.activeSelf == true) 
				{
					var player = GameObject.FindGameObjectWithTag ("Player");
					tp = tp - 1;
					player.transform.position = spawnPos;
					player.transform.rotation = spawnRot;
					audioSource.PlayOneShot (audTP, 1.5F);
				}
				else audioSource.PlayOneShot (audUsed, 0.25F);
			}
			else audioSource.PlayOneShot (audUsed, 0.25F);
		}
	}

}
