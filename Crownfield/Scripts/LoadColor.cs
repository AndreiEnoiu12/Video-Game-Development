using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

//Loads the color of the player from settings
public class LoadColor : MonoBehaviour {

	public GameObject player;


	//Loads the .dat file and sets the ball color through the material rederer component 
	void Awake ()
	{
		Cursor.lockState = CursorLockMode.None;

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

		int ld = settingsData.crownSet;

		if (ld == 0) 
		{
			player.transform.GetComponent<Renderer>().material.color = Color.white;
		} 
		if (ld == 1) 
		{
			player.transform.GetComponent<Renderer>().material.color = Color.red;
		}
		if (ld == 2) 
		{
			player.transform.GetComponent<Renderer>().material.color = Color.blue;
		}
		if (ld == 3) 
		{
			player.transform.GetComponent<Renderer>().material.color = Color.green;
		}
		if (ld == 4) 
		{
			player.transform.GetComponent<Renderer>().material.color = new Color(251f/255f, 105f/255f, 180f/255f);
		}

	}
}