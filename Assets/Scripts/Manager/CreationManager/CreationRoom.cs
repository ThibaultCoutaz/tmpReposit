using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreationRoom : Singleton<CreationRoom>
{

    protected CreationRoom() { }
    
    public GameObject infosPlayer;
    public Transform redTeam;
    public Transform blueTeam;
    public Button bPlay;

    [HideInInspector]
    public PhotonView viewMaster;

	// Use this for initialization
	void Start ()
    {
        if (PhotonNetwork.player.isMasterClient)
            bPlay.interactable = true;

        InitTeam();

        ChooseTeam();
    }

    private void InitTeam()
    {
        Array enumVals = Enum.GetValues(typeof(TeamScript.Team));
        foreach (var enumVal in enumVals)
        {
            TeamScript.PlayersPerTeam[(TeamScript.Team)enumVal].Clear();
        }

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            PhotonPlayer player = PhotonNetwork.playerList[i];
            TeamScript.Team playerTeam = player.GetPlayerTeam();
            TeamScript.PlayersPerTeam[playerTeam].Add(player);
        }
    }

    private void ChooseTeam()
    {    
        int nbRed = 0;
        int nbBlue = 0;
        //We start with Red
        List<PhotonPlayer> listPlayer;
        if(TeamScript.PlayersPerTeam.TryGetValue(TeamScript.Team.red,out listPlayer))
        {
            foreach(PhotonPlayer p in listPlayer)
                nbRed++;
        }


        //Then Blue Team
        if (TeamScript.PlayersPerTeam.TryGetValue(TeamScript.Team.blue, out listPlayer))
        {
            foreach (PhotonPlayer p in listPlayer)
                nbBlue++;
        }

        if(nbRed > nbBlue && nbBlue < 5)
        {
            GameObject tmp = PhotonNetwork.Instantiate("Prefabs/UI/" + infosPlayer.name, new Vector3(0, 0, 0), Quaternion.identity, 0, null);
            tmp.GetComponent<InfosCharacter>().colorTeam = TeamScript.Team.blue;
            tmp.transform.SetParent(blueTeam);
            tmp.GetComponent<Image>().color = Color.blue;
            PhotonNetwork.player.SetPlayerTeam(TeamScript.Team.blue);
        }
        else if(nbRed < 5)
        {
            GameObject tmp = PhotonNetwork.Instantiate("Prefabs/UI/" + infosPlayer.name, new Vector3(0, 0, 0), Quaternion.identity, 0, null);
            tmp.GetComponent<InfosCharacter>().colorTeam = TeamScript.Team.red;
            tmp.transform.SetParent(redTeam);
            tmp.GetComponent<Image>().color = Color.red;
            PhotonNetwork.player.SetPlayerTeam(TeamScript.Team.red);
        }
        else
        {
            Debug.Log("a bah la tu es perdu jeune fou il n'y a plus de place ");
        }
    }

	// Update is called once per frame
	void Update () {

        //DisplayTeam();
	}

    public void Play()
    {
        PhotonNetwork.room.visible = false;
        viewMaster.RPC("LaunchGame", PhotonTargets.AllBuffered);
    }

    private void DisplayTeam()
    {
        int nbRed = 0;
        int nbBlue = 0;
        //We start with Red
        List<PhotonPlayer> listPlayer;
        if (TeamScript.PlayersPerTeam.TryGetValue(TeamScript.Team.red, out listPlayer))
        {
            foreach (PhotonPlayer p in listPlayer)
            {
                nbRed++;
            }
        }


        //Then Blue Team
        if (TeamScript.PlayersPerTeam.TryGetValue(TeamScript.Team.blue, out listPlayer))
        {
            foreach (PhotonPlayer p in listPlayer)
            {
                nbBlue++;
            }
        }

        Debug.LogError("Team Red = " + nbRed + "/" + "Team Blue = " + nbBlue);
    }


    //When the master client switch
    void OnMasterClientSwitched(PhotonPlayer newMaster)
    {
        if (PhotonNetwork.isMasterClient)
        {
            bPlay.interactable = true;
        }
    }
}
