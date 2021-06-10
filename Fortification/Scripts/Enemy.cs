using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script that controls the enemy health, speed and bounty for the player
//Calculates damage received, updates UI and checks if health reaches 0
public class Enemy : MonoBehaviour 
{
	private GameObject gameMaster;
	private bool kill = false;

	public float health;
	private float healthRemaining;
	private float healthMax;

	public float speed;
	[HideInInspector] public float speedRemaining;

	public GameObject particlesDeath;
	public Image canvasBar;
	public int bounty;

	//When enemy spawns, calculate itś life and set it's speed
	void Start ()
	{
		gameMaster =  GameObject.FindWithTag ("GameManager");

		speedRemaining = speed;

		healthRemaining = health;
		healthRemaining = healthRemaining + (40 * (gameMaster.GetComponent<WaveController>().waveIndex2 + 1));
		healthMax = healthRemaining;
		Debug.Log(healthRemaining + "");
	}

	//Receives damage from a turret and calculates how much life was lost
	//If health reaches 0, object is killed
	public void ReceiveDamage (float dmg)
	{
		healthRemaining = healthRemaining - dmg;
		canvasBar.fillAmount = healthRemaining / healthMax;

		if (healthRemaining <= 0 && !kill)
		{
			Killed();
		}
	}

	//Receives slow from a turret and calculates how much was lost (% based)
	public void ReceiveSlow (float percent)
	{
		speedRemaining = speed * (1f - percent);
	}

	//Called when the enemy is kiled, it notifies the WaveController that enemy count decreased and plays kill effect
	//Bounty is added to total player money
	void Killed ()
	{
		kill = true;
		GameplaySettings.moneyTotal = GameplaySettings.moneyTotal + bounty;
		WaveController.enemyCount--;

		GameObject effect = (GameObject)Instantiate(particlesDeath, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		Destroy(gameObject);
	}

}
