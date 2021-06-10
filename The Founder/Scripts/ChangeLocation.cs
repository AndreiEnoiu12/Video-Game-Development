using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

//This script changes the background of the scene each time button is pressed
//Adds fade in and fade out
public class ChangeLocation : MonoBehaviour {

	bool _locButtonDown;

	public Image fadeScreen;
	public GameObject back1;
	public GameObject back2;
	public GameObject back3;
	public GameObject back4;
	public GameObject back5;
	public GameObject back6;

	public ParticleSystem rainDrops;
	public GameObject rain;
	public GameObject snow;
	public GameObject leaf1;
	public GameObject leaf2;

	private YieldInstruction fadeInstruction = new YieldInstruction();

	private int fadeTime = 1;
	private bool fadingStatus = false;
	private int pz;


	//Called when button is pressed
	//Used to check which location is currently active and make the next one active
	public void Location ()
	{
		
		if (_locButtonDown) 
		{

			if (back1.activeSelf == true && fadingStatus == false) 
			{
				fadingStatus = true;
				pz = 1;
				StartCoroutine(FadeIn(fadeScreen));
				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (back2.activeSelf == true && fadingStatus == false) 
			{
				fadingStatus = true;
				pz = 2;
				StartCoroutine(FadeIn(fadeScreen));
				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (back3.activeSelf == true && fadingStatus == false) 
			{
				fadingStatus = true;
				pz = 3;
				StartCoroutine(FadeIn(fadeScreen));
				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (back4.activeSelf == true && fadingStatus == false) 
			{
				fadingStatus = true;
				pz = 4;
				StartCoroutine(FadeIn(fadeScreen));
				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (back5.activeSelf == true && fadingStatus == false) 
			{
				fadingStatus = true;
				pz = 5;
				StartCoroutine(FadeIn(fadeScreen));
				EventSystem.current.SetSelectedGameObject(null);
			}
			else if (back6.activeSelf == true && fadingStatus == false) 
			{
				fadingStatus = true;
				pz = 6;
				StartCoroutine(FadeIn(fadeScreen));
				EventSystem.current.SetSelectedGameObject(null);
			}
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	//Fades out the image by increasing transparency in a short period of time
	IEnumerator FadeOut(Image image)
	{
		float elapsedTime = 0.0f;
		Color c = image.color;
		while (elapsedTime < fadeTime)
		{
			yield return fadeInstruction;
			elapsedTime += Time.deltaTime ;
			c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
			image.color = c;
		}
		fadingStatus = false;
	}

	//Fades in the image by decreasing transparency in a short period of time
	//Also sets backgrounds active and plays weather effects
	IEnumerator FadeIn(Image image)
	{
		float elapsedTime = 0.0f;
		Color c = image.color;
		while (elapsedTime < fadeTime)
		{
			yield return fadeInstruction;
			elapsedTime += Time.deltaTime ;
			c.a = Mathf.Clamp01(elapsedTime / fadeTime);
			image.color = c;
		}
		if (pz == 1) 
		{
			back1.gameObject.SetActive (false);
			rain.gameObject.SetActive (true);
			rainDrops.Play ();
			snow.gameObject.SetActive (false);
			leaf1.gameObject.SetActive (false);
			leaf2.gameObject.SetActive (false);
			back2.gameObject.SetActive (true);
		}
		if (pz == 2) 
		{
			back2.gameObject.SetActive (false);
			rainDrops.Stop ();
			rain.gameObject.SetActive (false);
			snow.gameObject.SetActive (true);
			leaf1.gameObject.SetActive (false);
			leaf2.gameObject.SetActive (false);
			back3.gameObject.SetActive (true);
		}
		if (pz == 3) 
		{
			back3.gameObject.SetActive (false);
			rainDrops.Stop ();
			rain.gameObject.SetActive (false);
			snow.gameObject.SetActive (true);
			leaf1.gameObject.SetActive (false);
			leaf2.gameObject.SetActive (false);
			back4.gameObject.SetActive (true);
		}
		if (pz == 4) 
		{
			back4.gameObject.SetActive (false);
			rainDrops.Stop ();
			rain.gameObject.SetActive (false);
			snow.gameObject.SetActive (false);
			leaf1.gameObject.SetActive (true);
			leaf2.gameObject.SetActive (false);
			back5.gameObject.SetActive (true);
		}
		if (pz == 5) 
		{
			back5.gameObject.SetActive (false);
			rainDrops.Stop ();
			rain.gameObject.SetActive (false);
			snow.gameObject.SetActive (false);
			leaf1.gameObject.SetActive (false);
			leaf2.gameObject.SetActive (true);
			back6.gameObject.SetActive (true);
		}
		if (pz == 6) 
		{
			back6.gameObject.SetActive (false);
			rain.gameObject.SetActive (true);
			rainDrops.Play ();
			snow.gameObject.SetActive (false);
			leaf1.gameObject.SetActive (false);
			leaf2.gameObject.SetActive (false);
			back1.gameObject.SetActive (true);
		}
		StartCoroutine(FadeOut(fadeScreen));
	}

	//Button press event
	public void OnLocButtonDown (bool down)
	{
		_locButtonDown = down;
		Location ();
	}
}