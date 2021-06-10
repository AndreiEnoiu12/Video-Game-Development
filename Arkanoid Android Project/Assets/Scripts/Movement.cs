using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{

	public Transform target;
	public 

	bool _leftButtonDown;
	bool _rightButtonDown;
	

	void Update () 
	{
		if (_rightButtonDown) 
		{
			float moveRight = (Time.deltaTime * 30);
			if (target.transform.position.x <= 7.5) 
			{
				target.Translate (moveRight, 0, 0);
				target.transform.rotation = Quaternion.identity;
			}
		}
		if (_leftButtonDown) 
		{
			float moveLeft = (Time.deltaTime * 30);
			if (target.transform.position.x >= -7.5) 
			{
				target.Translate (-moveLeft, 0, 0);
				target.transform.rotation = Quaternion.identity;
			}
		}
	}

	public void OnRightButtonDown (bool down)
	{
		_rightButtonDown = down;
	}

	public void OnLeftButtonDown (bool down)
	{
		_leftButtonDown = down;
	}
}
