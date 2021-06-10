using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

//UI buttons for each node square
//Activates seling and upgrading activities
public class NodeCanvasUI : MonoBehaviour 
{

	public Text priceSell;
	public Text priceUp;
	public Button buttonUp;

	private NodeSquare nodeSquare;

	public GameObject nodeCanvas;

	//Checks if current node has a turret and if it is upgraded already or not
	//Shows how much it costs to upgrade and sell
	public void SetNodeSquare (NodeSquare node)
	{
		nodeSquare = node;
		transform.position = nodeSquare.BuildPos();

		if (!nodeSquare.trUpgraded)
		{
			priceUp.text = "$" + nodeSquare.trDetails.price2;
			buttonUp.interactable = true;
		} else
		{
			priceUp.text = "DONE";
			buttonUp.interactable = false;
		}

		priceSell.text = "$" + nodeSquare.trDetails.SellCalculator();
		nodeCanvas.SetActive(true);

	}

	//Hides the canvas for this nude
	public void HideCanvas ()
	{
		nodeCanvas.SetActive(false);
	}

	//If button pressed, call sell function from nodeSquare script and deselect
	public void ActionSell ()
	{
		nodeSquare.Seller();
		Builder.buildManager.UnpickLocation();
	}

	//If button pressed, call upgrade function from nodeSquare script and deselect
	public void ActionUp ()
	{
		nodeSquare.Upgrader();
		Builder.buildManager.UnpickLocation();
	}

}
