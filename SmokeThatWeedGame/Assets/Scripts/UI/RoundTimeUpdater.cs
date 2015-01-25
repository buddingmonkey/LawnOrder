using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundTimeUpdater : MonoBehaviour {
	Text uiText;

	void Start() {
		uiText = GetComponent<Text> ();
	}
	// Update is called once per frame
	void Update () {
		int time = (int)GameController.roundTime;
		uiText.text = (time / 60).ToString("00:") + (time % 60).ToString("00");
	}
}
