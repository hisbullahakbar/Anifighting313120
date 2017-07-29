using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JumpToOtherScene : MonoBehaviour {

	[SerializeField]
	List<KeyCode> keysJumpScene;

	[SerializeField]
	List<string> keysSceneName;

	[SerializeField]
	bool isHasTransitionSound;

	[SerializeField]
	SoundManager soundManager;

	void Start () {
		
	}

	void Update(){
		for (int i = 0; i < keysJumpScene.Count; i++) {
			if (Input.GetKeyDown (keysJumpScene [i])) {
				#region specialfor fairy tail //just for main menu
				if (keysJumpScene [i] == KeyCode.Return || keysJumpScene [i] == KeyCode.Return) {
					if (isHasTransitionSound)
						soundManager.effectSoundPlay (8);
					StartCoroutine(goToSceneDelay("modechoosen"));
				}
				else
				{
					goToScene (keysSceneName [i]);
				}
				#endregion
			}
		}
	}

	IEnumerator goToSceneDelay(string scene){
		yield return new WaitForSeconds (2f);
		Application.LoadLevel(scene);
	}

    public static void quickGoToScene(string scene)
    {
        Application.LoadLevel(scene);
    }

    public void goToScene(string scene)
    {
        Application.LoadLevel(scene);
    }

    public void goToScene()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void goCloseApplication()
    {
        Application.Quit();
    }
}