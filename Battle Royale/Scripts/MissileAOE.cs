using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Master
{
	//Class used to calculate impact damage, radius and duration of missles strike
    public class MissileAOE : MonoBehaviour
    {
		
        public float maxDMG;                                 
        public float duration;                    
        public float radiusSize;
		public float radiusPush;

		public ParticleSystem particleEffect;
		public AudioSource explosionAudio;
		public LayerMask m_TankMask;   


		//Destroys game object after duration ends
        private void Start ()
        {
            Destroy (gameObject, duration);
        }

		//Calculates the amount of damage enemy tanks receive based on transform position
		private float FindDamage (Vector3 enemyPosition)
        {
            
			Vector3 distanceToTarget = enemyPosition - transform.position;
			float radiusDistance = distanceToTarget.magnitude;
			float aproxSpace = (radiusSize - radiusDistance) / radiusSize;

			float damageTotal = aproxSpace * maxDMG;
            damageTotal = Mathf.Max (0f, damageTotal);
            return damageTotal;
        }

		//Detect collision and deals damage to tanks affected
		//Damage is equal to transform position calculation from FindDamage
		//Destroys missile after impact
		private void OnTriggerEnter (Collider other)
		{

			Collider[] colliders = Physics.OverlapSphere (transform.position, radiusSize, m_TankMask);
			for (int i = 0; i < colliders.Length; i++)
			{
				Rigidbody rb = colliders[i].GetComponent<Rigidbody> ();

				if (!rb) 
				{
					continue;
				}
					
				rb.AddExplosionForce (radiusPush, transform.position, radiusSize);
				TankHealth hp = rb.GetComponent<TankHealth> ();

				if (!hp) 
				{
					continue;
				}
					
				float dmg = FindDamage (rb.position);
				hp.TakeDamage (dmg);
			}

			particleEffect.transform.parent = null;
			particleEffect.Play();
			explosionAudio.Play();
			ParticleSystem.MainModule systemMain = particleEffect.main;

			Destroy (particleEffect.gameObject, systemMain.duration);
			Destroy (gameObject);
		}

    }
}