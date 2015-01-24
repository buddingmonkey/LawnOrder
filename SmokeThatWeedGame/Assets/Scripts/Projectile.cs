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

	public GameObject player;

	private Transform t;

	// Use this for initialization
	void Start () {
		t = this.transform;	
	}
	
	// Update is called once per frame
	void Update () {
		switch (path){
		default:
			t.Translate(direction * speed * Time.deltaTime);
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject != player){
			Destroy(this.gameObject);
		}

	}
}
