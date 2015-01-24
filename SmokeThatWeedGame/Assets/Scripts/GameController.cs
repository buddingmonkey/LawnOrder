using UnityEngine;
using System.Collections;

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
	public static ulong[] score = new ulong[4];

	public static int round;

	public static int players = 4;

	public Transform[] spawners;

	public Transform playerPrefab;

	private Transform[] currentPlayers;

	void Awake () {
		_instance = this;
		currentPlayers = new Transform[4];
	}

	// Use this for initialization
	void Start () {
		Reset ();

		for (int i = 0; i < GameController.players; i++){
			SpawnPlayer(i);	
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void EndRound(){
		foreach (Transform t in currentPlayers){
			Destroy(t.gameObject);
		}
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
		player.GetComponent<SimplePlayer>().playerNum = playerNum;
		currentPlayers[playerNum] = player;
		player.name = "Player" + playerNum;
		player.parent = spawners[playerNum];
	}
}
