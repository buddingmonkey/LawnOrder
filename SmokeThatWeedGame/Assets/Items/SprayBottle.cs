using UnityEngine;
using System.Collections;

public class SprayBottle : Holdable {

	// Use this for initialization
	void Start () {
	
	}

	public override void GetHeld(CharacterMovement newHolder){
		base.GetHeld(newHolder);
		this.rigidbody2D.isKinematic = true;
		var shooter = newHolder.GetComponent<Shooter>();
		shooter.currentProjectile = shooter.projectiles[bulletID];
		shooter.coolDown = .0875f;
	}

	public override void GetDropped(Vector2 impulse){
		base.GetDropped(impulse);
		this.rigidbody2D.isKinematic = false;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}


}
