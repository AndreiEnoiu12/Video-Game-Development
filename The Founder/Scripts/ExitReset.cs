using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class restarts levels or exits to menu when hotkeys are pressed
public class ExitReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	//If the correct hotkey is pressed, level restarts or finishes
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.End)) 
		{
			Scene loadedLevel = SceneManager.GetActiveScene ();
			SceneManager.LoadScene (loadedLevel.buildIndex);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
