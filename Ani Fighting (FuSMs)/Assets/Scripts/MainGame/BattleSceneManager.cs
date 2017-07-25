using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour {

    public enum BattleSceneState
    {
        characterInfo = 0,
        beginingPose = 1,
        battle = 2,
        battlePause = 3,
        winLosePose = 4,
        winLoseInfo = 5
    }

    private static BattleSceneManager instance;
    public static BattleSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BattleSceneManager>();
            }
            return instance;
        }
    }
    
    [SerializeField]
    private BattleSceneState state;
    public BattleSceneState State
    {
        set
        {
            state = value;
        }
        get
        {
            return state;
        }
    }

    #region openingBattle
    bool isAlreadyBeginingPose, isFightIconDestroyed;
    [SerializeField]
    GameObject fightIcon;
    #endregion

    [SerializeField]
    Text textWinLoseState;

	[SerializeField]
	GameObject winLoseMenu;

    void Start()
    {
        state = BattleSceneState.characterInfo;
        isAlreadyBeginingPose = false;
        isFightIconDestroyed = false;
    }
	
	void Update () {
		switch (state) {
		case BattleSceneState.characterInfo: //0
			break;
		case BattleSceneState.beginingPose: //1
			if (!isAlreadyBeginingPose) {
				Player.Instance.CharaAnimator.SetBool ("beginingPose", true);
				Player.Instance.Target.GetComponent<Enemy> ().CharaAnimator.SetBool ("beginingPose", true);
				isAlreadyBeginingPose = true;
			} else {
				if (!Player.Instance.CharaAnimator.GetBool ("beginingPose")
				    && !Player.Instance.Target.GetComponent<Enemy> ().CharaAnimator.GetBool ("beginingPose")) {
					state = BattleSceneState.battle;
					fightIcon.GetComponent<SpriteRenderer> ().enabled = true;
				}
			}
			break;
		case BattleSceneState.battle: //2
			if (!isFightIconDestroyed) {
				if (fightIcon.transform.position.y < 1f)
					fightIcon.transform.position = Vector3.MoveTowards (fightIcon.transform.position,
						new Vector3 (0, 1f, 0), 0.3f);
				else {
					Destroy (fightIcon);
					isFightIconDestroyed = true;
				}
			}
			break;
		case BattleSceneState.battlePause: //3
			break;
		case BattleSceneState.winLosePose: //4
			textWinLoseState.GetComponent<Text> ().enabled = true;
			if (WinLoseManager.Instance.isPlayer1Win ()) {
				textWinLoseState.text = "Player 1 Win";
				Player.Instance.CharaAnimator.SetBool ("winPose", true);
			} else if (WinLoseManager.Instance.isPlayer2Win ()) {
				textWinLoseState.text = "Player 2 Win";
				Player.Instance.Target.GetComponent<Enemy> ().CharaAnimator.SetBool ("winPose", true);
			}

			//WAKTU PEMANGGILAN MENGGUNAKAN DELAY
			StartCoroutine (WinLoseMenuDelayActivating ());
			break;
		case BattleSceneState.winLoseInfo: //5
			textWinLoseState.enabled = false;
			winLoseMenu.SetActive (true);
			break;
		}
	}

	IEnumerator WinLoseMenuDelayActivating(){
		yield return new WaitForSeconds (2f);
		state = BattleSceneManager.BattleSceneState.winLoseInfo;
	}
}