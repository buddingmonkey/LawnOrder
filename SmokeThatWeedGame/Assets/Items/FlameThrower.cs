using UnityEngine;
using System.Collections;

public class FlameThrower : Holdable {

	// Use this for initialization
	void Start () {
	}
	
	public override void GetHeld(CharacterMovement newHolder){
		base.GetHeld(newHolder);
		this.rigidbody2D.isKinematic = true;
		var shooter = newHolder.GetComponent<Shooter>();
		shooter.currentProjectile = shooter.projectiles[bulletID];
		shooter.coolDown = .0167f; // 1FRAME!
	}
	
	public override void GetDropped(Vector2 impulse){
		base.GetDropped(impulse);
		this.rigidbody2D.isKinematic = false;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
