using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }

    public float timeSinceGameStart = 0.0f; //TimerManagement a faire !
    public float moneyEarnPerSecond = 10f;
    private float startTime;

    public GameObject ballOfGame= null;
    
    void Start()
    {
        HUDManager.Instance.DisplayTimerInGame(true);
    }

    void Update()
    {
        timeSinceGameStart += Time.deltaTime;
        HUDManager.Instance.EditTimerInGame(timeSinceGameStart);
        if (ballOfGame == null && GameObject.FindGameObjectWithTag("Ball"))
        {
            ballOfGame = GameObject.FindGameObjectWithTag("Ball");
        }
    }

}
