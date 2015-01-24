using UnityEngine;
using System.Collections;
using InControl;

public class InputControls : MonoBehaviour {

	public enum ControlState{
		down,
		held,
		none
	}

	public CharacterMovement player;
	InputDevice device;

	void Start(){
		if (InputManager.Devices != null && InputManager.Devices.Count > player.playerNum){
			device = InputManager.Devices[player.playerNum];
			Debug.Log("Device attached: " + device.Name + " Player: " + player.playerNum);
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

	public ControlState Attack(){
		ControlState state = ControlState.none;

		if (this.device == null){
			if (player.playerNum != 0 )return ControlState.none;

			if (Input.GetButtonDown("Fire1")){
				state = ControlState.down;
			} else if (Input.GetButton("Fire1")){
				state = ControlState.held;
			}

			return state; 
		}

		InputControl ctrl = device.GetControl (InputControlType.Action2);
		if (ctrl.IsPressed){
			if (ctrl.HasChanged){
				state = ControlState.down;
			} else {
				state = ControlState.held;
			}
		}
		return state;
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
