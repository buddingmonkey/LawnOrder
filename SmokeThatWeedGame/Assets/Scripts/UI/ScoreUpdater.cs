using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {

	public int slot;
	int previousScore;

	Text uiText;

	// Use this for initialization
	void Start () {
		uiText = (Text)GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		int score = GameController.score [slot];
		if (score != previousScore) {
			previousScore = score;
			uiText.text = "P" + (slot+1).ToString() + score.ToString(" 00000000");
		}
	}
}
