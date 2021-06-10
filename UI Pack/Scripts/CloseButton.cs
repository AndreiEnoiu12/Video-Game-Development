using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//when button is pressed, sub menu is closed

public class CloseButton : MonoBehaviour
{
	bool _closeButtonDown;

	public GameObject subMenu;

	public void Close ()
	{

		if (_closeButtonDown)
		{
			subMenu.gameObject.SetActive (false);
		}
		EventSystem.current.SetSelectedGameObject(null);
	}

	public void OnCloseButtonDown (bool down)
	{
		_closeButtonDown = down;
		Close ();
	}
}
