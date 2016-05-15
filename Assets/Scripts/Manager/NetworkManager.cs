using UnityEngine;
using System;
using System.Collections;

public class NetworkManager : Singleton<NetworkManager>
{

    protected NetworkManager() { }

    [SerializeField]
    private GameObject characterPrefab;
	
    void Start()
    {
        GameObject tmpCharacter = PhotonNetwork.Instantiate("Prefabs/Character/" + characterPrefab.name, characterPrefab.transform.position, Quaternion.identity, 0);
    }

	// Update is called once per frame
	void Update () {
        HUDManager.Instance.EditTextStatus("Status :" + PhotonNetwork.connectionStateDetailed.ToString());
        HUDManager.Instance.EditTextIsMaster("MasterClient : " + PhotonNetwork.isMasterClient);
        HUDManager.Instance.EditTextPing("Ping : " + PhotonNetwork.GetPing() + "ms");
    }






}
