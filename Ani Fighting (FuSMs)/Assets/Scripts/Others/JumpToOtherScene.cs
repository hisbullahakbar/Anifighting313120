using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JumpToOtherScene : MonoBehaviour {

	[SerializeField]
	List<KeyCode> keysJumpScene;

	[SerializeField]
	List<string> keysSceneName;

	void Start () {
		
	}

	void Update(){
		for (int i = 0; i < keysJumpScene.Count; i++) {
			if (Input.GetKeyDown (keysJumpScene [i])) {
				goToScene (keysSceneName [i]);
			}
		}
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