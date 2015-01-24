using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour {
	public float damage = 1;
	public CharacterMovement player;
	public bool stopAfterHit = true;

	
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag.Equals("Enemy")) {
			var enemy = collider.GetComponentInParent<BaseEnemy>();
			enemy.TakeDamage(damage, (collider.transform.position - transform.position).normalized, player.playerNum);

			if (stopAfterHit) {
				Destroy (gameObject);
			}
		}
	}
}
