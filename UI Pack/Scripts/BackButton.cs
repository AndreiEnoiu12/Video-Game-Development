using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//When button is pressed, the user goes back to specialization choices
public class BackButton : MonoBehaviour
{
	bool _backButtonDown;

	public GameObject subMenu;
	public GameObject spec1;
	public GameObject spec2;
	public GameObject spec3;

	public GameObject soft1;
	public GameObject soft2;
	public GameObject eco1;
	public GameObject eco2;
	public GameObject soc1;
	public GameObject soc2;
	public GameObject multi1;
	public GameObject multi2;


	public GameObject back;
	public GameObject close;

	public Text titleText;

	public void Back ()
	{

		if (_backButtonDown) 
		{
			subMenu.gameObject.SetActive (true);
			titleText.text = "Singleplayer";

			back.gameObject.SetActive (false);
			soft1.gameObject.SetActive (false);
			soft2.gameObject.SetActive (false);
			eco1.gameObject.SetActive (false);
			eco2.gameObject.SetActive (false);
			soc1.gameObject.SetActive (false);
			soc2.gameObject.SetActive (false);
			back.gameObject.SetActive (false);
			multi1.gameObject.SetActive (false);
			multi2.gameObject.SetActive (false);

			spec1.gameObject.SetActive (true);
			spec2.gameObject.SetActive (true);
			spec3.gameObject.SetActive (true);
			close.GetComponent<RectTransform>().localPosition = new Vector3 (0, close.GetComponent<RectTransform>().localPosition.y, close.GetComponent<RectTransform>().localPosition.z);
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public void OnBackButtonDown (bool down)
	{
		_backButtonDown = down;
		Back ();
	}

}