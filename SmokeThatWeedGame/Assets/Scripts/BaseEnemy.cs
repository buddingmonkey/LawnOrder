using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	public Sprite[] stages;
	public int movementStage = 2;
	public float startingHealth = 5;
	public float speed = 1;
	public float growthRateSec;

	float timeInStage;
	int stage;
	SpriteRenderer spriteRenderer;
	new Transform transform;

	float health;
	float direction;

	// Use this for initialization
	void Start () {
		stage = 0;
		timeInStage = 0;
		spriteRenderer = (SpriteRenderer)GetComponent<SpriteRenderer> ();
		health = startingHealth;
		transform = GetComponent<Transform> ();
		direction = 1;
	}
	
	// Update is called once per frame
	void Update () {
		timeInStage += Time.deltaTime;
		if (timeInStage > growthRateSec && stage < stages.Length-1) {
			stage++;
			timeInStage = 0;
			spriteRenderer.sprite = stages[stage];
		}

//		if (stage >= movementStage) {
			Move ();
//		}
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
		Vector2 position = new Vector2 (transform.position.x,transform.position.y);
		Vector2 moveTo = position + Vector2.right * direction * Time.deltaTime;
		RaycastHit2D hit = Physics2D.Raycast(moveTo - (Vector2.up*.5f), -Vector2.up, .5f);
		if (hit.transform != null) {
			Debug.DrawRay(hit.point,-Vector3.up*.5f,Color.red);
		}

	}
}
