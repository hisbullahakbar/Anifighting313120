using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaChoosenManager : MonoBehaviour {

	[SerializeField]
	GameObject[] arenasLogo;

	int selectedArena,previousArena;
	public static int statSelectedArena {
		set;
		get;
	}

	int maxRow = 2;

	[SerializeField]
	SoundManager soundManager;

	void Start () {
		selectedArena = -1;	
		statSelectedArena = -1;
	}

	void Update () {
		checkInput ();
		if (selectedArena >= 0) {
			updateSelectingArena ();
		}
	}

	void checkInput(){
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			previousArena = selectedArena;
			if (selectedArena < arenasLogo.Length / maxRow) {
				selectedArena = (selectedArena + 1) % (arenasLogo.Length / maxRow);
			} else {
				selectedArena = (selectedArena + 1) % arenasLogo.Length;
				if (selectedArena == 0)
					selectedArena = arenasLogo.Length / maxRow;
			}
			statSelectedArena = selectedArena;
			arenaChoosenSFX (statSelectedArena);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			previousArena = selectedArena;
			selectedArena -= 1;
			if (selectedArena < 0)
				selectedArena = (arenasLogo.Length / maxRow) - 1;
			else if (selectedArena < arenasLogo.Length / maxRow && previousArena >= arenasLogo.Length / maxRow)
				selectedArena = arenasLogo.Length - 1;
			
			statSelectedArena = selectedArena;
			arenaChoosenSFX (statSelectedArena);
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			previousArena = selectedArena;
			selectedArena = (selectedArena + (arenasLogo.Length / maxRow)) % arenasLogo.Length;
			statSelectedArena = selectedArena;
			arenaChoosenSFX (statSelectedArena);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			previousArena = selectedArena;
			selectedArena -= (arenasLogo.Length / maxRow);
			if (selectedArena < 0)
				selectedArena = previousArena + (arenasLogo.Length / maxRow);
			statSelectedArena = selectedArena;
			arenaChoosenSFX (statSelectedArena);
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			if (selectedArena != -1) {
				JumpToOtherScene.quickGoToScene ("mainscene");
			}
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			JumpToOtherScene.quickGoToScene ("characterchoosen");
		}
	}

	void updateSelectingArena(){
		arenasLogo [selectedArena].GetComponent<SpriteRenderer> ().color = new Color (107f/255f, 242f/255f, 124f/255f, 1f);

		if (previousArena >= 0) {
			arenasLogo [previousArena].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}
	}

	void arenaChoosenSFX(int idArena){
		soundManager.effectSoundPlay (idArena + 1);
	}
}