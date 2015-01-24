using UnityEngine;
using System.Collections;
using InControl;

public class SimplePlayer : MonoBehaviour {
	public int playerNum;

	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		Debug.Log(InputManager.Devices);
		body = this.rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.Devices[0];//(InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
		Debug.Log(inputDevice.DPad.Vector);
		body.AddForce(Vector2.right * Time.deltaTime * inputDevice.LeftStick.X);
	}
}
