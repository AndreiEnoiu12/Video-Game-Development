using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

//Class that loads the player settings regarding music
public class LoadSoundtrack : MonoBehaviour 
{

	private AudioSource audioSource;


	public AudioClip musical1;
	public AudioClip musical2;


	// Load settings from .dat file and allows mouse movement
	void Awake ()
	{

		Cursor.lockState = CursorLockMode.None;

		audioSource = GetComponent<AudioSource> ();

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

		int ld = settingsData.compSet;

		if (ld == 0) 
		{
			audioSource.clip = musical1;
			audioSource.Play();
		} 
		if (ld == 1) 
		{
			audioSource.clip = musical2;
			audioSource.Play();
		}

	}
}