using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public Holdable holdItem;
	public float meleeAttackLength = 0.33f;

	private float meleeTime;
	private bool isMelee = false;

	public Transform meleeFlash;
	public CharacterMovement movement;
	public Transform front;
	public InputControls inputControls;
	public Shooter shooter;

	public void Melee(InputControls.ControlState state){
		if (state == InputControls.ControlState.held) return;
		isMelee = true;
		meleeTime = 0;
		meleeFlash.gameObject.SetActive(true);
	}

	public void Shoot(InputControls.ControlState state){
		shooter.Shoot(state);
	}

	public void Drop(){
		Rigidbody2D r = holdItem.GetComponent<Rigidbody2D>();
		var impulse = this.rigidbody2D.velocity.normalized * 3;
		holdItem.GetDropped(impulse);
	}

	public void Update (){
		var attackState = inputControls.Attack();
		if (!isMelee && attackState != InputControls.ControlState.none){
			if (holdItem == null){
				Melee(attackState);
			} else {
				Shoot(attackState);
			}
		}

		if (holdItem != null && inputControls.Throw()){
			Drop();
		}

		if (isMelee){
			meleeTime += Time.deltaTime;
			if (meleeTime >= meleeAttackLength){
				isMelee = false;
				meleeFlash.gameObject.SetActive(false);
			}
		}
	}
}
