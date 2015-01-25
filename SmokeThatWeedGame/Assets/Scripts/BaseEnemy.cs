using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	public Sprite[] stages;
	public int movementStage = 2;
	public float fullHealth = 5;
	public float saplingHealth = 2;
	public float speed = 1;
	public float growthRateSec;
	public float gravity;
	public float terminalVelocity;
	public enum Type {
		Flower,
		HedgeHog
	};
	public Type EnemyType;

	public int points = 100;

	int colliderMask = ~0x400;

	float timeInStage;
	int stage;
	SpriteRenderer spriteRenderer;
	new RectTransform transform;
	new Rigidbody2D rigidbody;

	float health;
	public float direction;
	float halfWidth;
	float halfHeight;
	float timeSinceDamaged = 0;

	private Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		stage = 0;
		timeInStage = 0;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rigidbody = GetComponent<Rigidbody2D> ();
		health = saplingHealth;
		transform = GetComponent<RectTransform> ();
		direction = -1;
		halfWidth = transform.rect.width / 2;
		halfHeight = transform.rect.width / 2;
		speed *= Random.Range (0.9f, 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		timeInStage += Time.deltaTime;
		if (timeInStage > growthRateSec && stage < stages.Length-1) {
			stage++;
			timeInStage = 0;
			//spriteRenderer.sprite = stages[stage];
			animator.SetInteger("maturity",stage);
			health += (fullHealth-saplingHealth) / movementStage;
		}
		timeSinceDamaged += Time.deltaTime;

		if (stage >= movementStage && timeSinceDamaged > 1f) {
			if (EnemyType == Type.HedgeHog) {
				HedgeMove();
			} else {
				Move ();
			}
		} else {
			if (EnemyType == Type.HedgeHog) {
				HedgeStop();
			} else {
			}
		}

		ApplyGravity ();
	}

	void FixedUpdate() {
		if (stage < movementStage) {
			rigidbody.velocity = new Vector2 (0, rigidbody.velocity.y);
		}
	}

	void ApplyGravity() {
		Vector2 v = rigidbody.velocity;
		v += -Vector2.up * gravity * Time.deltaTime;
		if (Mathf.Abs (v.y) > terminalVelocity) {
			v.y = terminalVelocity * Mathf.Sign (v.y);
		}
		rigidbody.velocity = v;
	}

	public void TakeDamage(float damage, Vector2 damageDirection, int player) {
		timeSinceDamaged = 0;
		rigidbody.AddForce (Vector2.up*3 + damageDirection * 5, ForceMode2D.Impulse);
		health -= damage;
		if (health < 0) {
			Die(player);
		}
		ParticlePool.Instance.Spawn ("PlantBlood", transform.position + Vector3.up * (halfHeight * 2 * Random.Range(0,1f)));
	}

	void Die(int player) {
		GameController.score[player] += this.points;
		EnemySpawner.currentEnemyCount--;

		Destroy (gameObject);
		string[] deathSounds = new string[3] {"PlantDeath","PlantDeath2","PlantDeath3"};
		int deathIndex = Random.Range(0, deathSounds.Length);
		SfxManager.Instance.PlaySoundAt (deathSounds[deathIndex], transform.position);
	}

	private void Move() {
		rigidbody.velocity = Vector2.Lerp (rigidbody.velocity, new Vector2 (direction * speed,rigidbody.velocity.y), 0.9f);

		Vector2 position = new Vector2 (transform.position.x, transform.position.y + halfHeight);
		Vector2 moveTo = position + rigidbody.velocity * Time.deltaTime + direction*Vector2.right * halfWidth;

		transform.localScale = new Vector3(direction, 1, 1);


//		Debug.DrawRay(moveTo, Vector2.right * direction * 0.5f, Color.green);
		// ignore hit on player
		RaycastHit2D hit = Physics2D.Raycast(moveTo, Vector2.right * direction, .25f, colliderMask & ~(0xF << 10));
		if (hit.collider != null) {
//			Debug.DrawRay(moveTo,-Vector3.up*.5f,Color.red);
			// hit edge, turn around
			direction = -direction;

			rigidbody.velocity = new Vector2(direction * Mathf.Abs(rigidbody.velocity.x), rigidbody.velocity.y);
		}

	}

	private float timeSinceStop = 1;
	private void HedgeMove() {
		if (timeSinceStop > 2.5f && Random.Range (0, 120) == 0) {
			timeSinceStop = 0;
			animator.SetBool("Walking", false);
		}
		timeSinceStop += Time.deltaTime;
		if (timeSinceStop > .2f && timeSinceStop < 1.5f) {
			animator.SetBool("Peeking", true);
			return;
		} else if (timeSinceStop <= 1) {
			return;
		}
		animator.SetBool ("Walking", true);
		animator.SetBool ("Peeking", false);
		rigidbody.velocity = Vector2.Lerp (rigidbody.velocity, new Vector2 (direction * speed,rigidbody.velocity.y), 0.9f);
		
		Vector2 position = new Vector2 (transform.position.x, transform.position.y + halfHeight);
		Vector2 moveTo = position + rigidbody.velocity * Time.deltaTime + direction*Vector2.right * halfWidth;
		
		transform.localScale = new Vector3(direction, 1, 1);
		
		
		//		Debug.DrawRay(moveTo, Vector2.right * direction * 0.5f, Color.green);
		// ignore hit on player
		RaycastHit2D hit = Physics2D.Raycast(moveTo, Vector2.right * direction, .25f, colliderMask & ~(0xF << 10));
		if (hit.collider != null) {
			//			Debug.DrawRay(moveTo,-Vector3.up*.5f,Color.red);
			// hit edge, turn around
			direction = -direction;
			
			rigidbody.velocity = new Vector2(direction * Mathf.Abs(rigidbody.velocity.x), rigidbody.velocity.y);
		}
		
	}

	private void HedgeStop() {
		animator.SetBool ("Walking", false);
		animator.SetBool ("Peeking", false);
	}


}
