using UnityEngine;
using System.Collections;

public class Holdable : MonoBehaviour {
	public bool canBeHeld = true;
	private bool beingHeld;
	public CharacterMovement holder;
	public float droppedCoolDown = 1f;
	public float dropSpeed = 10f;
	protected float coolDownTimer;
	protected WeaponSpawner mySpawner;
	public int bulletID = 0;
	private bool discarded;
	public float destructTime = 5f;
	private float destructCountdown;

	Collider2D triggerCollider;
	// Use this for initialization
	public void Start () {
		triggerCollider = gameObject.GetComponentInChildren<Collider2D> ();
	}

	public void RememberSpawner(WeaponSpawner spawner)
	{
		mySpawner = spawner;
		rigidbody2D.isKinematic = true;
	}

	// Update is called once per frame
	protected void Update () {
		if (!beingHeld) {
			if(canBeHeld)
			{
				if(discarded)
				{
					destructCountdown-=Time.deltaTime;
					if(destructCountdown<0)
					{
						Destroy (gameObject);
					}
				}

				return;
			}

			if(coolDownTimer>0)//cool down pickup time when not being held
			{
				coolDownTimer-=Time.deltaTime;
			}
			else
			{
				canBeHeld = true;
				destructCountdown = destructTime;
			}
		} else {
			if(coolDownTimer>0f)//cool down action time when being held
			{
				coolDownTimer-=Time.deltaTime;
				return;
			}
		}
	}

	public virtual void GetHeld(CharacterMovement newHolder)
	{
		if(mySpawner != null)
		{
			mySpawner.GetGrabbed(newHolder.GetComponent<Shooter>());
			mySpawner = null;
		}
		var attack = newHolder.GetComponent<PlayerAttack>();
		if (attack.holdItem != null) return;
		attack.holdItem = this;
		beingHeld = true;
		holder = newHolder;
		transform.SetParent(newHolder.transform);
		transform.localPosition = Vector3.zero - Vector3.forward;
		//if (collider != null)
		triggerCollider.enabled = false;
		collider2D.enabled = false;
		rigidbody2D.isKinematic = true;
	}
	
	public virtual void GetDropped(Vector2 impulse)
	{
		var attack = holder.GetComponent<PlayerAttack>();
		canBeHeld = false;
		coolDownTimer = droppedCoolDown;
		beingHeld = false;
		attack.holdItem = null;
		holder = null;
		transform.SetParent(null);
		rigidbody2D.velocity = impulse * dropSpeed;
		rigidbody2D.isKinematic = false;
		collider2D.enabled = true;
		triggerCollider.enabled = true;
		discarded = true;
		destructCountdown = destructTime;
	}
	
	void OnTriggerStay2D(Collider2D coll) {
		if(beingHeld || coolDownTimer>0)
		{
			return;
		}
		CharacterMovement theCharacter = coll.gameObject.GetComponent<CharacterMovement> ();
		if (theCharacter != null)
		{
			if(theCharacter.GetComponent<PlayerAttack>().holdItem == null)
			{
				Debug.Log ("grabbed by "+theCharacter.gameObject.name);
				GetHeld (theCharacter);
			}
		}
	}
}
