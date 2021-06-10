using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Class in charge of controlling game logic and cards creation each phase
//Spawns cards in matrix order and gives them random formula body / formula name
//Keeps track of timers and score
public class LevelController : MonoBehaviour 
{


	public CardFrontal cardFrontal;
	public CardFrontal cardFrontal2;
	public CardFrontal cardFrontal3;

	public Sprite[] images;

	public GameObject startPanel;
	public Text startText;

	public Text phaseText;
	public GameObject winPanel;
	public GameObject losePanel;
	public Text winText;
	public Text loseText;

	private CardFrontal firstTurn;

	private CardFrontal secondTurn;

	private int playerScore = 0;
	private int targetScore = 12;

	public Text playerScoreText;
	public Text targetScoreText;

	public const float posZ = 10f;
	public const float posY = 12f;

	public const int rows = 3;
	public const int cols = 8;

	private int pl;

	public bool checkReveal
	{
		get { return secondTurn == null; }
	}

	private int phaseTime1 = 10;
	private int phaseTime2 = 10;
	private int phaseTime3 = 10;
	private int phaseTime4 = 5;

	public GameObject phaseCards1;
	public GameObject phaseCards2;
	public GameObject phaseCards3;

	public Text timerText;
	private float time = 1200;

	public Text countdownText;
	public float time2 = 330;

	private bool gameOver = false; 

	private AudioSource audioSource;
	public AudioClip audWin;
	public AudioClip audLose;
	public AudioClip audYesMatch;
	public AudioClip audNoMatch;
	public AudioClip audFlip;

	//When level starts, start initial pause timer and instantiate all cards for first phase
	//Cards are instantiated at fixed position in order 8 by 3
	private void Start()
	{
		pl = 0;
		gameOver = false;
		StartCoroutine ("PhaseTimer1");
		startText.text = ("Please wait: " + phaseTime1 + " seconds");
		targetScoreText.text = ("" + targetScore);
		audioSource = GetComponent<AudioSource > ();

		Vector3 startPos = cardFrontal.transform.position;

		int[] num = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23};
		num = Randomizer(num);

