using UnityEngine;
using System.Collections;

public class WorldBound : MonoBehaviour {
	Bounds b;

	void Start(){
		b = this.GetComponentInParent<Bounds>();;
	}

	void OnTriggerEnter2D(Collider2D other){
		b.OnTriggerEnter2D(other);
	}

	void OnTriggerStay2D(Collider2D other){
		b.OnTriggerStay2D(other);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("Collide");
	}
}
