using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Starting Panel is deactivated here
public class NrPanel : MonoBehaviour {

	public GameObject panel;
	private int t = 7;

	//Start coroutine of panel time
	void Start () {
		StartCoroutine("PanelTime");
	}

	//When time reaches 0, panel is deactivated
	void Update () {
		
		if (t <= 0)
		{
			StopCoroutine("PanelTime");
			panel.SetActive(false);
		}
	}

	//Timer for panel coroutine
	IEnumerator PanelTime()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			t--;
		}
	}

}
