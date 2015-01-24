using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	public Sprite[] stages;
	public int movementStage = 2;
	public float startingHealth = 5;
	public float speed = 1;
	public float growthRateSec;
	public float gravity;
	public float terminalVelocity;

	public int colliderMask = ~0x400;

	float timeInStage;
	int stage;
	SpriteRenderer spriteRenderer;
	new RectTransform transform;
	new Rigidbody2D rigidbody;

	float health;
	float direction;
	float width;
	float halfWidth;
	float height;
	float halfHeight;

	
	// Use this for initialization
	void Start () {
		stage = 0;
		timeInStage = 0;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rigidbody = GetComponent<Rigidbody2D> ();
		health = startingHealth;
		transform = GetComponent<RectTransform> ();
		direction = -1;
		width = transform.rect.width;
		halfWidth = transform.rect.width / 2;
		height = transform.rect.width;
		halfHeight = transform.rect.width / 2;
		speed *= Random.Range (0.9f, 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		timeInStage += Time.deltaTime;
		if (timeInStage > growthRateSec && stage < stages.Length-1) {
			stage++;
			timeInStage = 0;
			spriteRenderer.sprite = stages[stage];
		}

		if (stage >= movementStage) {
			Move ();
		}

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

	public void TakeDamage(float damage) {
		health -= damage;
		if (damage < 0) {
			Die();
		}
	}

	void Die() {
		Destroy (gameObject);
	}

	public void Move() {
		rigidbody.velocity = Vector2.Lerp (rigidbody.velocity, new Vector2 (direction * speed,rigidbody.velocity.y), 0.9f);

		Vector2 position = new Vector2 (transform.position.x, transform.position.y - halfHeight);
		Vector2 moveTo = position + rigidbody.velocity * Time.deltaTime + direction*Vector2.right * halfWidth;
//		Debug.DrawRay(moveTo, -Vector3.up*.5f, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(moveTo, -Vector2.up, .5f, colliderMask);
		if (hit.collider == null) {
//			Debug.DrawRay(moveTo,-Vector3.up*.5f,Color.red);
			// hit edge, turn around
			direction = -direction;
			rigidbody.velocity = new Vector2(direction * Mathf.Abs(rigidbody.velocity.x), rigidbody.velocity.y);
		}

	}
}
