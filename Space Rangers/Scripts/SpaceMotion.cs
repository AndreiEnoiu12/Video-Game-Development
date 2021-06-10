using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Class used to create the space floating motion of objects towards the player
public class SpaceMotion : MonoBehaviour
{
	private Rigidbody rigid;

	public float pace;

	//All objects with this script move in the scene until they exit the border
	void Start ()
	{
		rigid = GetComponent<Rigidbody>();
		rigid.velocity = transform.forward * pace;
	}
}