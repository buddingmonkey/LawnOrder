using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public Transform currentProjectile;
	public Transform[] projectiles;

	public bool autoFire = true;
	public float coolDown = .5f;
	private float currentCoolDown;

	public Transform shootLocation;

	public void Shoot(InputControls.ControlState state){
		if (currentProjectile == null || currentCoolDown > 0 || 
		    (!autoFire && state == InputControls.ControlState.held)) return;
		Transform b = (Transform)Instantiate (currentProjectile, shootLocation.position, Quaternion.identity);
		currentCoolDown = coolDown;
		b.GetComponent<Projectile>().player = this.gameObject;
		b.GetComponent<AttackHandler>().player = this.gameObject.GetComponent<CharacterMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCoolDown > 0){
			currentCoolDown -= Time.deltaTime;
		}
	}
}
