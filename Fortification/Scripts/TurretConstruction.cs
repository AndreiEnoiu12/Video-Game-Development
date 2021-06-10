using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

//Sets turret to construct based on which turret to construct
//Uses TurretDetails class values and prefabs
public class TurretConstruction : MonoBehaviour 
{
	
	public TurretDetails scanGuardTurrent;
	public TurretDetails totalAVLauncher;
	public TurretDetails zemanaBeamer;

	Builder buildManager;

	//BuildManager set
	void Start ()
	{
		buildManager = Builder.buildManager;
	}

	//Sets to build turret from buildManager
	public void ScanGuardTurret ()
	{
		buildManager.SetTurret(scanGuardTurrent);
	}

	//Sets to build launcher from buildManager
	public void TotalAVLauncher()
	{
		buildManager.SetTurret(totalAVLauncher);
	}

	//Sets to build beamer from buildManager
	public void ZemanaBeamer()
	{
		buildManager.SetTurret(zemanaBeamer);
	}

}
