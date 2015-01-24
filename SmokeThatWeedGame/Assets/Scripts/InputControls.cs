using UnityEngine;
using System.Collections;
using InControl;

public class InputControls : MonoBehaviour {
	public CharacterMovement player;
	InputDevice device;

	void Start(){
		if (InputManager.Devices != null && InputManager.Devices.Count > player.playerNum){
			Debug.Log ("Device Set!");
			device = InputManager.Devices[player.playerNum];
		}
	}

	public bool IsJumpDown() {
		if (this.device == null){
			if (player.playerNum != 0 ) return false;
			return Input.GetButtonDown ("Jump");
		}
		InputControl ctrl = device.GetControl (InputControlType.Action1);
		return ctrl.IsPressed && ctrl.HasChanged;
	}

	public bool Jumping() {
		if (this.device == null){
			if (player.playerNum != 0 )return false;
			return Input.GetButton ("Jump");
		}
		
		return device.GetControl( InputControlType.Action1);
	}

	public float XAxis() {
		if (this.device == null){
			if (player.playerNum != 0 ) return 0;
			return Input.GetAxis ("Horizontal");
		}
		
		if (Mathf.Abs(device.LeftStickX) > Mathf.Abs(device.DPadX)){
			return device.LeftStickX;
		} else {
			return device.DPadX;
		}
	}
}
