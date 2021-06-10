using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

//Loads the music of the level from the settings file
public class LoadMusic : MonoBehaviour {

	private AudioSource audioSource;


	public AudioClip music1;
	public AudioClip music2;
	public AudioClip music3;
	public AudioClip music4;
	public AudioClip music5;


	//Load settings data file and set scene music to desired one
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

		int ld = settingsData.funSet;

		if (ld == 0) 
		{
			audioSource.clip = music1;
			audioSource.Play();
		} 
		if (ld == 1) 
		{
			audioSource.clip = music2;
			audioSource.Play();
		}
		if (ld == 2) 
		{
			audioSource.clip = music3;
			audioSource.Play();
		}
		if (ld == 3) 
		{
			audioSource.clip = music4;
			audioSource.Play();
		}
		if (ld == 4) 
		{
			audioSource.clip = music5;
			audioSource.Play();
		}

	}
}