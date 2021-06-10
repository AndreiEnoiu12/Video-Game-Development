using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Enemy shooting class for one laser
public class EnemyShoot : MonoBehaviour 
{

	public GameObject laserEnemy;

	public Transform laserEnemyLoc;

	public float laserCD;

	public float laserDelay;

	private AudioSource audioSource;

	//Invoke "Fire" function with a delay and cooldown
	void Start ()
	{
		
		audioSource = GetComponent<AudioSource> ();

		InvokeRepeating ("Fire", laserDelay, laserCD);

	}

	//Instantiates laser object at designated location and plays audio
	void Fire ()
	{
		
		Instantiate (laserEnemy, laserEnemyLoc.position, laserEnemyLoc.rotation);

		audioSource.Play ();

	}
}