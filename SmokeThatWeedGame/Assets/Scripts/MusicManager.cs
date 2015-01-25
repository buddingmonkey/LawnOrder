using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip [] clips;
	public int clipIndex = 0;
	public bool selectRandom = true;
	private int x;

	// Use this for initialization
	void Start () {
		if (selectRandom) {
			x = Random.Range(0, clips.Length);
		}
		else {
			x = (int)clipIndex;
		}
		var audio = GetComponent<AudioSource> ();
		audio.clip = clips[x];
		Debug.Log("index:" + x.ToString());
		audio.Play();
	}
}
