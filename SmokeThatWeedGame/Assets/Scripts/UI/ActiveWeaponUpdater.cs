using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActiveWeaponUpdater : MonoBehaviour {

	Text uiText;
	string oldWeapon = null;
	Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
		uiText = GetComponent<Text> ();
		uiText.text = "";
	}
	// Update is called once per frame
	void Update () {
		if (oldWeapon != GameController.activeProjectile) {
			animator.SetTrigger("Flash");
			uiText.text = GameController.activeWeaponText;
			oldWeapon = GameController.activeProjectile;
			switch (GameController.activeWeaponText) {
			case "Flame Thrower":
				SfxManager.Instance.PlaySound("AnnounceFlamethrower");
				break;
			case "Weed Whacker":
				SfxManager.Instance.PlaySound("AnnounceWeedwhacker");
				break;
			case "Lawn Mower":
				SfxManager.Instance.PlaySound("AnnounceLawnmower");
				break;
			case "Agent Green":
				SfxManager.Instance.PlaySound("AnnounceAgentGreen");
				break;
			}


		}
	}

}
