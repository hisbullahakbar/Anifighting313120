using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {

    [SerializeField]
    private int timeDuration;

    int timer;
    bool isTimerActive;
    
    [SerializeField]
    bool isTimeBattleMode;

    [SerializeField]
    private Text timerText;

    void Start()
    {
        StartingTimer();
        UpdateUI();
        if (isTimeBattleMode)
        {
            InvokeRepeating("UpdateTimer", 1.0f, 1.0f);
        }
    }

    void UpdateTimer()
    {
        if (BattleSceneManager.Instance.State == BattleSceneManager.BattleSceneState.battle)
        {
            if (isTimerActive)
            {
                if (!IsTimeOut())
                {
                    timer -= 1;
                    UpdateUI();
                }
                else
                {
                    if (Player.Instance.getHealth() > Player.Instance.Target.GetComponent<Enemy>().getHealth())
                    {
                        WinLoseManager.Instance.setWinLoseState(WinLoseManager.WinloseState.player1Win);
                        BattleSceneManager.Instance.State = BattleSceneManager.BattleSceneState.winLosePose;
                    }
                    else
                    {
                        WinLoseManager.Instance.setWinLoseState(WinLoseManager.WinloseState.player2Win);
                        BattleSceneManager.Instance.State = BattleSceneManager.BattleSceneState.winLosePose;
                    }
                    isTimerActive = false;
                }
            }
        }
    }

    void UpdateUI()
    {
        timerText.text = timer + "";
    }

    public void StartingTimer()
    {
        timer = timeDuration;
        isTimerActive = true;  
    }

    public bool IsTimeOut()
    {
        if (timer <= 0)
        {
            return true;
        }
        return false;
    }
}