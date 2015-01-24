using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public float spawnFrequency = 3f;
	public float spawnDelay = 1f;
	public bool randomStart = false;
	float timeSinceSpawn = 0;

	// Use this for initialization
	void Start () {
		timeSinceSpawn = spawnFrequency - spawnDelay;
		if (randomStart) {
			timeSinceSpawn = -spawnDelay + Random.Range(0, spawnFrequency);
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Time.deltaTime;
		if (timeSinceSpawn > spawnFrequency) {
			Spawn();
			timeSinceSpawn = 0;
		}
	}

	void Spawn() {
		GameObject enemy = (GameObject) Instantiate(GameController.Instance.enemyPrefab);
		enemy.transform.position = transform.position;
	}
}
