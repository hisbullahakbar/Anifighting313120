using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAutoScale : MonoBehaviour {
	[SerializeField]
	float maxScale;
	[SerializeField]
	bool isOneTimeScale;
	[SerializeField]
	float scalePerTime;

	void Start () {
	}

	void Update () {
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one * maxScale, scalePerTime);

		if (isOneTimeScale) {
			if (transform.localScale == Vector3.one * maxScale)
				Destroy (gameObject.GetComponent<SpriteAutoScale> ());
		}
	}
}