using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Each container has its own number
public class ContainerNr : MonoBehaviour {

	public int contNr;
	public int contType;

	//Sets container type to number
	void Start () {

		contType = contNr;
	}

}
