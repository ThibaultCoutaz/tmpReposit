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
       

    void Start()
    {
    }

    //To Init the Player Name
    private void InitNamePlayer()
    {
        // generate a name for this player, if none is assigned yet
        if (PlayerPrefs.GetString("playerName") != null)
        {
            PhotonNetwork.playerName = PlayerPrefs.GetString("playerName");
            playerName.text = PhotonNetwork.playerName.ToString();
        }

        if (String.IsNullOrEmpty(PhotonNetwork.playerName))
        {
            PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
            playerName.text = PhotonNetwork.playerName.ToString();
        }
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
