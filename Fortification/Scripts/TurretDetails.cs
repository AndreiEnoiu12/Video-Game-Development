using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//Each turret has its own price for buying and upgrading
[System.Serializable]
public class TurretDetails 
{

	public GameObject level1;
	public int price1;

	public GameObject level2;
	public int price2;

	//Sell money is calculated with this formula
	public int SellCalculator ()
	{
		return price1 - (price1 / 5);
	}

}
