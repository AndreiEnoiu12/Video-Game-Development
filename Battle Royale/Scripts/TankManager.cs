using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Master
{
	//This tank instances all the player tank scripts
	//Sets player number, color and spawn location
    [Serializable]
    public class TankManager
    {

        [HideInInspector] public int playerNr;            
        [HideInInspector] public string textColor;    
        [HideInInspector] public GameObject self;         

		public Color playerColor;  

		private TankMovement moveComponent;                        
		private TankShooting shootComponent;                        
		private GameObject canvasComponent;
		                           
		public Transform spawnLoc;

		//Creates instances of movement shooting and UI
		//Colors the tank by changing its material render color
        public void Setup ()
        {
            
            moveComponent = self.GetComponent<TankMovement> ();
            shootComponent = self.GetComponent<TankShooting> ();
            canvasComponent = self.GetComponentInChildren<Canvas> ().gameObject;

            moveComponent.playerNr = playerNr;
            shootComponent.playerNr = playerNr;
            
            textColor = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + playerNr + "</color>";
            MeshRenderer[] renderers = self.GetComponentsInChildren<MeshRenderer> ();
		
            for (int i = 0; i < renderers.Length; i++)
            { 
                renderers[i].material.color = playerColor;
            }

        }

		//Tank is respawned and set active to starting position
		public void Respawner ()
		{
			self.transform.position = spawnLoc.position;
			self.transform.rotation = spawnLoc.rotation;
			self.SetActive (false);
			self.SetActive (true);
		}


		//When called, activates tank control for that player
		public void ActivateCommands ()
		{
			canvasComponent.SetActive (true);
			moveComponent.enabled = true;
			shootComponent.enabled = true;
		}

		//When called, deactivates tank control for that player
        public void DeactivateCommands ()
        {
			canvasComponent.SetActive (false);
            moveComponent.enabled = false;
            shootComponent.enabled = false;
        }

    }
}