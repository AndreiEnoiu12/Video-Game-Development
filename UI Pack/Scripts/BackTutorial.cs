using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

//When button is pressed, the previous tutorial is shown in the panel
public class BackTutorial : MonoBehaviour {

	bool _backtButtonDown;

	public GameObject tut1;
	public GameObject tut2;
	public GameObject tut3;
	public GameObject tut4;
	public GameObject tut5;
	public GameObject tut6;

	public Text tutorialText;

	public void BackT ()
	{

		if (_backtButtonDown) 
		{

			if (tut1.activeSelf == true) 
			{
				tut1.SetActive (false);
				tut6.SetActive (true);

				tutorialText.text = "Tutorial - Battle Royale";

				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (tut2.activeSelf == true) 
			{
				tut2.SetActive (false);
				tut1.SetActive (true);

				tutorialText.text = "Tutorial - Space Rangers";

				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (tut3.activeSelf == true) 
			{
				tut3.SetActive (false);
				tut2.SetActive (true);

				tutorialText.text = "Tutorial - Fortification";

				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (tut4.activeSelf == true) 
			{
				tut4.SetActive (false);
				tut3.SetActive (true);

				tutorialText.text = "Tutorial - The Founder";

				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (tut5.activeSelf == true) 
			{
				tut5.SetActive (false);
				tut4.SetActive (true);

				tutorialText.text = "Tutorial - Completionist";

				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (tut6.activeSelf == true) 
			{
				tut6.SetActive (false);
				tut5.SetActive (true);

				tutorialText.text = "Tutorial - Crownfield";

				EventSystem.current.SetSelectedGameObject(null);
			}
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public void OnBacktButtonDown (bool down)
	{
		_backtButtonDown = down;
		BackT ();
	}
}