﻿using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public float gravity;
	public float terminalVelocity;
	public float maxSpeed;
	public float jumpSpeed;
	public float maxAcceleration;
	public float maxJumpDuration;
	public float strikeCoolDown = 1f;
	public float strikeRange = 1f;
	public int playerNum;
	public int direction = 1;
	public ParticleSystem particles;
	private InputControls input;
	private RectTransform trans;
	private float height;
	private float halfHeight;
	private float width;
	private float halfWidth;
	private float jumpTime;
	private float strikeTime;
	private Vector2 pos2D;

	const float distToGround = .08f;

	private Animator animator;

	int colliderMask = (~((1<<17)|(1<<18)));		// don't collide with weapons layer
	new Rigidbody2D rigidbody;

	enum PlayerState {
		Grounded,
		Jumping,
		Falling,
	}
	PlayerState state;

	private LayerMask playerLayer;
	private static LayerMask platformLayer;

	// Use this for initialization
	void Start () {
		input = GetComponent<InputControls>();
		trans = GetComponent<RectTransform>();
		rigidbody = GetComponent<Rigidbody2D>();
		height = trans.rect.height;
		width = .6f; //trans.rect.width;
		halfHeight = height / 2;
		halfWidth = width / 2 * 0.95f;
		state = PlayerState.Falling;

		animator = GetComponent<Animator> ();

		animator.SetInteger ("player", playerNum);
		//if (platformLayer == null)
		//{
			platformLayer = LayerMask.NameToLayer ("Platform");
		//}
		playerLayer = LayerMask.NameToLayer ("Player"+playerNum);
		gameObject.layer = playerLayer;
	}
	
	// Update is called once per frame

	void Update () {
		colliderMask &= ~(1 << (10 + playerNum));

		Physics2D.IgnoreLayerCollision (playerLayer, platformLayer, rigidbody2D.velocity.y > 0);

		pos2D = new Vector2 (trans.position.x, trans.position.y);

		pos2D = new Vector2 (trans.position.x, trans.position.y - halfHeight);

		if (state == PlayerState.Grounded)
			state = PlayerState.Falling;

		if (state != PlayerState.Jumping) {
			//Debug.DrawRay(pos2D + Vector2.right * halfWidth, -Vector3.up * distToGround, Color.green);
			RaycastHit2D hit = Physics2D.Raycast(pos2D + Vector2.right * halfWidth, -Vector2.up, distToGround, colliderMask);
			if (hit.collider == null) {
				hit = Physics2D.Raycast(pos2D - Vector2.right * halfWidth, -Vector2.up, distToGround, colliderMask);
				if(hit.collider == null)
				{//also check the middle with a raycast to see if you're on top of another player?
					hit = Physics2D.Raycast(pos2D, -Vector2.up, distToGround, colliderMask);
				}
			}
			if (hit.collider != null) {
				particles.Stop();
				state = PlayerState.Grounded;
				//Debug.DrawRay(hit.point,-Vector3.right*1f,Color.red);
				trans.position = new Vector2(trans.position.x, hit.point.y + halfHeight);
			}
		}

		if (state == PlayerState.Grounded) {
			if (input.IsJumpDown()) {
				state = PlayerState.Jumping;
				particles.Play();
				jumpTime = 0;
				rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
				SfxManager.Instance.PlaySoundAt("Jump", trans.position);
			}
		} else if (state == PlayerState.Jumping) {
			jumpTime += Time.deltaTime;
			if (jumpTime < maxJumpDuration && input.Jumping()) {
				state = PlayerState.Jumping;
				rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
			} else {
				state = PlayerState.Falling;
			}
		}

		ApplyMovement ();

		ApplyGravity ();
	}

	void ApplyGravity() {
		Vector2 v = rigidbody.velocity;
		v += -Vector2.up * gravity * Time.deltaTime;
		if (Mathf.Abs (v.y) > terminalVelocity) {
			v.y = terminalVelocity * Mathf.Sign (v.y);
		}
		rigidbody.velocity = v;
	}

	void ApplyMovement() {
		float dir = input.XAxis ();
		Vector2 v = rigidbody.velocity;
		float vx = dir * maxSpeed;
		rigidbody.velocity = Vector2.Lerp(v, new Vector2(vx, v.y), maxAcceleration);
		if (dir != 0) {
			transform.localScale = new Vector3 (Mathf.Sign (dir), 1, 1);
			direction = (int)Mathf.Sign (dir);
		}
		animator.SetFloat ("speed",Mathf.Abs (vx));
	}

}
