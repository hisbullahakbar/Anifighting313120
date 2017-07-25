using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChoosenManager : MonoBehaviour {

	[SerializeField]
	GameObject[] modesLogo;
	[SerializeField]
	GameObject[] modesText;

	int selectedMode,previousMode;

	void Start () {
		selectedMode = -1;	
	}
	
	void Update () {
		checkInput ();
		if (selectedMode >= 0) {
			updateSelectingMode ();
		}
	}

	void checkInput(){
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			previousMode = selectedMode;
			selectedMode = (selectedMode + 1) % modesLogo.Length;
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			previousMode = selectedMode;
			selectedMode -= 1;
			if (selectedMode < 0)
				selectedMode = modesLogo.Length - 1;
		}
	}

	void updateSelectingMode(){
		modesLogo [selectedMode].GetComponent<SpriteRenderer> ().color = new Color (120f/255f, 88f/255f, 88f/255f, 1f);
		modesText [selectedMode].transform.position = Vector3.MoveTowards (modesText [selectedMode].transform.position,
			new Vector3 (modesLogo [selectedMode].transform.position.x, -0.455f, -1f), 0.3f);
		
		if (previousMode >= 0) {
			modesLogo [previousMode].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			modesText [previousMode].transform.position = Vector3.MoveTowards (modesText [previousMode].transform.position,
				new Vector3 (modesLogo [previousMode].transform.position.x, -2.974989f, -1f), 0.3f);
		}
		//StartCoroutine (setSelectingMode ());
	}

	IEnumerator setSelectingMode(){
		yield return new WaitForSeconds (2f);
		modesText [selectedMode].transform.position = Vector3.MoveTowards (modesText [selectedMode].transform.position,
			new Vector3 (modesLogo [selectedMode].transform.position.x, -0.455f, 
				modesText [selectedMode].transform.position.z), 0.0001f);
	}
}