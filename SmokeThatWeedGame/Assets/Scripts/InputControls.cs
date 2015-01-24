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
	
	public float YAxis() {
		if (this.device == null){
			if (player.playerNum != 0 ) return 0;
			return Input.GetAxis ("Vertical");
		}
		
		if (Mathf.Abs(device.LeftStickY) > Mathf.Abs(device.DPadY)){
			return device.LeftStickY;
		} else {
			return device.DPadY;
		}
	}
	
	public bool Attack(){
		if (this.device == null){
			if (player.playerNum != 0 )return false;
			return Input.GetButtonDown ("Fire1"); 
		}

		InputControl ctrl = device.GetControl (InputControlType.Action2);
		return ctrl.IsPressed;
	}

	public bool Throw(){
		if (this.device == null){
			if (player.playerNum != 0 )return false;
			return Input.GetKeyDown (KeyCode.LeftShift); 
		}
		
		InputControl ctrl = device.GetControl (InputControlType.Action3);
		return ctrl.IsPressed && ctrl.HasChanged;
	}
}
