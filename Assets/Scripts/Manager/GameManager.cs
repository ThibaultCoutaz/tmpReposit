using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }

    public GameObject ballOfGame= null;

    void Update()
    {
        if (ballOfGame == null && GameObject.FindGameObjectWithTag("Ball"))
        {
            ballOfGame = GameObject.FindGameObjectWithTag("Ball");
        }
    }

}
