using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool isAlreadyBeginingPose;

    void Start()
    {
        state = BattleSceneState.characterInfo;
        isAlreadyBeginingPose = false;
    }
	
	void Update () {
		switch(state){
            case BattleSceneState.characterInfo: //0
                break;
            case BattleSceneState.beginingPose: //1
                if (!isAlreadyBeginingPose)
                {
                    Player.Instance.CharaAnimator.SetBool("beginingPose", true);
                    Player.Instance.Target.GetComponent<Enemy>().CharaAnimator.SetBool("beginingPose", true);
                    isAlreadyBeginingPose = true;
                }
                else
                {
                    if (!Player.Instance.CharaAnimator.GetBool("beginingPose")
                        && !Player.Instance.Target.GetComponent<Enemy>().CharaAnimator.GetBool("beginingPose"))
                        state = BattleSceneState.battle;
                }
                break;
            case BattleSceneState.battle: //2
                break;
            case BattleSceneState.battlePause: //3
                break;
            case BattleSceneState.winLosePose: //4
                break;
            case BattleSceneState.winLoseInfo: //5
                break;
        }
	}
}