		for(int i = 0; i < cols; i++)
		{
			for(int j = 0; j < rows; j++)
			{
				CardFrontal card;
				if(i == 0 && j == 0)
				{
					card = cardFrontal;
				}
				else
				{
					card = Instantiate(cardFrontal) as CardFrontal;
					card.transform.parent = phaseCards1.transform; 
				}

				int index = j * cols + i;

				int id = num[index];

				card.ChangeImage(id, images[id]);

				float Z = (posZ * i) + startPos.z;
				float Y = (posY * j) + startPos.y;

				card.transform.position = new Vector3(startPos.x, Y, Z);
			}
		}
		phaseCards1.SetActive (false);
	}
		
	//Update end-game text and checks if finished timers and game states have reached certain point
	void Update()
	{
		winText.text = "Play Time: " + timerText.text;
		loseText.text = "Play Time: " + timerText.text;

		if (playerScore == targetScore) 
		{
			if (phaseTime1 == 0 && pl == 1) 
			{
				pl = 2;
				StartCoroutine ("PhaseTimer2");
			}
			if (phaseTime2 == 0 && pl == 3) 
			{
				pl = 6;
				gameOver = true;
				StartCoroutine ("PhaseTimer4");
			}
			if (phaseTime3 == 0 && pl == 5) 
			{
				pl = 6;
				gameOver = true;
				StartCoroutine ("PhaseTimer4");
			}

		}

		if (phaseTime1 == 0 && pl == 0) 
		{
			pl = 1;
			StopCoroutine ("PhaseTimer1");
			startPanel.SetActive (false);
			phaseCards1.SetActive (true);
			TotalTimer();
			CountdownTimer();
		}
		if (phaseTime2 == 0 && pl == 2) 
		{
			pl = 3;
			StopCoroutine ("PhaseTimer2");
			startPanel.SetActive (false);
			phaseCards2.SetActive (true);
		}
		if (phaseTime3 == 0 && pl == 4) 
		{
			pl = 5;
			StopCoroutine ("PhaseTimer3");
			startPanel.SetActive (false);
			phaseCards3.SetActive (true);
		}
		if (phaseTime4 == 0 && pl == 6) 
		{
			pl = 7;
			StopCoroutine ("PhaseTimer4");
			phaseCards3.SetActive (false);
			phaseCards2.SetActive (false);
			audioSource.PlayOneShot (audWin, 1.25F);
			winPanel.SetActive (true);
		}

	}

	//Randomize the array so that each time we have a different order for cards
	//Only ne array is returned
	private int[] Randomizer(int[] numbers)
	{
		int[] copyArray = numbers.Clone() as int[];

		for(int i = 0; i < copyArray.Length; i++)
		{
			int x = copyArray[i];

			int nr = Random.Range(i, copyArray.Length);

			copyArray[i] = copyArray[nr];
			copyArray[nr] = x;
		}

		return copyArray;
	}
		
	//Checks if card revealed is the first or the second
	//If second, check if match
	public void Reveal(CardFrontal card)
	{
		if(firstTurn == null)
		{
			firstTurn = card;
		}
		else
		{
			secondTurn = card;
			StartCoroutine(CheckMatch());
		}
	}

	//Check if formula body and formula name match (positioned at nr * 2 distance in the array to make checking formula easier)
	//Plays sounds when match is found or not
	private IEnumerator CheckMatch()
	{
		if((firstTurn.id == secondTurn.id + (images.Length/2)) || secondTurn.id == (firstTurn.id + (images.Length/2)))
		{
			time2 = time2 + 10;
			playerScore++;
			playerScoreText.text = ("" + playerScore);
			audioSource.PlayOneShot (audYesMatch, 0.99F);
		}
		else
		{
			//audioSource.PlayOneShot (audNoMatch, 1.0F);
			yield return new WaitForSeconds(1f);

			firstTurn.Unturn();
			secondTurn.Unturn();
			audioSource.PlayOneShot (audFlip, 1.5F);
		}

		firstTurn = null;
		secondTurn = null;

	}

	//Randomization for cards in phase 2 (same as start one)
	public void Phase2Cards ()
	{
		Vector3 startPos = cardFrontal2.transform.position;

		int[] num = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23};
		num = Randomizer(num);

		for(int i = 0; i < cols; i++)
		{
			for(int j = 0; j < rows; j++)
			{
				CardFrontal card;
				if(i == 0 && j == 0)
				{
					card = cardFrontal2;
				}
				else
				{
					card = Instantiate(cardFrontal2) as CardFrontal;
					card.transform.parent = phaseCards2.transform; 
				}

				int index = j * cols + i;

				int id = num[index];

				card.ChangeImage(id, images[id]);

				float Z = (posZ * i) + startPos.z;
				float Y = (posY * j) + startPos.y;

				card.transform.position = new Vector3(startPos.x, Y, Z);
			}
		}
	}

	//Randomization for cards in phase 3 (same as start one)
	public void Phase3Cards ()
	{
		Vector3 startPos = cardFrontal3.transform.position;

		int[] num = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23};
		num = Randomizer(num);

		for(int i = 0; i < cols; i++)
		{
			for(int j = 0; j < rows; j++)
			{
				CardFrontal card;
				if(i == 0 && j == 0)
				{
					card = cardFrontal3;
				}
				else
				{
					card = Instantiate(cardFrontal3) as CardFrontal;
					card.transform.parent = phaseCards3.transform; 
				}

				int index = j * cols + i;

				int id = num[index];

				card.ChangeImage(id, images[id]);

				float Z = (posZ * i) + startPos.z;
				float Y = (posY * j) + startPos.y;

				card.transform.position = new Vector3(startPos.x, Y, Z);
			}
		}
	}

	//Timer for phase 1 loading
	IEnumerator PhaseTimer1()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			phaseTime1--;
			startText.text = ("Please wait: " + phaseTime1 + " seconds");
		}
	}

	//Timer for phase 2 loading
	IEnumerator PhaseTimer2()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			phaseTime2--;

			if (phaseTime2 == 5) 
			{
				playerScore = 0;
				playerScoreText.text = ("" + playerScore);
				startPanel.SetActive (true);
				phaseCards1.SetActive (false);
				Phase2Cards ();
				phaseText.text = ("Game Phase status: 2 / 2");
			}

			startText.text = ("Please wait: " + phaseTime2 + " seconds");
		}
	}

	//Timer for phase 3 loading
	IEnumerator PhaseTimer3()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			phaseTime3--;

			if (phaseTime3 == 5) 
			{
				playerScore = 0;
				playerScoreText.text = ("" + playerScore);
				startPanel.SetActive (true);
				phaseCards2.SetActive (false);
				Phase3Cards ();
				phaseText.text = ("Game Phase status: 3 / 3");
			}

			startText.text = ("Please wait: " + phaseTime3 + " seconds");
		}
	}

	//Timer for phase 4 loading
	IEnumerator PhaseTimer4()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			phaseTime4--;
		}
	}

	//Used to start the player timer every level 
	void TotalTimer()
	{
		if (timerText != null)
		{
			time = 0;
			timerText.text = (0 + ":" + 00);
			InvokeRepeating("UpdateTotalTimer", 0.0f, 0.01667f);
		}
	}

	//Updated the player timer each second based on minutes and seconds formula
	void UpdateTotalTimer()
	{
		if (pl != 6 || pl != 7)
		{
			if (gameOver == false) 
			{
				time += Time.deltaTime;
				string minutes = Mathf.Floor (time / 60).ToString ("00");
				string seconds = (time % 60).ToString ("00");

				if (seconds != "60") {
					timerText.text = (minutes + ":" + seconds);
				}
			}
		}
	}

	//Used to start the countdown timer every level
	void CountdownTimer()
	{
		if (countdownText != null)
		{
			string minutes2 = Mathf.Floor(time2 / 60).ToString("00");
			string seconds2 = (time2 % 60).ToString("00");

			countdownText.text = (minutes2 + ":" + seconds2);
			InvokeRepeating("UpdateTotalTimer2", 0.0f, 0.01667f);
		}
	}

	//Updates the countdown timer and checks if it reaches 0
	//Throws game over and stops the other coroutines to signal that the level is done
	void UpdateTotalTimer2()
	{
		if (pl != 6 || pl != 7)
		{
			if (gameOver == false) 
			{
				time2 -= Time.deltaTime;
				string minutes2 = Mathf.Floor (time2 / 60).ToString ("00");
				string seconds2 = (time2 % 60).ToString ("00");

				if (minutes2 == "00" && seconds2 == "00") {
					countdownText.text = ("00:00");
					gameOver = true;
					StopCoroutine ("PhaseTimer1");
					StopCoroutine ("PhaseTimer2");
					StopCoroutine ("PhaseTimer3");
					StopCoroutine ("PhaseTimer4");
					startPanel.SetActive(false);
					phaseCards1.SetActive(false);
					phaseCards2.SetActive(false);
					phaseCards3.SetActive(false);
					winPanel.SetActive (false);
					audioSource.PlayOneShot (audLose, 0.95F);
					losePanel.SetActive(true);
				} else {
					if (seconds2 != "60") {
						countdownText.text = (minutes2 + ":" + seconds2);
					}
				}
			}
		}
	}

}
