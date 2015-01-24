using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public Transform currentProjectile;
	public Transform[] projectiles;
	public InputControls inputControls;

	public bool autoFire = true;
	public float coolDown = .5f;
	private float currentCoolDown;

	public Transform shootLocation;

	public void Shoot(InputControls.ControlState state){
		if (currentProjectile == null || currentCoolDown > 0 || 
		    (!autoFire && state == InputControls.ControlState.held)) return;

		var movement = this.gameObject.GetComponent<CharacterMovement> ();
		Vector3 location = shootLocation.position;
		Vector3 direction = Vector3.right;

		// get game controller direction or player direction
		float dx = inputControls.XAxis ();
		// left/right
		if (dx < 0 || dx == 0 && movement.direction < 0) {
			location.x = transform.position.x - (location.x - transform.position.x);
			direction = -Vector3.right;
		}

		Transform b = (Transform)Instantiate (currentProjectile, location, Quaternion.identity);
		currentCoolDown = coolDown;
		var p = b.GetComponent<Projectile>();
		p.player = this.gameObject;
		b.GetComponent<AttackHandler>().player = movement;

		p.direction = direction;
		p.velocity = movement.rigidbody2D.velocity;

	}
	
	// Update is called once per frame
	void Update () {
		if (currentCoolDown > 0){
			currentCoolDown -= Time.deltaTime;
		}
	}
}
