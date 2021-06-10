using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Master
{
//Controls the waypoints for enemy Tanks patrol mechanic
public class PatrolController : MonoBehaviour 
{

	public Transform nextLocation;

	private List<Transform> copyList = new List<Transform>();
	public int posNr;

	public float speed;
	private bool booler = false;

	public List<Transform> wayPointsList = new List<Transform>();

	//At start we make a copy of the list to reset if tank dies
	void Start () 
	{
		copyList.AddRange(wayPointsList);
		nextLocation = wayPointsList [posNr];

		StartCoroutine ("StartTimer");
	}

	//Clears orignal list and replaces with copy list starting from index 1
	public void PositionOrigin ()
	{
		wayPointsList.Clear();
		wayPointsList.AddRange(copyList);
		nextLocation = wayPointsList [1];
	}
		
	//Tank starts moving only after starting time ends
	void FixedUpdate () 
	{
		if (booler == true) 
		{
			StopCoroutine ("StartTimer");	
			PatrolAIMover ();
		}
	}

	//Moves the AI tank from point to point on the scene
	//Adjusts rotation every fixed update
	public void PatrolAIMover()
	{
		float currentDistance = Vector2.Distance (transform.position, nextLocation.position);

		if (currentDistance == 0) 
		{
			if (posNr != wayPointsList.Count - 1) 
			{
				posNr++;
				nextLocation = wayPointsList [posNr];

			} else 
			{
				posNr = 1;
				nextLocation = wayPointsList [posNr];
			}
		}

		Vector3 rotatorPos = nextLocation.position - transform.position;
		transform.rotation = Quaternion.LookRotation (rotatorPos);
		transform.position = Vector3.MoveTowards (transform.position, nextLocation.position, speed * Time.deltaTime);
	}

	//Start timer counter
	IEnumerator StartTimer()
	{
		while (true)
		{
			yield return new WaitForSeconds(5);
			booler = true;
		}
	}
}
}