using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float startingHealth = 5;
	float health;
	float timeHurt = 0;
	float timeInvinsible = 1;
	float flashTime;
	new SpriteRenderer renderer;
	Material mat;
	bool flashOn;

	public bool invincible = false;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		health = 5;
		timeHurt = 0;
		flashOn = false;
		mat = renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeHurt < timeInvinsible) {
			timeHurt += Time.deltaTime;
			flashTime+=Time.deltaTime;
			if (!flashOn) {
				if (flashTime > 0.1f) {
					flashOn = true;
					mat.SetFloat("_colorOverlay", .9f);
					flashTime = 0;
				}
			} else {
				if (flashTime > 0.05f) {
					flashOn = false;
					mat.SetFloat("_colorOverlay", 0);
					flashTime = 0;
				}
			}
		} else if (flashOn) {
			flashOn = false;
			mat.SetFloat("_colorOverlay", 0);
		}

	}

	
	void OnCollisionStay2D (Collision2D collision)
	{
		if (timeHurt > 1 && !invincible) {
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
					flashOn = true;
					mat.SetFloat("_colorOverlay", .9f);
				}
			}
		}
	}

}
