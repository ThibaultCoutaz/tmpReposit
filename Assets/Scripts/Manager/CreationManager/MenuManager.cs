using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MenuManager : MonoBehaviour {

    [SerializeField] GameObject LobbyJoinCreate;
    [SerializeField] GameObject Matchmaking;

    private string gameVersion = "1.0";

    [SerializeField]InputField playerName;


    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(gameVersion); // To check if all the player have the same version of the game
        PhotonNetwork.sendRate = 30; //Surment a revoir
        PhotonNetwork.sendRateOnSerialize = 30;

        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.automaticallySyncScene = true;
        
        InitNamePlayer();
    }

    void Update()
    {
        HUDManager.Instance.EditTextPing("Ping : " + PhotonNetwork.GetPing() + "ms");
    }

    //To Init the Player Name  ////****TO DO******case a cocher pour le cas ou le joueur souhaiterait se souvenir de son pseudo pour ne pas avoir a le reecrire a chaque fois********///
    private void InitNamePlayer()
    {
        // generate a name for this player, if none is assigned yet
        if (PlayerPrefs.GetString("playerName") != null)
        {
            PhotonNetwork.playerName = PhotonNetwork.playerName;
            playerName.text = PhotonNetwork.playerName.ToString();
        }

        if (String.IsNullOrEmpty(PhotonNetwork.playerName))
        {
            PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
            playerName.text = PhotonNetwork.playerName.ToString();
        }
    }

    //When name is Submit
    public void OnNameSubmit()
    {
        PhotonNetwork.playerName = playerName.text;
    }


    public void ClickLobbyJoinCreate()
    {
        Matchmaking.SetActive(false);
        LobbyJoinCreate.SetActive(true);
    }

    public void ClickMatchMaking()
    {
        Matchmaking.SetActive(true);
        LobbyJoinCreate.SetActive(false);
    }

    void OnJoinedLobby()
    {
        Matchmaking.SetActive(false);
        LobbyJoinCreate.SetActive(true);
    }
}
