using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Class in charge of camera movement and zoom
//Has maximum and minimum values to make sure that it says in game bondaries
public class CameraMover : MonoBehaviour 
{


	public float moveRate;

	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	public float minZ;
	public float maxZ;

	public float cameraMargin;
	public float smoothingZoom;


	//Every frame we check if game is over to deactivate, if not move the camera in certain direction 
	//Stops camera from going outside bondaries by setting X, Y, Z individually
	void Update () {

		if (GameManager.gameOver)
		{
			this.enabled = false;
			return;
		}

		if (Input.GetKey("up") || Input.GetKey("w") || Input.mousePosition.y >= Screen.height - cameraMargin)
		{
            transform.Translate(Vector3.forward * moveRate * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("down") || Input.GetKey("s") || Input.mousePosition.y <= cameraMargin)
		{
			transform.Translate(Vector3.back * moveRate * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("right") || Input.GetKey("d") || Input.mousePosition.x >= Screen.width - cameraMargin)
		{
			transform.Translate(Vector3.right * moveRate * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("left") ||  Input.GetKey("a") || Input.mousePosition.x <= cameraMargin)
		{
			transform.Translate(Vector3.left * moveRate * Time.deltaTime, Space.World);
		}

		float zoom = Input.GetAxis("Mouse ScrollWheel");
		Vector3 cameraPos = transform.position;
		cameraPos.y -= zoom * 1000 * smoothingZoom * Time.deltaTime;

		cameraPos.x = Mathf.Clamp(cameraPos.x, minX, maxX);
		cameraPos.y = Mathf.Clamp(cameraPos.y, minY, maxY);
		cameraPos.z = Mathf.Clamp(cameraPos.z, minZ, maxZ);

		transform.position = cameraPos;

	}
}
