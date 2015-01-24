using UnityEngine;
using System.Collections;
using InControl;

public class InputControls : MonoBehaviour {
	public CharacterMovement player;
	InputDevice device;

	void Start(){
		if (InputManager.Devices != null && InputManager.Devices.Count > player.playerNum){
			device = InputManager.Devices[player.playerNum];
		}
	}

	public bool Jumping() {
		if (device == null ) return false;
		return device.GetControl( InputControlType.Action1);
	}

	public float XAxis() {
		if (device == null ) return 0;
		if (Mathf.Abs(device.LeftStickX) > Mathf.Abs(device.DPadX)){
			return device.LeftStickX;
		} else {
			return device.DPadX;
		}
	}
}
