using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//Updates the money UI every frame
public class Bank : MonoBehaviour 
{

	public Text moneyText;

	//Money text object is updated to current value
	void Update () 
	{
		moneyText.text = ("$" + GameplaySettings.moneyTotal.ToString());
	}
}
