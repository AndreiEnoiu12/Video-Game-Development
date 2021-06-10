using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//when button is pressed, the Demo / Tutorial level is loaded

public class TutorialButton : MonoBehaviour
{
	bool _tutorialButtonDown;

	// Update is called once per frame
	void Update ()
	{

		if (_tutorialButtonDown) 
		{
			SceneManager.LoadScene ("Tutorial");
		}
		EventSystem.current.SetSelectedGameObject(null);
	}

	public void OnTutorialButtonDown (bool down)
	{
		_tutorialButtonDown = down;
	}

}
