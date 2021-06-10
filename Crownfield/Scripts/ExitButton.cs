using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Checks if button has been pressed
//Notifies Controller to take action
public class ExitButton : MonoBehaviour
{
	bool _exitButtonDown;

	// Update is called once per frame
	void Update ()
	{
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
