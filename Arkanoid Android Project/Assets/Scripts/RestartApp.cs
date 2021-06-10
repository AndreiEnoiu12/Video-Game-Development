using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartApp : MonoBehaviour {

	bool _restartAppButtonDown;


	public void Pur2 ()
	{

		if (_restartAppButtonDown) 
		{
			SceneManager.LoadScene("arkanoid");
		}
	}

	public void OnRestartAppButtonDown (bool down)
	{
		_restartAppButtonDown = down;
		Pur2 ();
	}
}