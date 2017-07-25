using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseManager : MonoBehaviour {

    private static WinLoseManager instance;
    public static WinLoseManager Instance
    {
        set
        {
            instance = value;
        }
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<WinLoseManager>();
            }
            return instance;
        }
    }

    public enum WinloseState
    {
        gameStillRunning,
        player1Win,
        player2Win
    }

    WinloseState winLoseState;

    void Start()
    {
        winLoseState = WinloseState.gameStillRunning;
    }

    void Update()
    {
    }

    public void setWinLoseState(WinLoseManager.WinloseState state)
    {
        winLoseState = state;
    }

    public bool isGameStillRunning()
    {
        if (winLoseState == WinloseState.gameStillRunning)
        {
            return true;
        }

        return false;
    }

    public bool isPlayer1Win()
    {
        if (winLoseState == WinloseState.player1Win)
        {
            return true;
        }

        return false;
    }

    public bool isPlayer2Win()
    {
        if (winLoseState == WinloseState.player2Win)
        {
            return true;
        }

        return false;
    }
}
