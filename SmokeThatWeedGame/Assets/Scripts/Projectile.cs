using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public enum Path{
		linear,
		sine,
		spread,
		fall
	}

	public Path path = Path.linear;
	public float speed = 10f;
	public Vector2 direction = Vector2.right;

	public Vector2 velocity = Vector2.zero;

	public GameObject player;

	public float lifeTime = 10f;

	private Transform t;

	// Use this for initialization
	void Start () {
		t = this.transform;
		/*
		if (path == Path.fall){
			direction = new Vector2(1f, 0f);
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;

		if (lifeTime <= 0){
			Destroy(this.gameObject);
		}

		switch (path){
		case Path.fall:
			t.Translate((direction * speed * Time.deltaTime) + (velocity * Time.deltaTime));
			break;
		default:
			t.Translate((direction * speed * Time.deltaTime) + (new Vector2(velocity.x, 0) * Time.deltaTime));
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (path == Path.fall) return;
		if (other.gameObject != player){
			Destroy(this.gameObject);
		}
	}
}
