using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Class that controls the buying, the selling and the upgrading of the turrets on that node location
//Plays audio sounds for each action
public class NodeSquare : MonoBehaviour 
{

	[HideInInspector]
	public GameObject tr;
	[HideInInspector]
	public TurretDetails trDetails;
	[HideInInspector]
	public bool trUpgraded = false;

	private Renderer rd;

	Builder buildManager;

    public Vector3 pos;

	private AudioSource audioSource;
	public AudioClip audSell;
	public AudioClip audUp;
	public AudioClip audBuy;

	private Color baseColor;
	public Color accessColor;
	public Color unavailableColor;


	//Intances of different components necessary
	void Start ()
	{
		rd = GetComponent<Renderer>();
		baseColor = rd.material.color;
		audioSource = GetComponent<AudioSource > ();
		buildManager = Builder.buildManager;
    }

	//Check if player has enough money for upgrade
	//Detroys normal turret and constructs new one (+plays sound)
	public void Upgrader ()
	{
		if (GameplaySettings.moneyTotal < trDetails.price2)
		{
			return;
		}

		GameplaySettings.moneyTotal -= trDetails.price2;
		Destroy(tr);

		GameObject newTr = (GameObject)Instantiate(trDetails.level2, BuildPos(), Quaternion.identity);
		tr = newTr;
		GameObject fx = (GameObject)Instantiate(buildManager.buyFX, BuildPos(), Quaternion.identity);
		Destroy(fx, 5f);
		trUpgraded = true;

		audioSource.PlayOneShot (audUp, 0.8F);

	}

	//Destroys current turret on that node and increases player money
	//Plays particle and sound effect
	public void Seller ()
	{
		GameplaySettings.moneyTotal += trDetails.SellCalculator();

		GameObject fx = (GameObject)Instantiate(buildManager.sellFX, BuildPos(), Quaternion.identity);
		Destroy(fx, 5f);
		Destroy(tr);
		trDetails = null;

		audioSource.PlayOneShot (audSell, 0.8F);
	}

	//Check if player has enough money for turret
	//Constructs new turret and plays sound and particle effect
	void Buyer (TurretDetails turretDetails)
	{
		if (GameplaySettings.moneyTotal < turretDetails.price1)
		{
			return;
		}

		GameplaySettings.moneyTotal -= turretDetails.price1;

		GameObject newTr = (GameObject)Instantiate(turretDetails.level1, BuildPos(), Quaternion.identity);
		tr = newTr;
		trDetails = turretDetails;
		GameObject fx = (GameObject)Instantiate(buildManager.buyFX, BuildPos(), Quaternion.identity);
		Destroy(fx, 5f);

		audioSource.PlayOneShot (audBuy, 0.8F);

	}

	//Checks if mouse enters the node region
	//If player has enough money or not, color is changed based on that
	void OnMouseEnter ()
	{
		if (EventSystem.current.IsPointerOverGameObject()) 
			return;

		if (!buildManager.checkBuild) 
			return;

		if (buildManager.checkMoney)
		{
			rd.material.color = accessColor;
		} else
		{
			rd.material.color = unavailableColor;
		}
	}

	//Change to base color when mouse exits node region
	void OnMouseExit ()
	{
		rd.material.color = baseColor;
	}

	//When node is clicked, construct a turret
	void OnMouseDown ()
	{
		if (EventSystem.current.IsPointerOverGameObject()) 
			return;

		if (tr != null)
		{
			buildManager.PickLocation(this);
			return;
		}

		if (!buildManager.checkBuild) 
			return;

		Buyer(buildManager.GetTurret());

	}

	//Set position to build on the node (Vector3)
	public Vector3 BuildPos ()
	{
		return transform.position + pos;
	}

}
