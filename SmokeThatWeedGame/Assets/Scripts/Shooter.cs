using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public Transform currentProjectile;
	public Transform[] projectiles;

	public bool autoFire = true;
	public float coolDown = .5f;
	private float currentCoolDown;

	public Transform shootLocation;

	public void Shoot(){
		if (currentProjectile == null || currentCoolDown > 0) return;
		Transform b = (Transform)Instantiate (currentProjectile, shootLocation.position, Quaternion.identity);
		currentCoolDown = coolDown;
		b.GetComponent<Projectile>().player = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCoolDown > 0){
			currentCoolDown -= Time.deltaTime;
		}
	}
}
