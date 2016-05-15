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
        DontDestroyOnLoad(InputManager.Instance.gameObject);
        //PhotonNetwork.isMessageQueueRunning = false;  //a remetre si ca bug 
        SceneManager.LoadScene("LobbyMatchMaking");

    }
}
