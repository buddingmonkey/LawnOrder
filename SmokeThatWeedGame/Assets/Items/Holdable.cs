using UnityEngine;
using System.Collections;

public class Holdable : MonoBehaviour {
	public bool canBeHeld = true;
	public bool beingHeld;
	public CharacterMovement holder;
	public float droppedCoolDown = 1f;
	public float dropSpeed = 10f;
	protected float coolDownTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void BaseUpdate () {
		if (!beingHeld) {
			if(canBeHeld)
			{
				return;
			}
			
			if(coolDownTimer>0)//cool down pickup time when not being held
			{
				coolDownTimer-=Time.deltaTime;
			}
			else
			{
				collider2D.enabled = true;
				canBeHeld = true;
			}
			
			return;
		}
		
		if(coolDownTimer>0f)//cool down action time when being held
		{
			coolDownTimer-=Time.deltaTime;
			return;
		}

		if(Input.GetKeyDown ("x"))
		{
			GetDropped ();
		}
	}

	public void GetHeld(CharacterMovement newHolder)
	{
		newHolder.holding = true;
		beingHeld = true;
		holder = newHolder;
		transform.parent = newHolder.transform;
		transform.localPosition = Vector3.zero - Vector3.forward;
		//if (collider != null)
		collider2D.enabled = false;
		rigidbody2D.isKinematic = true;
	}
	
	public void GetDropped()
	{
		canBeHeld = false;
		coolDownTimer = droppedCoolDown;
		beingHeld = false;
		holder.holding = false;
		holder = null;
		transform.parent = null;
		rigidbody2D.velocity = Vector2.up*dropSpeed;
		rigidbody2D.isKinematic = false;
		//collider2D.enabled = true;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if(beingHeld)
		{
			return;
		}
		CharacterMovement theCharacter = coll.gameObject.GetComponent<CharacterMovement> ();
		if (theCharacter != null)
		{
			if(!theCharacter.holding)
			{
				Debug.Log ("grabbed by "+theCharacter.gameObject.name);
				GetHeld (theCharacter);
			}
		}
	}
}
