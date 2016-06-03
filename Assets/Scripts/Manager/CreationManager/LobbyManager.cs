using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LobbyManager : MonoBehaviour {

    private string gameVersion = "1.0";

    [SerializeField]InputField playerName;
    [SerializeField]InputField roomName;
    [SerializeField]Text InfoListPlayers;
    [SerializeField]Button JoinRandomRoomB;
    [SerializeField]Text InfoListRooms;
    [SerializeField]Transform ListRooms;
    [SerializeField]GameObject infosRoom;
    [SerializeField]float secondForReload = 5.0f;

    void Start()
    {
        InitRoomName();
    }
    
    //To Init the Room Name
    private void InitRoomName()
    {
        roomName.text = "Room of "+playerName.text;
    }

    //To create a room
    public void CreateARoom()
    {
        PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { maxPlayers = 10 }, null);
    }

    //to join a random room
    public void ClickJoinRandomRoom()
    {
        Debug.Log("joinrandomroom");
        PhotonNetwork.JoinRandomRoom();
    }

    bool reload = true;

    void Update()
    {
        if (PhotonNetwork.countOfRooms <= 0)
        {
            JoinRandomRoomB.enabled = false;
            JoinRandomRoomB.image.color = Color.gray;
        }
        else
        {
            JoinRandomRoomB.enabled = true;
            JoinRandomRoomB.image.color = Color.white;
        }


        InfoListPlayers.text = PhotonNetwork.countOfPlayers + " users are online, " + PhotonNetwork.countOfPlayersInRooms + " users are online in a Room";
        InfoListRooms.text = PhotonNetwork.countOfRooms + " rooms available";

        if (reload)
        {
            Invoke("ManagerListRoom", secondForReload);
            reload = false;
        }
        
    }

    private void ManagerListRoom()
    {
        if(PhotonNetwork.GetRoomList().Length != 0)
        {
            ListRooms.GetComponent<RectTransform>().sizeDelta = new Vector2(ListRooms.GetComponent<RectTransform>().rect.width, infosRoom.GetComponent<RectTransform>().rect.height * PhotonNetwork.GetRoomList().Length);

            foreach (Transform child in ListRooms)
            {
                Destroy(child.gameObject);
            }

            foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
            {
                GameObject tmp = (GameObject)Instantiate(infosRoom);
                tmp.GetComponent<InfosRoom>().EditServeurName(roomInfo.name);
                tmp.GetComponent<InfosRoom>().EditNumberPlayers(roomInfo.playerCount.ToString());
                tmp.transform.SetParent(ListRooms);
                tmp.transform.localScale = new Vector3(1, 1, 1); // NEed to find a solution later ! it's not beautiful to set the scale after setting the parent
            }
        }
        reload = true;
    }

    public void ReloadManually()
    {
        ManagerListRoom();
    }

    //**********************************//

    public void LaunchRoom()
    {
        DontDestroyOnLoad(HUDManager.Instance.gameObject);
        SceneManager.LoadScene("CreationRoom");

    }

    //***Methode call back from Photon***//

    void OnCreateRoom()
    {
        LaunchRoom();
    }

    void OnJoinedRoom()
    {
        LaunchRoom();
        Debug.Log("You join a Room !");
    }

    void OnPhotonJoinRoomFailed()
    {
        Debug.Log("can't join a random room");
    }
}
