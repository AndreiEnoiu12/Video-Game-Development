using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//when button is pressed,  the options sub menu opens

public class OptionsButton : MonoBehaviour
{
	bool _optionsButtonDown;

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

	public GameObject optionPanel;

	public GameObject back;
	public GameObject close;

	public Text titleText;

	public void Options ()
	{

		if (_optionsButtonDown) 
		{
			subMenu.gameObject.SetActive (true);
			titleText.text = "Options";

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
			spec1.gameObject.SetActive (false);
			spec2.gameObject.SetActive (false);
			spec3.gameObject.SetActive (false);

			optionPanel.gameObject.SetActive (true);
			close.GetComponent<RectTransform>().localPosition = new Vector3 (0, close.GetComponent<RectTransform>().localPosition.y, close.GetComponent<RectTransform>().localPosition.z);
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public void OnOptionsButtonDown (bool down)
	{
		_optionsButtonDown = down;
		Options ();
	}

}