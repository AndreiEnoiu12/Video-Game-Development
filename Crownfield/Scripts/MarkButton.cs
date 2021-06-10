using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Instantiates a banana on the floor for player to remember on the way back
public class MarkButton : MonoBehaviour
{

	Vector3 startPos;
	public GameObject hazard;
	public float banana;
	public Text bananasText;

	public AudioClip audUsed;
	private AudioSource audioSource;

	//Audio instance
	void Start()
	{
		audioSource = GetComponent<AudioSource > ();
	}

	//Check if hotkey was pressed and instantiate a banana at player location
	//Play sound if we have charges and if we don't
	void Update ()
	{
		bananasText.text = ("" + banana);
		if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) 
		{
			if (banana != 0)
			{
				var player = GameObject.FindGameObjectWithTag ("Player");

				if (player.transform.position.y <= 0.7) 
				{
					startPos = player.transform.position;
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, startPos, spawnRotation);
					banana = banana - 1;
				}
			}
			else audioSource.PlayOneShot (audUsed, 0.25F);
	
		}
	}

}
