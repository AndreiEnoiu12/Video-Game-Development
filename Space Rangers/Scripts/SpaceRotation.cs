using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Used to create the rotation of objects in space
public class SpaceRotation : MonoBehaviour
{
	private Rigidbody rigid;

	public float spin;


	//Creates a random spinning rotation of the object
	void Start ()
	{
		rigid = GetComponent<Rigidbody>();

		rigid.angularVelocity = Random.insideUnitSphere * spin; 
	}
}