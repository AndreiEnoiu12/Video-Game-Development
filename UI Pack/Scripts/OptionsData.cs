using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that holds the data types for options changes
[System.Serializable]
public class OptionsData
{

	public float volume;
	public int quality;
	public bool fullscreen;


	public OptionsData(float volumeFloat, int qualityInt, bool fullscreenBool)
	{
		volume = volumeFloat;
		quality = qualityInt;
		fullscreen = fullscreenBool;
	}
}
