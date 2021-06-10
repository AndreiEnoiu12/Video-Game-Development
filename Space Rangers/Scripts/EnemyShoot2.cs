using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Enemy shooting class for two lasers
public class EnemyShoot2 : MonoBehaviour 
{

	public GameObject laserEnemy;

	public Transform[] laserEnemyLocs;

	public float laserCD;

	public float laserDelay;

	private AudioSource audioSource;

	//Invoke "Fire" function with a delay and cooldown
	void Start ()
	{
		
		audioSource = GetComponent<AudioSource> ();

		InvokeRepeating ("Fire", laserDelay, laserCD);

	}

	//Instantiates laser objects at designated location and plays audio
	void Fire ()
	{
		foreach (var shotSpawn in laserEnemyLocs)
		{
			
			Instantiate (laserEnemy, shotSpawn.position, shotSpawn.rotation);

		}

		audioSource.Play ();

	}
}