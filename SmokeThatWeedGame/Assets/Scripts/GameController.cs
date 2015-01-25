using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	static GameController _instance;
	public static GameController Instance {
		get {
			return _instance;
		}
	}

	public enum GameState {
		Start,
		Ready,
		Playing,
		Paused,
		GameOver
	}

	public static GameState state;
	public static int[] score = new int[4];

	public static int round;
	public static float roundTime;

	public static int players = 4;

	public static string activeProjectile = null;
	public static string activeWeaponText = "None";

	public Transform[] enemyPrefabs;
	public Transform[] spawners;

	public ScreenManager screenManager;
	public Transform playerPrefab;
	public float roundLength;
	private float startTime;

	private Transform[] currentPlayers;

	void Awake () {
		_instance = this;
		currentPlayers = new Transform[4];
	}

	// Use this for initialization
	void Start () {
		Reset ();
		roundTime = roundLength;

		for (int i = 0; i < GameController.players; i++){
			SpawnPlayer(i);	
		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy Spawn")) {
			if (obj.GetComponent<EnemySpawner>() == null) {
				obj.AddComponent<EnemySpawner>();
			}
		}

		startTime = 4;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == GameState.Start) {
			startTime -= Time.deltaTime;
			if (startTime <= 0) {
				state = GameState.Playing;
			}
		}
		if (state == GameState.Playing) {
			roundTime -= Time.deltaTime;
			if (roundTime <= 0) {
				roundTime = 0;
				EndRound();
			}
		}
	}

	public void EndRound(){
		state = GameState.GameOver;
		foreach (Transform t in currentPlayers){
			Destroy(t.gameObject);
		}
		currentPlayers [0] = null;
		currentPlayers [1] = null;
		currentPlayers [2] = null;
		currentPlayers [3] = null;

		int highScore = 0;
		for (int i=0; i<4; i++) {
			if (score[i] > highScore) {
				highScore = score[i];
			}
		}
		List<int> winners = new List<int> ();
		for (int i=0; i<4; i++) {
			if (score[i] == highScore) {
				winners.Add(i);
			}
		}
		string winnerStr;
		if (winners.Count > 1) {
			winnerStr = "-- TIE --";
		} else {
			winnerStr = "Player " + (winners[0]+1).ToString() + " Wins!";
		}
		screenManager.ShowGameOver (winnerStr);
	}

	public static void Reset() {
		state = GameState.Start;
		round = 1;
		score[0] = 0;
		score[1] = 0;
		score[2] = 0;
		score[3] = 0;
	}

	public void SpawnPlayer(int playerNum){
		Transform player = (Transform) Instantiate(playerPrefab);
		player.position = spawners[playerNum].position;
		player.GetComponent<CharacterMovement>().playerNum = playerNum;
		currentPlayers[playerNum] = player;
		player.name = "Player" + playerNum;
		player.SetParent(spawners[playerNum]);
	}
}
