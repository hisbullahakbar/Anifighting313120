using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChoosenManager : MonoBehaviour {

	[SerializeField]
	GameObject[] modesLogo;
	[SerializeField]
	GameObject[] modesText;

	[SerializeField]
	SoundManager soundManager;

	int selectedMode,previousMode;
	public static int statSelectedMode {
		set;
		get;
	}

	bool afterMove;

	void Start () {
		selectedMode = -1;	
		statSelectedMode = -1;
		afterMove = false;
	}
	
	void Update () {
		checkInput ();
		if (selectedMode >= 0) {
			updateSelectingMode ();
		}
	}

	void checkInput(){
		if (Input.GetKeyDown (KeyCode.RightArrow) || (Input.GetAxis ("Horizontal") >= 0.75f && !afterMove)) {
			previousMode = selectedMode;
			selectedMode = (selectedMode + 1) % modesLogo.Length;
			statSelectedMode = selectedMode;
			soundManager.effectSoundPlay (1);
			afterMove = true;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) || (Input.GetAxis ("Horizontal") <= -0.75f && !afterMove)) {
			previousMode = selectedMode;
			selectedMode -= 1;
			if (selectedMode < 0)
				selectedMode = modesLogo.Length - 1;
			statSelectedMode = selectedMode;
			soundManager.effectSoundPlay (1);
			afterMove = true;
		}

		if (Input.GetAxis ("Horizontal") == 0.0f) {
			afterMove = false;
		}

		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown("joystick button 0")) {
			if (selectedMode != -1) {
				JumpToOtherScene.quickGoToScene ("characterchoosen");
			}
		}

		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown("joystick button 1")) {
			JumpToOtherScene.quickGoToScene ("mainmenu");
		}
	}

	void updateSelectingMode(){
		modesLogo [selectedMode].GetComponent<SpriteRenderer> ().color = new Color (120f/255f, 88f/255f, 88f/255f, 1f);
		modesText [selectedMode].transform.position = Vector3.MoveTowards (modesText [selectedMode].transform.position,
			new Vector3 (modesLogo [selectedMode].transform.position.x, -0.16f, -1f), 0.3f);
		
		if (previousMode >= 0) {
			modesLogo [previousMode].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			modesText [previousMode].transform.position = Vector3.MoveTowards (modesText [previousMode].transform.position,
				new Vector3 (modesLogo [previousMode].transform.position.x, -2.974989f, -1f), 0.3f);
		}
	}
}