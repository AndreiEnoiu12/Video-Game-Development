using UnityEngine;
using System.Collections;

//Class used to follow the player object and update every late frame

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	//Calculate position offset between player and camera at start
	void Start ()
	{
		offset = transform.position - player.transform.position;
	}

	//Updates camera position in relation to the player each frame
	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}
}
