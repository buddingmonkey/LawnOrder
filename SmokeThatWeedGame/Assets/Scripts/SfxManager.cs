using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SfxManager : MonoBehaviour {
	private static SfxManager _instance;
	public static SfxManager Instance {
		get {
			return _instance;
		}
	}

	[System.Serializable]
	public class NamedAudioClip {
		public string name;
		public AudioClip clip;
	}

	public NamedAudioClip[] audioClips;

	private Dictionary<string, AudioClip> audioDict;

	void Start() {
		_instance = this;
		audioDict = new Dictionary<string, AudioClip> ();
		foreach (NamedAudioClip c in audioClips) {
			audioDict.Add(c.name, c.clip);
		}
	}

	public void PlaySoundAt(string name, Vector3 location) {
		AudioSource.PlayClipAtPoint (audioDict[name], location);
	}
}
