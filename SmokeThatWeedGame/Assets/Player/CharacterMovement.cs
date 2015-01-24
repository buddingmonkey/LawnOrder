using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public bool keyboard;
	private RectTransform tran;
	private float height;
	// Use this for initialization
	void Start () {
		tran = GetComponent<RectTransform>();
		height = tran.rect.height;
		Debug.Log (height);
	}
	
	// Update is called once per frame
	void Update () {
		if(keyboard)
		{
			if(Input.GetKey ("x"))
			{	
				Vector2 pos2D = new Vector2 (tran.position.x,tran.position.y);
				Ray2D toeTouch = new Ray2D(pos2D,-Vector2.up);
				Debug.DrawRay(tran.position-(Vector3.up*.5f),-Vector3.up*.05f,Color.green);

				RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up*.5f), -Vector2.up,.05f);
				if(hit.transform != null)
				{
					Debug.Log (hit.point);
					Debug.DrawRay(hit.point,-Vector3.right*.1f,Color.red);
					//Debug.Log ("xxxxx" +Time.time);
					rigidbody2D.velocity += Vector2.up;
				}
			}
		}
	}
}
