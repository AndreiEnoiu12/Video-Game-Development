using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//Clas that updates the UI text for player health
public class HealthUI : MonoBehaviour 
{

	public Text livesText;

	//Updates health and check if it reaches 0 so it stops going negative
	void Update () 
	{
		
		if (GameplaySettings.healthTotal <= 0) 
		{
			livesText.text = (0 + "");
		} else {
			livesText.text = (GameplaySettings.healthTotal.ToString() + "");
		}

	}
}
