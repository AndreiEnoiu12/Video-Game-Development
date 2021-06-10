using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that holds the data types for settings changes
[System.Serializable]
public class GameData
{
	
	public int spaceSet;
	public int fortSet;
	public int funSet;
	public int compSet;
	public int crownSet;
	public int battleSet;


	public GameData(int spaceSetInt, int fortSetInt, int funSetInt, int compSetInt, int crownSetInt, int battleSetInt)
	{
		spaceSet = spaceSetInt;
		fortSet = fortSetInt;
		funSet = funSetInt;
		compSet = compSetInt;
		crownSet = crownSetInt;
		battleSet = battleSetInt;
	}
}
