using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour {
	public float damage = 1;

	
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag.Equals("Enemy")) {
			print (collider.tag);
			var enemy = collider.GetComponentInParent<BaseEnemy>();
			enemy.TakeDamage(damage, (collider.transform.position - transform.position).normalized);
		}
	}
}
