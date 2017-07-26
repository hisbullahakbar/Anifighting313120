using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLockSystem : MonoBehaviour {
	
	public enum LockState
	{
		locked,
		unlocked
	}

	public LockState lockState;
	public string characterName;

	[SerializeField]
	int id;

	void Start () {
		//cara untuk unlock character belum tersedia / belum dibuat. //saat ini lockState di set melalui layar properti.
		if (lockState == LockState.locked) {
			characterName = "locked";
		} else {
			loadName (id);
		}
	}
	
	void loadName (int id) {
		switch (id) {
		case 0:
			characterName = "erza";
			break;
		case 1:
			characterName = "lyon";
			break;
		case 2:
			characterName = "juvia";
			break;
		case 3:
			characterName = "natsu";
			break;
		}
	}
}
