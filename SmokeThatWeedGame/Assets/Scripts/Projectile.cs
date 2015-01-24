using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public enum Path{
		linear,
		sine,
		spread
	}

	public Path path = Path.linear;
	public float speed = 10f;
	public Vector2 direction = Vector2.right;

	private Transform t;

	// Use this for initialization
	void Start () {
		t = this.transform;	
	}
	
	// Update is called once per frame
	void Update () {
		t.Translate(direction * speed * Time.deltaTime);
	}
}
