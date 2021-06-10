using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

//CLass that sets the player stats (money and health)
public class GameplaySettings : MonoBehaviour 
{

	public int startMoney;
	private int startHealth;

	public static int healthTotal;
	public static int moneyTotal;

	//Load the starting health from settings data file and allow cursor movement
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

		int ld = settingsData.fortSet;

		if (ld == 0) 
		{
			startHealth = 100;
		} 
		if (ld == 1) 
		{
			startHealth = 75;
		}
		if (ld == 2) 
		{
			startHealth = 50;
		}
		if (ld == 3) 
		{
			startHealth = 25;
		}

	}

	//Set start health and money
	void Start ()
	{
		moneyTotal = startMoney;
		healthTotal = startHealth;
	}

}
