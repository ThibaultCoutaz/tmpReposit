using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : Singleton<NetworkManager>
{

    protected NetworkManager() { }

    [SerializeField]
    private GameObject characterPrefab;

    public GameObject Ball;

    private PhotonView view;

    //Dictionary<string, GameObject> _objects;

    void Start()
    {
        //GameObject[] objects = Resources.LoadAll<GameObject>("Objects");

        //_objects = new Dictionary<string, GameObject>();

        //foreach (GameObject go in objects)
        //    _objects.Add(go.name, go);

        view = GetComponent<PhotonView>();
        if (PhotonNetwork.isMasterClient)
        {
             GameManager.Instance.ballOfGame = PhotonNetwork.InstantiateSceneObject("Prefabs/Objects/" + Ball.name, new Vector3(0, 10, 0), Quaternion.identity, 0,null);
        }

        GameObject tmpCharacter = PhotonNetwork.Instantiate("Prefabs/Character/" + characterPrefab.name, characterPrefab.transform.position, Quaternion.identity, 0);
    }

	// Update is called once per frame
	void Update () {
        HUDManager.Instance.EditTextStatus("Status :" + PhotonNetwork.connectionStateDetailed.ToString());
        HUDManager.Instance.EditTextIsMaster("MasterClient : " + PhotonNetwork.isMasterClient);
        HUDManager.Instance.EditTextPing("Ping : " + PhotonNetwork.GetPing() + "ms");
    }

    [PunRPC]
    private void RPCTranslateBall(GameObject ball,float power)
    {
        ball.GetComponent<Rigidbody>().velocity = transform.forward * power;
    }

    public void TransalteBall(string ball ,float power)
    {
        view.RPC("RPCTranslateBall",
        PhotonTargets.All,
        new object[] { ball,power }
        );
    }
}
