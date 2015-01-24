using UnityEngine;
using System.Collections;

public class SprayBottle : Holdable {
	public float sprayCoolDown = 1f;

	// Use this for initialization
	void Start () {
	
	}

	public override void GetHeld(CharacterMovement newHolder){
		base.GetHeld(newHolder);
		this.rigidbody2D.isKinematic = true;
	}

	public override void GetDropped(){
		base.GetDropped();
		this.rigidbody2D.isKinematic = false;
	}
	
	// Update is called once per frame
	void Update () {
		base.BaseUpdate ();

		Vector2 sprayDirection = Vector2.zero;
		if(Input.GetKey ("w"))
		{
			sprayDirection +=Vector2.up;
		}
		if(Input.GetKey ("s"))
		{
			sprayDirection -=Vector2.up;
		}
		if(Input.GetKey ("a"))
		{
			sprayDirection +=Vector2.right;
		}
		if(Input.GetKey ("d"))
		{
			sprayDirection +=Vector2.right;
		}

		if(sprayDirection != Vector2.zero && coolDownTimer<=0)
		{
			//fire a spray in that direction.

			coolDownTimer = sprayCoolDown;
		}


	}


}
