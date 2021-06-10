using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Class used to destroy objects that leave the border zone
public class SpaceBorder : MonoBehaviour
{
	//Used to destroy objects that leave the camera bounds and are not needed anymore
	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject);
	}
}