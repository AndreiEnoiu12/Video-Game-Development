using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class used to make the Boost ability work when player presses hotkey
public class Boost : MonoBehaviour {

	private float boost = 1;
	private float originalSpeed;
	public int boostSeconds;
	public Text boostText;

	public AudioClip audUsed;
	public AudioClip audBoost;
	private AudioSource audioSource;

	//Instance of audio source to be used
	void Start()
	{
		audioSource = GetComponent<AudioSource > ();
	}

	// Check every frame if button pressed, if boost time ends and update UI text
	//Doubles the speed of the player for a limited time and plays sound
	//Can detect if no boost charges are left
	void Update ()
	{
		boostText.text = ("" + boost);
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) 
		{
			if (boost != 0)
			{
				var player = GameObject.FindGameObjectWithTag ("Player");

				if (player.transform.position.y <= 0.7) 
				{
					boost = 0;
					originalSpeed = player.GetComponent<KeyboardController> ().speed;
					player.GetComponent<KeyboardController> ().speed = 14;
					StartCoroutine("BoostTime");
					audioSource.PlayOneShot (audBoost, 0.4F);
				}
			}
			else audioSource.PlayOneShot (audUsed, 0.25F);
		}

		if (boostSeconds <= 0)
		{
			var player = GameObject.FindGameObjectWithTag ("Player");
			StopCoroutine("BoostTime");
			player.GetComponent<KeyboardController> ().speed = originalSpeed;
		}
	}

	//Time during which the boost is active	
	IEnumerator BoostTime()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			boostSeconds--;
		}
	}
}
