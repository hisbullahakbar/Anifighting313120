using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoopScale : MonoBehaviour {
	[SerializeField]
	float minScale;
	[SerializeField]
	float maxScale;
	[SerializeField]
	float delayStart;
	[SerializeField]
	float scalePerTime;

	bool isFromMinScale; //goto max scale
	bool isAlreadyLoopScale;

	void Start () {
		isFromMinScale = true;
		StartCoroutine (alreadyLoopScale ());
	}

	void Update () {
		if (isAlreadyLoopScale) {
			if (isFromMinScale) {
				if (transform.localScale != Vector3.one * maxScale)
					transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one * maxScale, scalePerTime);
				else
					isFromMinScale = false;
			} else {
				if (transform.localScale != Vector3.one * minScale)
					transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one * minScale, scalePerTime);
				else
					isFromMinScale = true;
			}
		}
	}

	IEnumerator alreadyLoopScale(){
		yield return new WaitForSeconds (delayStart);
		isAlreadyLoopScale = true;
		StopAllCoroutines ();
	}
}
