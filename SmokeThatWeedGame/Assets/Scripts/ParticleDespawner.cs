using UnityEngine;
using System.Collections;

public class ParticleDespawner : MonoBehaviour {

	public float despawnAfter;
	public string poolName;

	float time = 0;
	ParticleSystem system;

	// Use this for initialization
	void Start () {
		time = 0;
		system = GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (ParticlePool.Instance.isSpawned(transform)) {
			time += Time.deltaTime;
			if (despawnAfter > 0 && time > despawnAfter) {
				time = 0;
				system.Stop();
				ParticlePool.Instance.Despawn(poolName, transform);
			}
		} else {
			time = 0;
		}
	}
}
