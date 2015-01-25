using UnityEngine;
using System.Collections;

public class SfxTrigger : MonoBehaviour {
	public Vector3 location;
	// Use this for initialization
	void Start () {
		SfxManager.Instance.PlaySoundAt("Flamethrower",location);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
