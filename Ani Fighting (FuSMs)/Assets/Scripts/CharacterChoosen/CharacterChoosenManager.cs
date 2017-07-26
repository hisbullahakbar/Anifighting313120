using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoosenManager : MonoBehaviour {
	
	[SerializeField]
	GameObject[] charactersLogo;
	[SerializeField]
	GameObject[] charactersText;
	[SerializeField]
	GameObject[] playerIcon;

	int selectedCharacter;
	public static int statSelectedCharacter1 {
		set;
		get;
	}
	public static int statSelectedCharacter2 {
		set;
		get;
	}

	void Start () {
		selectedCharacter = -1;	
		statSelectedCharacter1 = -1;
		statSelectedCharacter2 = -1;
	}

	void Update () {
		checkInput ();
		updateSelectingMode ();

		if (statSelectedCharacter1 == -1) {
			playerIcon [0].GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			playerIcon [0].GetComponent<SpriteRenderer> ().enabled = true;
			playerIcon [0].transform.position = new Vector3 (
				charactersLogo [statSelectedCharacter1].transform.position.x, -3.5f, charactersLogo [statSelectedCharacter1].transform.position.z);
		}

		if (statSelectedCharacter2 == -1) {
			playerIcon [1].GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			playerIcon [1].GetComponent<SpriteRenderer> ().enabled = true;
			playerIcon [1].transform.position = new Vector3 (
				charactersLogo [statSelectedCharacter2].transform.position.x, -3.5f, charactersLogo [statSelectedCharacter2].transform.position.z);
		}
	}

	void checkInput(){
		if (statSelectedCharacter2 == -1) {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				selectedCharacter = (selectedCharacter + 1) % charactersLogo.Length;

				if (selectedCharacter == statSelectedCharacter1) {
					selectedCharacter = (selectedCharacter + 1) % charactersLogo.Length;
				}
			} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				selectedCharacter -= 1;
				if (selectedCharacter < 0)
					selectedCharacter = charactersLogo.Length - 1;
			
				if (selectedCharacter == statSelectedCharacter1) {
					selectedCharacter -= 1;
					if (selectedCharacter < 0)
						selectedCharacter = charactersLogo.Length - 1;
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			
			if (statSelectedCharacter2 != -1) {
				JumpToOtherScene.quickGoToScene ("mainscene");
			} else {
				if (charactersLogo [selectedCharacter].GetComponent<CharacterLockSystem> ().lockState == CharacterLockSystem.LockState.unlocked) {
					if (statSelectedCharacter1 == -1) {
						statSelectedCharacter1 = selectedCharacter;
						selectedCharacter = -1;
					} else {
						if (statSelectedCharacter1 != selectedCharacter) { //tidak memilih karakter yg sama
							statSelectedCharacter2 = selectedCharacter;
							selectedCharacter = -1;
						}
					}
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			if (statSelectedCharacter2 != -1) {
				statSelectedCharacter2 = -1;
			}
			else if (statSelectedCharacter1 != -1) {
				statSelectedCharacter1 = -1;
			} else {
				JumpToOtherScene.quickGoToScene ("modechoosen");
			}
		}
	}

	void updateSelectingMode(){
		for (int i = 0; i < charactersLogo.Length; i++) {
			if (i == selectedCharacter || i == statSelectedCharacter1 || i == statSelectedCharacter2) { 
				selectingAnimation (charactersLogo [i], charactersText [i]);
			} else {
				unselectingAnimation (charactersLogo [i], charactersText [i]);
			}
		}
	}
		
	void selectingAnimation(GameObject someLogo, GameObject someText){
		someLogo.GetComponent<SpriteRenderer> ().color = new Color (120f / 255f, 88f / 255f, 88f / 255f, 1f);
		someText.transform.position = Vector3.MoveTowards (someText.transform.position,
			new Vector3 (someLogo.transform.position.x, someLogo.transform.position.y, someText.transform.position.z), 0.3f);
	}

	void unselectingAnimation(GameObject someLogo, GameObject someText){
		someLogo.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		someText.transform.position = Vector3.MoveTowards (someText.transform.position,
			new Vector3 (someLogo.transform.position.x, -3.5f, someText.transform.position.z), 0.3f);
	}	
}