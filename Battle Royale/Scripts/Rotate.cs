using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used to rotate food items on the map (no treasures)
public class Rotate : MonoBehaviour {

	public int calories;
	public bool rot;
	
	//Rotate object every frame
	void Update () 
	{
		if (rot == true) 
		{
			transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
		}
	}
}
