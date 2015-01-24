using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Bounds : MonoBehaviour {
	public Collider2D left,right,top,bottom;

	private Collider2D[] colliders;

	void Start(){
		colliders = new[]{left, right, top, bottom};
	}

	public void OnTriggerEnter2D(Collider2D other){
		//Debug.Log("TRIGGERED");
	}

	public void OnTriggerStay2D(Collider2D other) {
		Debug.Log ("TRIGGERED");
		if (other.tag == "Player" || other.tag == "Weapon" || other.tag == "Enemy"){
			foreach (Collider2D c in colliders){
				List<Collider2D> contains = new List<Collider2D>(Physics2D.OverlapAreaAll(c.bounds.max, c.bounds.min));
				if (contains.Contains(other)){
					Teleport(c, other.transform);
					break;
				}
  			}
		}
	}

	void Teleport(Collider2D from, Transform player){
		var r = player.rigidbody2D;

		if (from == left && r.velocity.x < 0){
			player.transform.position = new Vector2(right.transform.position.x, player.transform.position.y);
		} else if (from == right && r.velocity.x > 0){
			player.transform.position = new Vector2(left.transform.position.x, player.transform.position.y);
		} else if (from == top && r.velocity.y > 0){
			player.transform.position = new Vector2(player.transform.position.x, bottom.transform.position.y);
		} else if (from == bottom && r.velocity.y < 0){
			player.transform.position = new Vector2(player.transform.position.x, top.transform.position.y);
		}
	}
}
