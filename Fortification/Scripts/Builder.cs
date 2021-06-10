using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Class that holds references to buying and selling particle effects, and UI
//Can check turret selected, if player has enough money to build
//Set and Get methods for turrets
public class Builder : MonoBehaviour 
{

	public GameObject buyFX;
	public GameObject sellFX;

	private TurretDetails turretSelected;
	private NodeSquare squareSelected;

	public static Builder buildManager;

	public NodeCanvasUI nodeButtons;

	public bool checkBuild 
	{ 
		get 
		{ 
			return turretSelected != null; 
		} 
	}

	public bool checkMoney 
	{ 
		get 
		{ 
			return GameplaySettings.moneyTotal >= turretSelected.price1; 
		} 
	}

	//Check of buildManager exists (rare issue)
	void Awake ()
	{
		if (buildManager != null)
		{
			
			Debug.LogError("Error!");
			return;

		}

		buildManager = this;
	}

	//Return turret selected when called
	public TurretDetails GetTurret ()
	{
		return turretSelected;
	}

	//Set turret and call unPickLocation function to hide UI
	public void SetTurret (TurretDetails turret)
	{
		turretSelected = turret;
		UnpickLocation();
	}

	//Sets the UI canvas for the node square selected
	//Unpicks if it's same node
	public void PickLocation (NodeSquare node)
	{
		if (squareSelected == node)
		{
			UnpickLocation();
			return;
		}

		squareSelected = node;
		turretSelected = null;

		nodeButtons.SetNodeSquare(node);
	}

	//When location is unpicked, the quare selected state and it's UI are deactivated
	public void UnpickLocation()
	{
		squareSelected = null;
		nodeButtons.HideCanvas();
	}

}
