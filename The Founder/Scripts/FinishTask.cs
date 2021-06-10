using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

//Class used to finish game tasks faster by setting the countdown timer to 0
public class FinishTask : MonoBehaviour {

	bool _finishButtonDown;

	public GameObject overLord;

	//Checks if button was pressed and if the game status is the right one
	public void Finish ()
	{

		if (_finishButtonDown && overLord.gameObject.GetComponent <Overlord> ().pl == 1) 
		{
			overLord.gameObject.GetComponent <Overlord> ().canvasTimeLeft = 0;
		}

		if (_finishButtonDown && overLord.gameObject.GetComponent <Overlord> ().pl == 6) 
		{
			overLord.gameObject.GetComponent <Overlord> ().budgetTime = 0;
		}

		if (_finishButtonDown && overLord.gameObject.GetComponent <Overlord> ().pl == 10) 
		{
			overLord.gameObject.GetComponent <Overlord> ().companyDetailsTime = 0;
		}

		EventSystem.current.SetSelectedGameObject(null);
	}

	//Button press event
	public void OnFinishButtonDown (bool down)
	{
		_finishButtonDown = down;
		Finish ();
	}
}