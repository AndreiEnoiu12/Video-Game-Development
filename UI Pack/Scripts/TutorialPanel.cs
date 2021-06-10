using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//when button is pressed,  tutorial menu panel is activated

public class TutorialPanel: MonoBehaviour
{
	bool _tutButtonDown;

	public GameObject mainMenu;
	public GameObject subMenu;

	public GameObject tutorialPanel;


	public void Tut ()
	{

		if (_tutButtonDown) 
		{
			mainMenu.gameObject.SetActive (false);
			subMenu.gameObject.SetActive (false);

			tutorialPanel.gameObject.SetActive (true);
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public void OnTutButtonDown (bool down)
	{
		_tutButtonDown = down;
		Tut ();
	}

}