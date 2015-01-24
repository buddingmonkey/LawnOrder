using UnityEngine;
using System.Collections;
using InControl;

public class SimplePlayer : MonoBehaviour {
	public int playerNum;

	private float moveMultiplier = 1000;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		InputManager.Setup();
		InputManager.AttachDevice( new UnityInputDevice( new BuffaloClassicGamepad() ) );
		//Debug.Log(InputManager.Devices[0]);
		body = this.rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.Devices[0];//(InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;

		float move = 0;

		if (Input.GetKey(KeyCode.RightArrow)){
			move = 1;
		} else if (Input.GetKey(KeyCode.LeftArrow)){
			move = -1;
		}

		body.AddForce(Vector2.right * Time.deltaTime * move * moveMultiplier);
	}
}
