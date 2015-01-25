using UnityEngine;
using System.Collections;

public class MeleeFlash : MonoBehaviour {
	public float duration=.2f;

	private float timer;
	// Use this for initialization
	void Start () {
		timer = duration;
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
	}
}
