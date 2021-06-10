using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//when button is pressed, user returns to main menu screen

public class QuitTutorial : MonoBehaviour
{
	bool _quitButtonDown;

	public GameObject mainMenu;
	public GameObject tutorialPanel;

	public void Quit ()
	{

		if (_quitButtonDown)
		{
			tutorialPanel.gameObject.SetActive (false);

			mainMenu.gameObject.SetActive (true);
		}
		EventSystem.current.SetSelectedGameObject(null);
	}

	public void OnQuitButtonDown (bool down)
	{
		_quitButtonDown = down;
		Quit ();
	}
}
