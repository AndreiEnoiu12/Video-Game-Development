using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This class spawns waves of enemies each round and checks if there are enemies left alive to start next wave
//A random enemy type is spawned each round
public class WaveController : MonoBehaviour 
{

	public GameObject panelStart;
	public GameObject panelWin;

	public GameManager gameMaster;

	public Wave[] waves;

	public Transform startPos;

	private int nr;

	public float waitTime;

	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;

	private GameObject enemy;

	public Text roundText;

	private int waveIndex = 0;
	public static int enemyCount = 0;

	[HideInInspector] public int waveIndex2;
	private float waitTime2 = 10f;

	//Check if enemies are still alive each frame, also check if last round has been reached
	//Starts coroutine EnemyWave when the waiting time between rounds is over
	void Update ()
	{
		waveIndex2 = waveIndex;

		if (enemyCount > 0)
		{
			return;
		}

		if (waveIndex == waves.Length)
		{
			
			if (GameManager.gameOver == false) 
			{
				panelWin.SetActive (true);
			}

			gameMaster.WinCondition();
			this.enabled = false;

		}

		if (waitTime2 <= 0f)
		{
			StartCoroutine("EnemyWave");
			waitTime2 = waitTime;
			return;
		}

		waitTime2 -= Time.deltaTime;
		waitTime2 = Mathf.Clamp(waitTime2, 0f, Mathf.Infinity);
	}

	//Start values are assigned and random wave is selected for first wave
	void Start ()
	{
		
		enemyCount = 0;
		waitTime2 = 10f;
		waveIndex2 = 0;

		nr = Random.Range (1, 4);
		if(nr == 1) 
		{
			enemy = enemy1;
			roundText.text = ("Round " + (waveIndex + 1) + " - " + "Spyware");
		}
		if(nr == 2) 
		{
			enemy = enemy2;
			roundText.text = ("Round " + (waveIndex + 1) + " - " + "Malware");
		}
		if(nr == 3) 
		{
			enemy = enemy3;
			roundText.text = ("Round " + (waveIndex + 1) + " - " + "Adware");
		}

		StartCoroutine ("PanelMain");

	}

	//Destorys the start panel after 10 seconds
	IEnumerator PanelMain ()
	{
		yield return new WaitForSeconds(10);
		Destroy (panelStart);
		StopCoroutine("PanelMain");
	}

	//Each time couroutine is started, we make a for loop to instantiate enemies at wave.rate interval for a number of wave.count times
	//Also checks if game is over, waves are 18 / 19 (boss) in order to avoid some bugs
	IEnumerator EnemyWave ()
	{
		
		if (GameManager.gameOver == false) 
		{
			

			Wave wave = waves [waveIndex];

			if (waveIndex == 18 || waveIndex == 19) 
			{
				enemy = enemy4;
			}

			enemyCount = wave.count;

			for (int i = 0; i < wave.count; i++) 
			{
				InstantiateEnemy (enemy);
				yield return new WaitForSeconds (1f / wave.rate);
			}

			waveIndex++;

			if (waveIndex <= 19) 
			{
				nr = Random.Range (1, 4);
				if (nr == 1) {
					enemy = enemy1;
					roundText.text = ("Round " + (waveIndex + 1) + " - " + "Spyware");
				}
				if (nr == 2) {
					enemy = enemy2;
					roundText.text = ("Round " + (waveIndex + 1) + " - " + "Malware");
				}
				if (nr == 3) {
					enemy = enemy3;
					roundText.text = ("Round " + (waveIndex + 1) + " - " + "Adware");
				}
				if (waveIndex == 18 || waveIndex == 19) {
					roundText.text = ("Round " + (waveIndex + 1) + " - " + "Worm");
				}
			}
		}
	}

	//Each time it is called, an object of type enemy is intantiated
	void InstantiateEnemy (GameObject enemy)
	{
		Instantiate(enemy, startPos.position, startPos.rotation);
	}

}
