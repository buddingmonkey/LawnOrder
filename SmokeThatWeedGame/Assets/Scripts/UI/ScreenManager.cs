using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour {
	public Transform gameOverPanel;

	private Transform canvas;

	// Use this for initialization
	void Start () {
		canvas = ((GameObject)GameObject.Find ("Canvas")).transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowGameOver(string winners) {
		Transform panel = (Transform)Instantiate (gameOverPanel);
		panel.FindChild ("WinnerText").GetComponent<Text> ().text = winners;
		panel.SetParent (canvas, false);
	}
}
