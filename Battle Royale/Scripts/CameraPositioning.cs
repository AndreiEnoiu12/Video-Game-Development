using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Master
{
	//The class makes the automatic camra movement possible when following tanks
    public class CameraPositioning : MonoBehaviour
    {
		private Camera sceneCamera;                        
		private float closeIn;

		private Vector3 motionRate;                 
		private Vector3 bestPos;
		public Transform[] targets; 

		public float reaction;
        public float edgeTarget;
        public float smallestTarget;


		//Camera component instance
        private void Awake ()
        {
            sceneCamera = GetComponentInChildren<Camera> ();
        }

		//Call the functions every fixed frame rate (frame)
        private void FixedUpdate ()
        {
            CameraMovement ();
            CameraZoom ();
        }

		//Level starts with this transform position and zoom numbers for camera
		public void InitialSetup ()
		{
			Positioning ();
			transform.position = bestPos;
			sceneCamera.orthographicSize = CameraScale ();
		}

		//Calls positioning function and creates a transition to the transform position of the camera
        private void CameraMovement ()
        {
            Positioning ();
            transform.position = Vector3.SmoothDamp(transform.position, bestPos, ref motionRate, reaction);

        }

		//Zoom by creating a size variable based on CameraScale() size returned that is used to calculate the ortographic size
		private void CameraZoom ()
		{
			float zoomSize = CameraScale();
			sceneCamera.orthographicSize = Mathf.SmoothDamp (sceneCamera.orthographicSize, zoomSize, ref closeIn, reaction);
		}

		//Checks the positioning of each tank alive based on their number
		//Best positioning is the average position calculated
        private void Positioning ()
        {
            Vector3 averagePos = new Vector3 ();
            int numTargets = 0;

            for (int i = 0; i < targets.Length; i++)
            {
				if (!targets [i].gameObject.activeSelf) 
				{
					continue;
				}

                averagePos += targets[i].position;
                numTargets++;
            }

			if (numTargets > 0) 
			{
				averagePos /= numTargets;
			}

            averagePos.y = transform.position.y;
            bestPos = averagePos;
        }

		//Find position by going through all the tanks and check which one is active or not
		//Calculate transform position of tanks to camera
        private float CameraScale ()
        {
			Vector3 wantedPosition = transform.InverseTransformPoint(bestPos);
			float a = 0f;
            
            for (int i = 0; i < targets.Length; i++)
            {
				if (!targets [i].gameObject.activeSelf) 
				{
					continue;
				}
				Vector3 originPosition = transform.InverseTransformPoint(targets[i].position);
				Vector3 objectPosition = originPosition - wantedPosition;
                
                a = Mathf.Max(a, Mathf.Abs(objectPosition.y));
                a = Mathf.Max(a, Mathf.Abs(objectPosition.x) / sceneCamera.aspect);
            }

            a += edgeTarget;
            a = Mathf.Max (a, smallestTarget);
            return a;
        }
			
    }
}