using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EffectsDestroy : MonoBehaviour
{

	void OnTriggerEnter(Collider other) 
	{
		Destroy (gameObject);
	}
}