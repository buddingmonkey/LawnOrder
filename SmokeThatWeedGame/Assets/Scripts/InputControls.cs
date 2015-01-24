using UnityEngine;
using System.Collections;

public class InputControls : MonoBehaviour {

	public bool Jumping() {
		return Input.GetButton ("Jump");
	}

	public float XAxis() {
		return Input.GetAxis ("Horizontal");
	}
}
