using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour {

	public float ballStartVelocity = 500f;
	private Rigidbody rb;
	private bool ballInPlay;
	bool _shootButtonDown;
	public GameObject buttonStart;

	void Awake () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	

	void Update () 
	{
		if (_shootButtonDown && ballInPlay == false) 
		{
			buttonStart.gameObject.SetActive (false);
			transform.parent = null;
			ballInPlay = true;
			rb.isKinematic = false;
			rb.AddForce(new Vector3(150F, ballStartVelocity, 0));
		}
	}

	public void OnShootButtonDown (bool down)
	{
		_shootButtonDown = down;
	}
}
