using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//when button is pressed,  changes applied to game settings will be saved to file

public class SaveButton : MonoBehaviour
{
	bool _saveButtonDown;

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

	public Transform dropDownSpace;
	public Transform dropDownFort;
	public Transform dropDownFun;
	public Transform dropDownComp;
	public Transform dropDownCrown;
	public Transform dropDownBattle;

	//applies the default values to save variables
	void Start ()
	{
		saveSpace = dropDownSpace.GetComponent<Dropdown> ().value;
		saveFort = dropDownFort.GetComponent<Dropdown> ().value;
		saveFun = dropDownFun.GetComponent<Dropdown> ().value;
		saveComp = dropDownComp.GetComponent<Dropdown> ().value;
		saveCrown = dropDownCrown.GetComponent<Dropdown> ().value;
		saveBattle = dropDownBattle.GetComponent<Dropdown> ().value;
	}

	//each frame we update variables in case any changes were made
	void Update ()
	{
		saveSpace = dropDownSpace.GetComponent<Dropdown> ().value;
		saveFort = dropDownFort.GetComponent<Dropdown> ().value;
		saveFun = dropDownFun.GetComponent<Dropdown> ().value;
		saveComp = dropDownComp.GetComponent<Dropdown> ().value;
		saveCrown = dropDownCrown.GetComponent<Dropdown> ().value;
		saveBattle = dropDownBattle.GetComponent<Dropdown> ().value;
	}

	//When button is pressed, we check if the file is already created and then we store the variables in binary format
	public void SaveData ()
	{

		if (_saveButtonDown) 
		{
			
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


			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	//Save Button checker
	public void OnSaveButtonDown (bool down)
	{
		_saveButtonDown = down;
		SaveData ();
	}

}