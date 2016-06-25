using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Transform redSpawn;
    public Transform blueSpawn;
    public static Transform _redSpawn;
    public static Transform _blueSpawn;

    private double timeStart = 0;
    public float moneyEarnPerSecond = 10f;
    public static bool pause = false;

    public static GameObject ballOfGame= null;
    
    void Start()
    {
        _redSpawn = redSpawn;
        _blueSpawn = blueSpawn;
        HUDManager.Instance.DisplayTimerInGame(true);
        timeStart = PhotonNetwork.time;
    }

    void Update()
    {

        HUDManager.Instance.EditTimerInGame(PhotonNetwork.time- timeStart); // Need to be Improve !

        if (ballOfGame == null && GameObject.FindGameObjectWithTag("Ball"))
        {
            ballOfGame = GameObject.FindGameObjectWithTag("Ball");
        }
    }

    public static void GamePause()
    {
        Debug.LogError("Pause");
        pause = !pause;
        Cursor.visible = pause;
        if (pause)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
