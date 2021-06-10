using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script allows us to customize each type of turret and give it different settings such as range, speed and if its a laser beamer or not
//Also moves the turret head to face the target it is following
public class TurretController : MonoBehaviour 
{

	private AudioSource audioSource;
	public AudioClip audBullet;
	public AudioClip audMissile;
	public AudioClip audLaser;

	public string targetTag = "Enemy";

	public GameObject projectile;
	public float speed;
	public float range;
	public Transform aimLocation;

	private Transform transTarget;
	private Enemy enemyTarget;

	public Transform turretHead;
	public float turnRateHead ;
	private float speedCooldown = 0f;


	public bool zemanaLaser;
	public int damageLaser;
	public float slow;
	public LineRenderer laserRender;
	public Light laserLight;
	public ParticleSystem laserEffect;

	//Upon start, UpdateTarget is called 
	void Start () 
	{
		audioSource = GetComponent<AudioSource > ();
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}


	//Every frame we make sure that the function that rotates the head is called
	//Checks if itś zemana beamer to control laser light, effect and renderer
	void Update () 
	{
		if (transTarget == null)
		{
			if (zemanaLaser)
			{
				if (laserRender.enabled)
				{
					laserRender.enabled = false;
					laserEffect.Stop();
					laserLight.enabled = false;
				}
			}
			return;

		}

		LockOnTarget();
		if (zemanaLaser)
		{
			FireLaser();
		} else
		{
			if (speedCooldown <= 0f)
			{
				FireProjectile();
				speedCooldown = 1f / speed;
			}

			speedCooldown = speedCooldown - Time.deltaTime;
		}

	}

	//Finds closest enemy from an array of enemies with same tag
	//Priority is put on the closest enemy still alive that is in range of the turret
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

		float closestDistance = Mathf.Infinity;
		GameObject closestEnemy = null;

		foreach (GameObject enemy in enemies)
		{
			float rangeOfEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (rangeOfEnemy < closestDistance)
			{
				closestDistance = rangeOfEnemy;
				closestEnemy = enemy;
			}
		}

		if (range >= closestDistance && closestEnemy != null)
		{
			transTarget = closestEnemy.transform;
			enemyTarget = closestEnemy.GetComponent<Enemy>();
		} else
		{
			transTarget = null;
		}

	}

	//Locks focus on enemy target and moves the head towards his position (called every frame)
	void LockOnTarget ()
	{
		Vector3 direction = transTarget.position - transform.position;
		Quaternion dirRotate = Quaternion.LookRotation(direction);

		Vector3 rotate = Quaternion.Lerp(turretHead.rotation, dirRotate, Time.deltaTime * turnRateHead).eulerAngles;
		turretHead.rotation = Quaternion.Euler(0f, rotate.y, 0f);
	}


	//Projectiles are instantiated and play audio based on type
	//Accesses Projectile script of instance to follow target
	void FireProjectile ()
	{
		GameObject projectileInst = (GameObject)Instantiate(projectile, aimLocation.position, aimLocation.rotation);
		Projectile projectileObject = projectileInst.GetComponent<Projectile>();

		if (range == 40f) 
		{
			audioSource.PlayOneShot (audMissile, 0.275F);
		} else 
			{
				audioSource.PlayOneShot (audBullet, 0.275F);
			}

		if (projectileObject != null) 
		{
			projectileObject.Find (transTarget);
		}

	}
		
	//Beamers fire laser instead of projectile
	//Deals damage and slow and activates laser beam
	//Makes sure that the laser effect is set to the right transform position
	void FireLaser ()
	{
		enemyTarget.ReceiveDamage(damageLaser * Time.deltaTime);
		enemyTarget.ReceiveSlow(slow);

		if (!laserRender.enabled)
		{
			laserRender.enabled = true;
			laserEffect.Play();
			laserLight.enabled = true;
			audioSource.PlayOneShot (audLaser, 0.40F);
		}

		laserRender.SetPosition(0, aimLocation.position);
		laserRender.SetPosition(1, transTarget.position);

		Vector3 direct = aimLocation.position - transTarget.position;
		laserEffect.transform.position = transTarget.position + direct.normalized;
		laserEffect.transform.rotation = Quaternion.LookRotation(direct);
	}

	//Draw gizmo if object is selected
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
