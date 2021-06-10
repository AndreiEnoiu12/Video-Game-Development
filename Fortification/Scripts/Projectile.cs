using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//Class that keeps track of projectile damage, area, movement speed and sounds to play
//Also follows the enemy transform position
public class Projectile : MonoBehaviour 
{

	public int damage;
	public float area;
	public float movementSpeed;

	private AudioSource audioSource;
	public AudioClip audBullet;
	public AudioClip audMissile;

	public GameObject explosionFX;

	private Transform enemyTarget;

	//Audio source instance
	void Start ()
	{
		audioSource = GetComponent<AudioSource > ();
	}

	// Update direction and check distance to call Strike function
	//Kills object 2 seconds after it has dealt damage or enemy disappears
	void Update () {

		if (enemyTarget == null)
		{
			gameObject.transform.position = new Vector3(transform.position.x, 500, transform.position.z);
			StartCoroutine ("Killer");
			return;
		}

		Vector3 direction = enemyTarget.position - transform.position;
		float distance = movementSpeed * Time.deltaTime;

		if (direction.magnitude <= distance)
		{
			Strike();
			return;
		}

		transform.Translate(direction.normalized * distance, Space.World);
		transform.LookAt(enemyTarget);

	}

	//Sets target to enemy transform location
	public void Find (Transform targetLoc)
	{
		enemyTarget = targetLoc;
	}

	//Called when an enemy is hit to deal area / non-area damage and add effects
	void Strike ()
	{
		GameObject particlesFX = (GameObject)Instantiate(explosionFX, transform.position, transform.rotation);
		Destroy(particlesFX, 5f);

		StartCoroutine ("Killer");

		if (area > 0f)
		{
			audioSource.PlayOneShot (audMissile, 0.275F);
			AreaDamage();
		} else
		{
			audioSource.PlayOneShot (audBullet, 0.275F);
			SingleDamage(enemyTarget);
		}

		gameObject.transform.position = new Vector3(transform.position.x, 500, transform.position.z);
			
	}

	//counts 2 seconds before destroying the object
	IEnumerator Killer()
	{
			yield return new WaitForSeconds(2);
			Destroy(gameObject);
	}

	//Called if the damage type is only single target
	//Calls ReceiveDamage function of enemy with damage variable
	void SingleDamage (Transform enemyTarg)
	{
		Enemy enemyHit = enemyTarg.GetComponent<Enemy>();

		if (enemyHit != null)
		{
			enemyHit.ReceiveDamage(damage);
		}

	}

	//Called if the damage type is area type
	//Calls SingleDamage function for each enemy in area
	void AreaDamage ()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, area);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Enemy")
			{
				SingleDamage(collider.transform);
			}
		}

	}

	//Draw gizmo if object is selected
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, area);
	}

}
