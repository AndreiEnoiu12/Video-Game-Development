using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Effects are destroyed manually since they can't be destroyed by border
public class EffectsDestroy : MonoBehaviour
{
	public float duration;

	//Destroy the effect that has this script attached after a short duration
	void Start ()
	{
		Destroy (gameObject, duration);
	}
}