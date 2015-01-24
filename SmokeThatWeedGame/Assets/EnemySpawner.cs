using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public float spawnFrequency = 3f;
	public float spawnDelay = 1f;
	public bool randomStart = false;
	float timeSinceSpawn = 0;
	public enum Direction {
		Left,
		Right,
		Random
	}
	public Direction direction;

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
		switch (direction) {
		case Direction.Left:
			enemy.direction = -1;
			break;
		case Direction.Right:
			enemy.direction = 1;
			break;
		default:
			enemy.direction = Random.Range(0,2) == 0 ? -1 : 1;
			break;
		}
	}
}
