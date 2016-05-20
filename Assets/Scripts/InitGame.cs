using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour {

    void Awake()
    {
        Invoke("LaunchGame", 1.0f);
    }

    void LaunchGame()
    {
        DontDestroyOnLoad(HUDManager.Instance.gameObject);
        //PhotonNetwork.isMessageQueueRunning = false;  //a remetre si ca bug 
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.autoCleanUpPlayerObjects = false;
        SceneManager.LoadScene("LobbyMatchMaking");

    }
}
