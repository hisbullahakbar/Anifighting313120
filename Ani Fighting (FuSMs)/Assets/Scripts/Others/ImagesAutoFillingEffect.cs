using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesAutoFillingEffect : MonoBehaviour {

	[SerializeField]
	float switchDelay;
	[SerializeField]
	float fillInPerTime;
	[SerializeField]
	float fillOutPerTime;

	[SerializeField]
	bool randomFirstImage;

	[SerializeField]
	Image[] images;

	int activeIndex;
	bool isAlreadyFilled;
	bool isAlreadySwitch;

	void Start () {
		for (int i = 0; i < images.Length; i++) {
			images [i].type = Image.Type.Filled;
			images [i].fillAmount = 0;
		}

		if (randomFirstImage)
			activeIndex = Random.Range (0, images.Length);
		else
			activeIndex = 0;
		
		images [activeIndex].fillAmount = 1;
		isAlreadyFilled = true;
		isAlreadySwitch = false;
		activeIndex = (activeIndex + 1) % images.Length;
	}
	
	void Update () {
		if (isAlreadyFilled) {
			if (!isAlreadySwitch)
				StartCoroutine (switchTime ());
			else
				StopAllCoroutines ();
		} else {
			activeIndex = (activeIndex + 1) % images.Length;
			isAlreadyFilled = true;
		}

		if (isAlreadySwitch) {
			if (activeIndex > 0) {
				if (images [activeIndex - 1].fillAmount > 0) {
					images [activeIndex - 1].fillAmount -= fillOutPerTime;
				}	
			} else {
				if (images [images.Length - 1].fillAmount > 0) {
					images [images.Length - 1].fillAmount -= fillOutPerTime;
				}
			}

			if (images [activeIndex].fillAmount < 1) {
				images [activeIndex].fillAmount += fillInPerTime;
			} else {
				isAlreadyFilled = false;
				isAlreadySwitch = false;
			}
		}
	}

	IEnumerator switchTime(){
		yield return new WaitForSeconds (switchDelay);
		isAlreadySwitch = true;
	}
}