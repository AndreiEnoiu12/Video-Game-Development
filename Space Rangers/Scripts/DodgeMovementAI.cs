using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Script that makes the AI ships to dodge projectiles at random intervals and speeds
public class DodgeMovementAI : MonoBehaviour 
{
	public Boundary boundary;

	public float nr;
	public float dodgeSmooth;
	public float lean;

	public Vector2 startTime;
	public Vector2 dodgeTime;
	public Vector2 dodgeCD;

	private float dodgeSpeed;
	private float dodgeMove;
	private Rigidbody rigid;

	//Starts coroutine Evasion() to perform dodges
	void Start ()
	{
		rigid = GetComponent <Rigidbody> ();
		dodgeSpeed = rigid.velocity.z;
		StartCoroutine (Evasion ());
	}
		
	//This function makes the evasion happen after some starting seconds
	//While alive, calculate dodge movements at random intervals and length
	IEnumerator Evasion()
	{
		yield return new WaitForSeconds (Random.Range (startTime.x, startTime.y));

		while (true)
		{
			dodgeMove = Random.Range (1, nr) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (dodgeTime.x, dodgeTime.y));
			dodgeMove = 0;
			yield return new WaitForSeconds (Random.Range (dodgeCD.x, dodgeCD.y));
		}
	}

	//Makes sure that the dodge movement does not make the object leave the game boundary at any times
	//Changes ship position and velocity
	//Adds a leaning rotation when the ship moves left or right
	void FixedUpdate ()
	{
		float dodgeAction = Mathf.MoveTowards (rigid.velocity.x, dodgeMove, Time.deltaTime * dodgeSmooth);
		rigid.velocity = new Vector3 (dodgeAction, 0.0f, dodgeSpeed);
		rigid.position = new Vector3 
			(
				Mathf.Clamp (rigid.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp (rigid.position.z, boundary.zMin, boundary.zMax)
			);

		rigid.rotation = Quaternion.Euler (0.0f, 0.0f, rigid.velocity.x * -lean);
	}
}