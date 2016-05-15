using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfosRoom : MonoBehaviour {

    [SerializeField]
    Text serverName;

    [SerializeField]
    Text serverNumberPlayer;

    [SerializeField]
    Button joinServer;

    private string nameServer;

    public void EditServeurName(string text)
    {
        nameServer = text;
        serverName.text = text;
    }

    public void EditNumberPlayers(string text)
    {
        serverNumberPlayer.text = text;
    }

    public void JoinServer()
    {
        PhotonNetwork.JoinRoom(nameServer);
        //NetworkManager.Instance.LaunchGame();
    }

}
