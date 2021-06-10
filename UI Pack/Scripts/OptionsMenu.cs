using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//This class is used to load options data from file and modify the game graphics, sound and screen resolution
//It also used to set the functionality for the buttons of each option
public class OptionsMenu : MonoBehaviour {

	Resolution[] resolutions;

	public Dropdown resolutionDropdown;

	[HideInInspector]
	public float saveVolume;
	[HideInInspector]
	public int saveQuality;
	[HideInInspector]
	public bool saveFullscreen;

	public Slider soundSlider;
	public Dropdown graphicsDropdown;
	public Toggle fullscreenToggle;



	//We load the file and apply option changes to UI and game
	//If no file is found, we create one
	void Awake () 
	{

		string destination = Application.persistentDataPath + "/options.dat";
		FileStream file;

		if(File.Exists(destination)) file = File.OpenRead(destination);
		else
		{
			Debug.Log("File not found");
			Save ();
			AudioListener.volume = soundSlider.GetComponent<Slider> ().value ;
			Debug.Log("File created");
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		OptionsData optionsData = (OptionsData) bf.Deserialize(file);
		file.Close();

		soundSlider.GetComponent<Slider> ().value = optionsData.volume;
		graphicsDropdown.GetComponent<Dropdown> ().value = optionsData.quality;
		fullscreenToggle.GetComponent<Toggle> ().isOn = optionsData.fullscreen;

		AudioListener.volume = soundSlider.GetComponent<Slider> ().value ;

	}

	//Adjust volume for all scenes and saves changes
	public void AdjustVolume (float newVolume) 
	{
	    AudioListener.volume = newVolume;
		Save ();
	}

	//Adjusts screen resolution dropdown to show resolutions compatible with the monitor
	//Sets a standard resolution that is equal to the current resolution when starting the game for the first time
	void Start ()
	{
		resolutions = Screen.resolutions;

		resolutionDropdown.ClearOptions();

		List<string> options = new List<string> ();

		int currentResolutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate +"Hz";
			options.Add(option);

			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate) 
			{
				currentResolutionIndex = i;
			}
		}

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue ();

	}
	//Applies resolution selected to game screen
	public void setResolution (int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
	}

	//Applies graphics and quality changes for all scenes and saves changes
	public void setQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel (qualityIndex);
		Save ();
	}

	//Applies fullscreen toggle changes for all scenes and saves changes
	public void setFullscreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
		Save ();
	}

	//We check if the options file already exists, and then we serialize the variables into binary data to store
	public void Save ()
	{
		saveVolume = soundSlider.GetComponent<Slider> ().value;
		saveQuality = graphicsDropdown.GetComponent<Dropdown> ().value;
		saveFullscreen = fullscreenToggle.GetComponent<Toggle> ().isOn;

		string destination = Application.persistentDataPath + "/options.dat";
		FileStream file;

		if (File.Exists (destination)) 
		{
			file = File.OpenWrite (destination);
		}
		else file = File.Create(destination);

		OptionsData optionsData = new OptionsData(saveVolume, saveQuality, saveFullscreen);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, optionsData);
		file.Close();
	}

}
