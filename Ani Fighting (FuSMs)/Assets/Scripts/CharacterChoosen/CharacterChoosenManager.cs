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

	[SerializeField]
	SoundManager soundManager;

	int selectedCharacter;
	public static int statSelectedCharacter1 {
		set;
		get;
	}
	public static int statSelectedCharacter2 {
		set;
		get;
	}

	bool afterMove;

	void Start () {
		selectedCharacter = -1;	
		statSelectedCharacter1 = -1;
		statSelectedCharacter2 = -1;
		afterMove = false;
	}

	void Update () {
		checkInput ();
		updateSelectingMode ();

		if (statSelectedCharacter1 == -1) {
			playerIcon [0].GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			playerIcon [0].GetComponent<SpriteRenderer> ().enabled = true;
			playerIcon [0].transform.position = new Vector3 (
				charactersLogo [statSelectedCharacter1].transform.position.x, -2.9f, charactersLogo [statSelectedCharacter1].transform.position.z);
		}

		if (statSelectedCharacter2 == -1) {
			playerIcon [1].GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			playerIcon [1].GetComponent<SpriteRenderer> ().enabled = true;
			playerIcon [1].transform.position = new Vector3 (
				charactersLogo [statSelectedCharacter2].transform.position.x, -2.9f, charactersLogo [statSelectedCharacter2].transform.position.z);
		}
	}

	void checkInput(){
		if (statSelectedCharacter2 == -1) {
			if (Input.GetKeyDown (KeyCode.RightArrow) || (Input.GetAxis("Horizontal") >= 0.75f && !afterMove)) {
				selectedCharacter = (selectedCharacter + 1) % charactersLogo.Length;

				if (selectedCharacter == statSelectedCharacter1) {
					selectedCharacter = (selectedCharacter + 1) % charactersLogo.Length;
				}
				soundManager.effectSoundPlay (1);
				afterMove = true;
			} else if (Input.GetKeyDown (KeyCode.LeftArrow) || (Input.GetAxis("Horizontal") <= -0.75f && !afterMove)) {
				selectedCharacter -= 1;
				if (selectedCharacter < 0)
					selectedCharacter = charactersLogo.Length - 1;
			
				if (selectedCharacter == statSelectedCharacter1) {
					selectedCharacter -= 1;
					if (selectedCharacter < 0)
						selectedCharacter = charactersLogo.Length - 1;
				}
				soundManager.effectSoundPlay (1);
				afterMove = true;
			}
		}
			
		if (Input.GetAxis ("Horizontal") == 0.0f) {
			afterMove = false;
		}

		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown("joystick button 0")) {
			if (statSelectedCharacter2 != -1) {
				JumpToOtherScene.quickGoToScene ("arenachoosen");
			} else {
				if (selectedCharacter != -1) {
					if (charactersLogo [selectedCharacter].GetComponent<CharacterLockSystem> ().lockState == CharacterLockSystem.LockState.unlocked) {
						if (statSelectedCharacter1 == -1) {
							statSelectedCharacter1 = selectedCharacter;
							selectedCharacter = -1;

							if (charactersLogo [statSelectedCharacter1].GetComponent<CharacterLockSystem> ().characterName == "erza")
								soundManager.effectSoundPlay (3);
							else if (charactersLogo [statSelectedCharacter1].GetComponent<CharacterLockSystem> ().characterName == "lyon")
								soundManager.effectSoundPlay (4);
						} else {
							if (statSelectedCharacter1 != selectedCharacter) { //tidak memilih karakter yg sama
								statSelectedCharacter2 = selectedCharacter;
								selectedCharacter = -1;
							}
							soundManager.effectSoundPlay (5);

							if (charactersLogo [statSelectedCharacter2].GetComponent<CharacterLockSystem> ().characterName == "erza")
								StartCoroutine (playingDelaySFX (3));
							else if (charactersLogo [statSelectedCharacter2].GetComponent<CharacterLockSystem> ().characterName == "lyon")
								StartCoroutine (playingDelaySFX (4));
						}
					} else {
						soundManager.effectSoundPlay (2);
					}
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown("joystick button 1")) {
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

	IEnumerator playingDelaySFX(int idSFX){
		yield return new WaitForSeconds (1);
		soundManager.effectSoundPlay (idSFX);
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
			new Vector3 (someLogo.transform.position.x, -2.9f, someText.transform.position.z), 0.3f);
	}	
}