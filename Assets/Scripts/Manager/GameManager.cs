using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }

    public float timeSinceGameStart = 0.0f; //TimerManagement a faire !
    public float moneyEarnPerSecond = 10f;
    public bool pause = false;

    public GameObject ballOfGame= null;
    
    void Start()
    {
        HUDManager.Instance.DisplayTimerInGame(true);
    }

    void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            timeSinceGameStart += Time.deltaTime;
            //GetComponent<PhotonView>().RPC("SyncTime", PhotonTargets.AllViaServer);
        }
        else
        {
            timeSinceGameStart = tmpTime;
        }

        HUDManager.Instance.EditTimerInGame(timeSinceGameStart);

        if (ballOfGame == null && GameObject.FindGameObjectWithTag("Ball"))
        {
            ballOfGame = GameObject.FindGameObjectWithTag("Ball");
        }
    }

    public void GamePause()
    {
        Debug.LogError("Pause");
        pause = !pause;
        Cursor.visible = pause;
        if (pause)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private float tmpTime;

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo message)
    {
        if (stream.isWriting)
        {
            stream.SendNext(timeSinceGameStart);
        }
        else
        {
            tmpTime = (float)stream.ReceiveNext();
        }
    }
}
