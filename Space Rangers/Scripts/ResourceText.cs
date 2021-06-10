using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class assigns text to each barrel and crate (world UI)
public class ResourceText : MonoBehaviour {

	public Text textTop;
	public Text textBottom;
	private int nr;
	[HideInInspector] public int type;

	//We assign a random number and compare it to different cases
	//Top and Bot text of object is edited
	void Start () {
		nr = Random.Range (1, 16);

		if (nr == 1) 
		{
			textTop.text = "++";
			textBottom.text = "++";
			type = 1;
		}
		if (nr == 2) 
		{
			textTop.text = "--";
			textBottom.text = "--";
			type = 1;
		}
		if (nr == 3) 
		{
			textTop.text = "%";
			textBottom.text = "%";
			type = 1;
		}


		if (nr == 4) 
		{
			textTop.text = "==";
			textBottom.text = "==";
			type = 2;
		}
		if (nr == 5) 
		{
			textTop.text = "!=";
			textBottom.text = "!=";
			type = 2;
		}
		if (nr == 6) 
		{
			textTop.text = ">";
			textBottom.text = ">";
			type = 2;
		}


		if (nr == 7) 
		{
			textTop.text = "&&";
			textBottom.text = "&&";
			type = 3;
		}
		if (nr == 8) 
		{
			textTop.text = "||";
			textBottom.text = "||";
			type = 3;
		}
		if (nr == 9) 
		{
			textTop.text = "!";
			textBottom.text = "!";
			type = 3;
		}


		if (nr == 10) 
		{
			textTop.text = "+=";
			textBottom.text = "+=";
			type = 4;
		}
		if (nr == 11) 
		{
			textTop.text = "-=";
			textBottom.text = "-=";
			type = 4;
		}
		if (nr == 12) 
		{
			textTop.text = "/=";
			textBottom.text = "/=";
			type = 4;
		}


		if (nr == 13) 
		{
			textTop.text = "~";
			textBottom.text = "~";
			type = 5;
		}
		if (nr == 14) 
		{
			textTop.text = ">>";
			textBottom.text = ">>";
			type = 5;
		}
		if (nr == 15) 
		{
			textTop.text = "<<";
			textBottom.text = "<<";
			type = 5;
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
