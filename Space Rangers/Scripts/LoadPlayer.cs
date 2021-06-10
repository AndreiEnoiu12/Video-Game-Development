using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

//This class loads the settings for which player controller is selected
public class LoadPlayer : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;

	public GameObject panel1;
	public GameObject panel2;


	//Loads settings file and hides player vehicle and panel of opposite controler
	void Awake ()
	{

		string destination = Application.persistentDataPath + "/settings.dat";
		FileStream file;

		if (File.Exists (destination))
			file = File.OpenRead (destination);
		else {
			Debug.Log ("File not found");
			return;
		}

		BinaryFormatter bf = new BinaryFormatter ();
		GameData settingsData = (GameData)bf.Deserialize (file);
		file.Close ();

		int ld = settingsData.spaceSet;

		if (ld == 0) 
		{
			player2.SetActive (false);
			panel2.SetActive (false);
		} 
		if (ld == 1) 
		{
			player1.SetActive (false);
			panel1.SetActive (false);
		}

	}
}