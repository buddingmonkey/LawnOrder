using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public Transform enemyPrefab;
	public float spawnFrequency = 3f;
	public float spawnDelay = 1f;
	public bool randomStart = false;
	float timeSinceSpawn = 0;

	public static int maxEnemyCount = 30;
	public static int currentEnemyCount = 0;

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
//		Debug.Log (currentEnemyCount+" enemies present!!!!");
		if (currentEnemyCount >= maxEnemyCount)
			return;

		currentEnemyCount++;
		Transform enemy = (Transform) Instantiate(enemyPrefab == null ? GameController.Instance.enemyPrefabs[Random.Range(0, GameController.Instance.enemyPrefabs.Length)] : enemyPrefab);
		enemy.position = transform.position;
		var baseEnemy = enemy.GetComponent<BaseEnemy> ();
		switch (direction) {
		case Direction.Left:
			baseEnemy.direction = -1;
			break;
		case Direction.Right:
			baseEnemy.direction = 1;
			break;
		default:
			baseEnemy.direction = Random.Range(0,2) == 0 ? -1 : 1;
			break;
		}
	}
}
