using UnityEngine;
using System.Collections;
using InControl;

public class InputControls : MonoBehaviour {
	public SimplePlayer player;
	InputDevice device;

	void Start(){
		device = InputManager.Devices[player.playerNum];
	}

	public bool Jumping() {
		if (device == null ) return false;
		return device.GetControl( InputControlType.Action1);
	}

	public float XAxis() {
		return Mathf.Max(device.LeftStickX, device.DPadX);
	}
}
