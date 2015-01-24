using UnityEngine;
using System.Collections;
using InControl;

public class InputControls : MonoBehaviour {


	void Start(){
	}

	public bool Jumping() {
		return Input.GetButton ("Action1");
	}

	public float XAxis() {
		return Input.GetAxis ("Horizontal");
	}
}
