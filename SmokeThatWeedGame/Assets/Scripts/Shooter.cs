using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public Transform currentProjectile;
	public Transform[] projectiles;

	public Transform shootLocation;

	// Use this for initialization
	void Start () {
		TESTMETHOD();
	}

	void TESTMETHOD(){
		currentProjectile = projectiles[0];
		InvokeRepeating("Shoot", .1f, .1f);
	}

	void Shoot(){
		if (currentProjectile == null) return;
		Instantiate (currentProjectile, shootLocation.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
