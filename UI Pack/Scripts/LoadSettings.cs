using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

//This class loads the settings file and applies changes to the UI of Main Menu
public class LoadSettings : MonoBehaviour {

	public Transform dropDownSpace;
	public Transform dropDownFort;
	public Transform dropDownFun;
	public Transform dropDownComp;
	public Transform dropDownCrown;
	public Transform dropDownBattle;

	[HideInInspector]
	public int saveSpace;
	[HideInInspector]
	public int saveFort;
	[HideInInspector]
	public int saveFun;
	[HideInInspector]
	public int saveComp;
	[HideInInspector]
	public int saveCrown;
	[HideInInspector]
	public int saveBattle;


	//In this method, we load the file if it exists and change the dropdown settings to custom ones
	void Awake () 
	{

			string destination = Application.persistentDataPath + "/settings.dat";
			FileStream file;

			if(File.Exists(destination)) file = File.OpenRead(destination);
			else
			{
				Debug.Log("File not found");
				return;
			}

			BinaryFormatter bf = new BinaryFormatter();
			GameData settingsData = (GameData) bf.Deserialize(file);
			file.Close();

			dropDownSpace.GetComponent<Dropdown> ().value = settingsData.spaceSet;
			dropDownFort.GetComponent<Dropdown> ().value = settingsData.fortSet;
			dropDownFun.GetComponent<Dropdown> ().value = settingsData.funSet;
			dropDownComp.GetComponent<Dropdown> ().value = settingsData.compSet;
			dropDownCrown.GetComponent<Dropdown> ().value = settingsData.crownSet;
			dropDownBattle.GetComponent<Dropdown> ().value = settingsData.battleSet;

	}

	//When the Main Menu start (after Awake), we create or modify the settings file to be loaded later
	//Important when the game is opened for the first time
	void Start ()
	{
		saveSpace = dropDownSpace.GetComponent<Dropdown> ().value;
		saveFort = dropDownFort.GetComponent<Dropdown> ().value;
		saveFun = dropDownFun.GetComponent<Dropdown> ().value;
		saveComp = dropDownComp.GetComponent<Dropdown> ().value;
		saveCrown = dropDownCrown.GetComponent<Dropdown> ().value;
		saveBattle = dropDownBattle.GetComponent<Dropdown> ().value;

		string destination = Application.persistentDataPath + "/settings.dat";
		FileStream file;

		if (File.Exists (destination)) 
		{
			file = File.OpenWrite (destination);
		}
		else file = File.Create(destination);

		GameData settingsData = new GameData(saveSpace, saveFort, saveFun, saveComp, saveCrown, saveBattle);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, settingsData);
		file.Close();
	}

}
