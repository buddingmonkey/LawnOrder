using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActiveWeaponUpdater : MonoBehaviour {

	Text uiText;
	string oldWeapon = null;
	
	void Start() {
		uiText = GetComponent<Text> ();
		uiText.text = "";
	}
	// Update is called once per frame
	void Update () {
		if (oldWeapon != GameController.activeProjectile) {
			uiText.text = GameController.activeWeaponText;
			oldWeapon = GameController.activeProjectile;
		}
	}

}
