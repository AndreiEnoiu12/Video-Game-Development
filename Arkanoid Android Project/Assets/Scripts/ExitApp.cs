using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApp : MonoBehaviour 
{

	bool _exitAppButtonDown;


	public void Pur ()
	{

		if (_exitAppButtonDown) 
		{
			Application.Quit();
		}
	}

	public void OnExitAppButtonDown (bool down)
	{
		_exitAppButtonDown = down;
		Pur ();
	}
}
