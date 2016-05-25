﻿using UnityEngine;
using System.Collections;

public class WaitingRoomManager : MonoBehaviour {

    [SerializeField]
    GameObject PlayerConnected;

    [SerializeField]
    GameObject BackGround;

    [SerializeField]
    GameObject[] TabPlayerCo;


	// Use this for initialization
	void Start () {
        //TabPlayerCo = new GameObject[PhotonNetwork.room.maxPlayers];
        TabPlayerCo = new GameObject[10];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnJoinedRoom()
    {
        if (PhotonNetwork.countOfPlayers < PhotonNetwork.room.maxPlayers)
        {
            //Tout afficher
            DisplayPlayerConnected();
        }
        else
        {
            //StartGame
        }
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (PhotonNetwork.countOfPlayers < PhotonNetwork.room.maxPlayers)
        {
            //Juste afficher le dernier arrive
            DisplayPlayerConnected();
        }
        else
        {
            //StartGame
        }
    }

    void DisplayPlayerConnected()
    {
        for(int i=0;i< PhotonNetwork.countOfPlayers; i++)
        {

        }
    }
}