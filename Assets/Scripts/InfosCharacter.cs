using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfosCharacter : MonoBehaviour {

    PhotonView view;

    [HideInInspector]
    public TeamScript.Team colorTeam;

    public GameObject switchTeam;

	// Use this for initialization
	void Start () {
        view = GetComponent<PhotonView>();
        view.RPC("InstantiateCharacter", PhotonTargets.OthersBuffered, colorTeam);
        if (view.isMine)
            switchTeam.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        //if (view.isMine)
        //    Debug.LogError("je suis dans l'equipe : " + PhotonNetwork.player.GetTeam() + "----" + view.viewID);
    }
    
    public void ButtonSwitchTeam()
    {
        if(colorTeam == TeamScript.Team.red)
        {
            PhotonNetwork.player.SetPlayerTeam(TeamScript.Team.blue);
            view.RPC("SwitchCharacterTeam", PhotonTargets.AllBuffered, colorTeam);
        }
        else if(colorTeam == TeamScript.Team.blue)
        {
            PhotonNetwork.player.SetPlayerTeam(TeamScript.Team.red);
            view.RPC("SwitchCharacterTeam", PhotonTargets.AllBuffered, colorTeam);
        }
    }


    //RPC function
    [PunRPC]
    private void InstantiateCharacter(TeamScript.Team team)
    {
        if (team == TeamScript.Team.red)
        {
            this.transform.SetParent(CreationRoom.Instance.redTeam);
            this.GetComponent<Image>().color = Color.red;
        }
        else if (team == TeamScript.Team.blue)
        {
            this.transform.SetParent(CreationRoom.Instance.blueTeam);
            this.GetComponent<Image>().color = Color.blue;
        }
        else
        {
           // Debug.LogError("The team " + colorTeam.ToString() + " doesn't exist");
        }
    }

    [PunRPC]
    private void SwitchCharacterTeam(TeamScript.Team team)
    {
        if (team == TeamScript.Team.red)
        {
            this.transform.SetParent(CreationRoom.Instance.blueTeam);
            this.GetComponent<Image>().color = Color.blue;
            colorTeam = TeamScript.Team.blue;
        }
        else if (team == TeamScript.Team.blue)
        {
            this.transform.SetParent(CreationRoom.Instance.redTeam);
            this.GetComponent<Image>().color = Color.red;
            colorTeam = TeamScript.Team.red;
        }
        else
        {
            //Debug.LogError("The team " + colorTeam.ToString() + " doesn't exist");
        }
    }
}
