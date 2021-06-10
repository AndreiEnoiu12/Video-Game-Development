using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Class that makes possible the movement of enemies from waypoint to waypoint
//When reaches the end, destroys it
[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour 
{
	private Enemy enemyStats;
	private int waypointNr = 0;
	private Transform waypointTarget;

	//Sets waypoint to 0 and stats
	void Start()
	{
		enemyStats = GetComponent<Enemy>();
		waypointTarget = EnemyWaypoints.waypoints[0];
	}

	//Each frame the object moves closer and closer to the waypoint transform pos at enemy speed stats
	void Update()
	{
		Vector3 mover = waypointTarget.position - transform.position;
		transform.Translate(mover.normalized * enemyStats.speedRemaining * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, waypointTarget.position) <= 0.4f)
		{
			MoveNext();
		}

		enemyStats.speedRemaining = enemyStats.speed;

	}

	//Checks if enemy has reached final destination, if not, go to next waypoint
	void MoveNext()
	{
		if (EnemyWaypoints.waypoints.Length - 1 <= waypointNr)
		{
			Finish();
			return;
		}

		waypointNr++;
		waypointTarget = EnemyWaypoints.waypoints[waypointNr];
	}

	//When object reaches the finish, enemy is destroyed
	//Player loses life when this is called
	void Finish()
	{
		
		GameplaySettings.healthTotal--;
		WaveController.enemyCount--;
		Destroy(gameObject);

	}

}
