using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour {
	public float damage = 1;
	public CharacterMovement player;
	public bool stopAfterHit = true;

	
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag.Equals("Enemy")) {
			var enemy = collider.GetComponentInParent<BaseEnemy>();
			Vector2 dir = new Vector2((collider.transform.position - transform.position).x > 0 ? 1 : -1, 0);

			if (this.tag != "Projectile" || this.name != GameController.activeProjectile){
				enemy.TakeDamage(damage, dir, player.playerNum);
			}

			if (stopAfterHit) {
				Destroy (gameObject);
			}
		}
	}
}
