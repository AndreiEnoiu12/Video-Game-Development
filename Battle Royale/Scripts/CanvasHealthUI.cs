using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Master
{
	//Positioning for tanks UI
    public class CanvasHealthUI : MonoBehaviour
    {


        public bool correctRotator = true;       
		private Quaternion rotator;          

		//Start values for rotator based on parent's rotation
        private void Start ()
        {
            rotator = transform.parent.localRotation;
        }

		//Update rotator each frame to correct angle 
        private void Update ()
        {
			if (correctRotator) 
			{
				transform.rotation = rotator;
			}
        }
    }
}