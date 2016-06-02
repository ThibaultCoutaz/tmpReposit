using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : Singleton<NetworkManager>
{

    protected NetworkManager() { }

    [SerializeField]
    private GameObject characterPrefab;

    public Transform redSpawn;
    public Transform blueSpawn;

    public float spawn_ray = 1;

    public GameObject Ball;


    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
             GameManager.Instance.ballOfGame = PhotonNetwork.InstantiateSceneObject("Prefabs/Objects/" + Ball.name, new Vector3(0, 10, 0), Quaternion.identity, 0,null);
        }
        if(PhotonNetwork.player.GetPlayerTeam() == TeamScript.Team.red)
        {
            GameObject tmpCharacter = PhotonNetwork.Instantiate("Prefabs/Character/" + characterPrefab.name, redSpawn.position + new Vector3(Random.Range(0f, spawn_ray), 0, Random.Range(0f, spawn_ray)), Quaternion.identity, 0);
        }
        else if(PhotonNetwork.player.GetPlayerTeam() == TeamScript.Team.blue)
        {
            GameObject tmpCharacter = PhotonNetwork.Instantiate("Prefabs/Character/" + characterPrefab.name, blueSpawn.position + new Vector3(Random.Range(0f, spawn_ray), 0, Random.Range(0f, spawn_ray)), Quaternion.identity, 0);
        }

    }

	// Update is called once per frame
	void Update () {
        HUDManager.Instance.EditTextStatus("Status :" + PhotonNetwork.connectionStateDetailed.ToString());
        HUDManager.Instance.EditTextIsMaster("MasterClient : " + PhotonNetwork.isMasterClient);
        HUDManager.Instance.EditTextPing("Ping : " + PhotonNetwork.GetPing() + "ms");
    }
}
