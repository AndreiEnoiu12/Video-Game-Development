using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Creates a list with all the waypoints and puts them in order
public class EnemyWaypoints : MonoBehaviour 
{

	public static Transform[] waypoints;

	//Gets every child's position of object Waypoints and adds them to list
	void Awake ()
	{
		waypoints = new Transform[transform.childCount];

		for (int i = 0; i < waypoints.Length; i++)
		{
			waypoints[i] = transform.GetChild(i);
		}
	}

}
