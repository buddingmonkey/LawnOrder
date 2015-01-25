using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour {
	public float tickTockTempo = 3f;
	public int tickTockCount = 8;
	public float tickTockDelay= 5f;

	public GameObject[] weaponPrefabs;

	public GameObject tickTockPrefab;

	private GameObject[] tickTocks;
	private bool counting = false;
	private float countDownTimer;
	private int ticksTocked;
	private int weaponInSight;

	private SpriteRenderer theSprite;
	public bool grabbed = true;

	// Use this for initialization
	void Start () {


		countDownTimer = tickTockDelay;
		tickTocks = new GameObject[tickTockCount];

		theSprite = GetComponent<SpriteRenderer>();

		ChooseRandomWeapon ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!grabbed)
		{
			return;
		}
		//Debug.Log ("tick tock");

		if(!counting)
		{
			//Debug.Log ("notcounting");
			countDownTimer-=Time.deltaTime;
			if(countDownTimer<=0)
			{
				counting = true;
				countDownTimer = tickTockTempo;
				ticksTocked = 0;
			}
		}
		else
		{
			//Debug.Log ("counting");
			countDownTimer -= Time.deltaTime;
			if(countDownTimer<=0)
			{
				if(ticksTocked>=tickTockCount)
				{
					//Debug.Log ("TICK TOCK COMPLETE!");

					theSprite.enabled =false;

					//spawn!
					Holdable spawned = (Instantiate (weaponPrefabs[weaponInSight])as GameObject).GetComponent<Holdable>();
					spawned.transform.position = transform.position;
					spawned.RememberSpawner(this);


					ChooseRandomWeapon();

					foreach(GameObject tick in tickTocks)
					{
						tick.SetActive (false);
					}

					grabbed = false;
					counting = false;
					countDownTimer = tickTockDelay;
				}
				else
				{
					if(tickTocks[ticksTocked]==null)
					{
						//instantiate new ticktock
						float angle = Mathf.Deg2Rad * (90 + (- 360f * ticksTocked/(tickTockCount)));

						tickTocks[ticksTocked] = Instantiate (tickTockPrefab)as GameObject;
						tickTocks[ticksTocked].transform.parent = transform;
						tickTocks[ticksTocked].transform.localPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
					}
					else
					{
						//Debug.Log ("Turn on old tick tock "+Time.time);
						tickTocks[ticksTocked].SetActive(true);
					}

					countDownTimer = tickTockTempo;
				}
				ticksTocked+=1;
			}
		}

	}

	public void GetGrabbed(Shooter s)
	{
		grabbed = true;
		theSprite.enabled = true;
		GameController.activeProjectile = s.projectiles[weaponInSight].name;
		GameController.activeWeaponText = s.weaponNames[weaponInSight];
	}

	private void ChooseRandomWeapon()
	{
		weaponInSight = Random.Range (0, weaponPrefabs.Length);
		theSprite.sprite = weaponPrefabs [weaponInSight].GetComponent<SpriteRenderer> ().sprite;
	}
}
