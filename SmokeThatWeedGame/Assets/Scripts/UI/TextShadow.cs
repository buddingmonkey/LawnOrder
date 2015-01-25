using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextShadow : MonoBehaviour {

	protected Text source;
	protected Text text;

	// Use this for initialization
	void Start () {
		if (this.source != null) {
			return;
		}
		GameObject obj = gameObject;

		Dup (obj, new Vector3 (2, -2, 0));
		this.source = null;
//		Dup (obj, new Vector3 (-1, -1, 0));
//		Dup (obj, new Vector3 (1, 1, 0));
//		Dup (obj, new Vector3 (-1, 1, 0));
	}

	void Dup(GameObject obj, Vector3 offset) {
		GameObject dup = (GameObject)Instantiate (obj);
		dup.transform.SetParent (obj.transform.parent, false);
		dup.transform.SetSiblingIndex (0);
		dup.GetComponent<RectTransform> ().localPosition = dup.GetComponent<RectTransform> ().localPosition + offset;
		dup.GetComponent<Text> ().color = new Color(.1f,.1f,.1f,1f);
		dup.GetComponent<TextShadow>().text = dup.GetComponent<Text> ();
		dup.GetComponent<TextShadow>().source = obj.GetComponent<Text> ();
		var animator = dup.GetComponent<Animator> ();
		if (animator != null) {
			animator.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (source != null && source.text != this.text.text) {
			this.text.text = source.text;
		}
	}
}
