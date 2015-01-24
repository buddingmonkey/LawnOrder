using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float startingHealth = 5;
	float health;
	float timeHurt = 0;
	float timeInvisible = 1;
	float flashTime;
	new SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		health = 5;
		timeHurt = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeHurt < 1) {
			timeHurt += Time.deltaTime;
			flashTime+=Time.deltaTime;
			if (renderer.enabled) {
				if (flashTime > 0.1f) {
					renderer.enabled = false;
					flashTime = 0;
				}
			} else {
				if (flashTime > 0.05f) {
					renderer.enabled = true;
					flashTime = 0;
				}
			}
		} else if (!renderer.enabled) {
			renderer.enabled = true;
		}

	}

	
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (timeHurt > 1) {
			if (collision.collider.gameObject.CompareTag ("Enemy")) {
				health -= 1;
				if (health < 0) {
					// TODO a cool animation or effect
					int pNum = GetComponent<CharacterMovement>().playerNum;
					Destroy (gameObject);
					GameController.Instance.SpawnPlayer(pNum);
				} else {
					timeHurt = 0;
					flashTime = 0;
					renderer.enabled = false;
				}
			}
		}
	}

}
