using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class in charge of swapping the card back with card front
//Image sprite is changed when phase starts and all cards are randomized
//Detects when a mouse click happens and "turns" the card around
public class CardFrontal : MonoBehaviour 
{


	public GameObject cardBack;
	public LevelController levelController;

	private AudioSource audioSource;
	public AudioClip audFlip;

	public int id
	{
		get { return _id; }
	}

	private int _id;

	//Audio Source for flip
	void Start ()
	{
		audioSource = GetComponent<AudioSource > ();
	}

	//When mouse click, reveal card front and play sound
	public void OnMouseDown()
	{
		if(cardBack.activeSelf && levelController.checkReveal)
		{
			cardBack.SetActive(false);
			levelController.Reveal(this);
			audioSource.PlayOneShot (audFlip, 1.5F);
		}
	}

	//Unreveal card front by setting card back active
	public void Unturn()
	{
		cardBack.SetActive(true);
	}

	//Change sprite image to random one from array
	public void ChangeImage(int id, Sprite img)
	{
		_id = id;
		GetComponent<SpriteRenderer>().sprite = img;
	}


}