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
	public PlayerHealth health;

	Vector2 meleeOffset;
	public void Start() {
		meleeOffset = meleeFlash.position - transform.position;
	}

	public void Melee(InputControls.ControlState state){
		if (state == InputControls.ControlState.held) return;
		isMelee = true;
		meleeTime = 0;



		// get game controller direction or player direction
		Vector2 d = new Vector2(inputControls.XAxis (), inputControls.YAxis ()).normalized;
		if (Mathf.Abs(d.y) > Mathf.Abs(d.x) && Mathf.Abs(d.y) > 0.9f) {
			// up/down
			if (d.y > 0) {
				meleeFlash.position = transform.position + new Vector3(meleeOffset.y, meleeOffset.x);
				meleeFlash.localRotation = Quaternion.AngleAxis(90, Vector3.forward);
			} else {
				meleeFlash.position = transform.position - new Vector3(meleeOffset.y, meleeOffset.x);
				meleeFlash.localRotation = Quaternion.AngleAxis(90, Vector3.forward);
			}
		} else if (Mathf.Abs(d.x) > 0) {
			meleeFlash.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
			// left/right
			if (d.x < 0 || d.x == 0 && movement.direction < 0) {
				meleeFlash.position = transform.position - new Vector3(meleeOffset.x, meleeOffset.y);
			} else {
				meleeFlash.position = transform.position + new Vector3(meleeOffset.x, meleeOffset.y);
			}
		}


		meleeFlash.gameObject.SetActive(true);
	}

	public void Shoot(InputControls.ControlState state){
		shooter.Shoot(state);
	}

	public void Drop(){
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
				health.invincible = false;
			}
		}
	}
}
