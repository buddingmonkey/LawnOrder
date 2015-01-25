using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour {
	public float damage = 1;
	public CharacterMovement player;
	public bool stopAfterHit = true;
	public PlayerHealth health;

	
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag.Equals("Enemy")) {

			if (this.tag != "Projectile" || this.name != GameController.activeProjectile){
				var enemy = collider.GetComponentInParent<BaseEnemy>();
				Vector2 dir = new Vector2((collider.transform.position - transform.position).x > 0 ? 1 : -1, 0);
				enemy.TakeDamage(damage, dir, player.playerNum);

				if (this.tag == "Melee"){
					health.invincible = true;
				}
			}

			if (stopAfterHit) {
				Destroy (gameObject);
			}
		}
	}
}
