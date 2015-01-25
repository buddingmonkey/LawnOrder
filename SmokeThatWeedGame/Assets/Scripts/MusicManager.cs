using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip [] clips;
	public int clipIndex;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().clip = clips[clipIndex];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
