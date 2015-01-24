using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	public Sprite[] stages;
	public int movementStage = 3;

	public float growthRateSec;
	float timeInStage;
	int stage;
	SpriteRenderer spriteRenderer;

	float direction;

	// Use this for initialization
	void Start () {
		stage = 0;
		timeInStage = 0;
		spriteRenderer = (SpriteRenderer)GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeInStage += Time.deltaTime;
		if (timeInStage > growthRateSec && stage < stages.Length-1) {
			stage++;
			timeInStage = 0;
			spriteRenderer.sprite = stages[stage];
		}
	}
}
