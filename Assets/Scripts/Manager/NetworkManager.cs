using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : Singleton<NetworkManager>
{

    protected NetworkManager() { }

    [SerializeField]
    private GameObject characterPrefab;

    public float spawn_ray = 1;

    public GameObject Ball;


    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
             GameManager.ballOfGame = PhotonNetwork.InstantiateSceneObject("Prefabs/Objects/" + Ball.name, new Vector3(0, 10, 0), Quaternion.identity, 0,null);
        }
        if(PhotonNetwork.player.GetPlayerTeam() == TeamScript.Team.red)
        {
            PhotonNetwork.Instantiate("Prefabs/Character/" + characterPrefab.name, GameManager._redSpawn.position + new Vector3(Random.Range(0f, spawn_ray), 0, Random.Range(0f, spawn_ray)), Quaternion.identity, 0);
        }
        else if(PhotonNetwork.player.GetPlayerTeam() == TeamScript.Team.blue)
        {
            PhotonNetwork.Instantiate("Prefabs/Character/" + characterPrefab.name, GameManager._blueSpawn.position + new Vector3(Random.Range(0f, spawn_ray), 0, Random.Range(0f, spawn_ray)), Quaternion.identity, 0);
        }

    }

	// Update is called once per frame
	void Update () {
        HUDManager.Instance.EditTextStatus("Status :" + PhotonNetwork.connectionStateDetailed.ToString());
        HUDManager.Instance.EditTextIsMaster("MasterClient : " + PhotonNetwork.isMasterClient);
        HUDManager.Instance.EditTextPing("Ping : " + PhotonNetwork.GetPing() + "ms");
    }

    public GameObject FindLocalPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in players)
        {
            if (go.GetComponent<PhotonView>().isMine)
            {
                return go;
            }
        }
        return null;
    }
}
