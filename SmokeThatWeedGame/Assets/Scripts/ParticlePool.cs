using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticlePool : MonoBehaviour {
	private static ParticlePool _instance;
	public static ParticlePool Instance {
		get {
			return _instance;
		}
	}
	
	[System.Serializable]
	public class NamedParticleSystem {
		public string name;
		public Transform system;
		public int hardLimit;
	}

	public NamedParticleSystem[] poolConfigs;
	private Dictionary<string, NamedParticleSystem> poolHash;
	private Dictionary<string, Stack<Transform>> pools;
	private Dictionary<string, int> useCounter;
	private HashSet<Transform> despawned;


	// Use this for initialization
	void Awake () {
		_instance = this;
		despawned = new HashSet<Transform> ();
		poolHash = new Dictionary<string, NamedParticleSystem> ();
		pools = new Dictionary<string, Stack<Transform>> ();
		useCounter = new Dictionary<string, int> ();
		foreach (NamedParticleSystem s in poolConfigs) {
			poolHash.Add(s.name, s);
			pools.Add(s.name, new Stack<Transform>());
			useCounter.Add(s.name, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform Spawn(string name, Vector3 location) {
		Transform sys;
		if (pools [name].Count == 0) {
			if (poolHash[name].hardLimit == 0 || useCounter[name] > poolHash[name].hardLimit) {
				return null;
			}
			// spawn a new one
			useCounter[name] = useCounter[name]+1;
			sys = (Transform)Instantiate (poolHash[name].system, location, Quaternion.identity);
			sys.GetComponent<ParticleSystem>().Play();
			return sys;
		}
		sys = pools [name].Pop ();
		sys.transform.position = location;
		sys.GetComponent<ParticleSystem>().Play ();
		despawned.Remove (sys);
		return sys;
	}

	public void Despawn(string name, Transform system) {
		despawned.Add (system);
		system.GetComponent<ParticleSystem>().Stop ();
		system.transform.parent = null;
		useCounter [name] = useCounter [name] - 1;
		pools [name].Push (system);
	}

	public bool isSpawned(Transform system) {
		return !despawned.Contains (system);
	}

}
