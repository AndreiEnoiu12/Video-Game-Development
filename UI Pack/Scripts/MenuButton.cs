using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//when button is pressed, player is sent back to Main Menu screen

public class MenuButton : MonoBehaviour
{
	bool _menuButtonDown;

	public GameObject mainMenu;
	public GameObject info;
	public GameObject settings;
	public GameObject levels;
	public GameObject loading;
	public GameObject menuButton;

	public GameObject crownfield1;
	public GameObject crownfield2;
	public GameObject crownfield3;
	public GameObject crownfield4;
	public GameObject spacerangers1;
	public GameObject spacerangers2;
	public GameObject spacerangers3;
	public GameObject founder1;
	public GameObject founder2;
	public GameObject founder3;
	public GameObject battleroyale1;
	public GameObject battleroyale2;
	public GameObject battleroyale3;
	public GameObject fortification1;
	public GameObject fortification2;
	public GameObject fortification3;
	public GameObject fortification4;
	public GameObject completionist1;
	public GameObject completionist2;

	public GameObject spaceSettings;
	public GameObject fortSettings;
	public GameObject funSettings;
	public GameObject compSettings;
	public GameObject crownSettings;
	public GameObject battleSettings;
	public GameObject noSettings;

	public GameObject spaceInfo;
	public GameObject fortInfo;
	public GameObject funInfo;
	public GameObject compInfo;
	public GameObject crownInfo;
	public GameObject battleInfo;

	public void Menu ()
	{

		if (_menuButtonDown) 
		{
			spacerangers1.gameObject.SetActive (false);
			spacerangers2.gameObject.SetActive (false);
			spacerangers3.gameObject.SetActive (false);
			crownfield1.gameObject.SetActive (false);
			crownfield2.gameObject.SetActive (false);
			crownfield3.gameObject.SetActive (false);
			crownfield4.gameObject.SetActive (false);
			founder1.gameObject.SetActive (false);
			founder2.gameObject.SetActive (false);
			founder3.gameObject.SetActive (false);
			battleroyale1.gameObject.SetActive (false);
			battleroyale2.gameObject.SetActive (false);
			battleroyale3.gameObject.SetActive (false);
			fortification1.gameObject.SetActive (false);
			fortification2.gameObject.SetActive (false);
			fortification3.gameObject.SetActive (false);
			fortification4.gameObject.SetActive (false);
			completionist1.gameObject.SetActive (false);
			completionist2.gameObject.SetActive (false);

			spaceSettings.gameObject.SetActive (false);
			fortSettings.gameObject.SetActive (false);
			funSettings.gameObject.SetActive (false);
			compSettings.gameObject.SetActive (false);
			crownSettings.gameObject.SetActive (false);
			battleSettings.gameObject.SetActive (false);
			noSettings.gameObject.SetActive (false);

			spaceInfo.gameObject.SetActive (false);
			fortInfo.gameObject.SetActive (false);
			funInfo.gameObject.SetActive (false);
			compInfo.gameObject.SetActive (false);
			crownInfo.gameObject.SetActive (false);
			battleInfo.gameObject.SetActive (false);

			info.gameObject.SetActive (false);
			settings.gameObject.SetActive (false);
			levels.gameObject.SetActive (false);
			loading.gameObject.SetActive (false);
			menuButton.gameObject.SetActive (false);

			mainMenu.gameObject.SetActive (true);
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public void OnMenuButtonDown (bool down)
	{
		_menuButtonDown = down;
		Menu ();
	}

}