using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This class moves the background space image at a tempo
public class BackgroundMover : MonoBehaviour 
{

	public float size;
	public float tempo;
	private Vector3 startPos;


	//Start position for object
	void Start () 
	{
		startPos = transform.position;
	}

	//Updates position of object by moving it at a certain tempo (with set size)
	void Update ()
	{
		float pos = Mathf.Repeat (Time.time * tempo, size);
		transform.position = startPos + Vector3.forward * pos;
	}
}