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
    private Text timerText;

	void Start () {
        StartingTimer();
        UpdateUI();
        InvokeRepeating("UpdateTimer", 1.0f, 1.0f);
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