using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfosCharacter : MonoBehaviour {

    PhotonView view;

    [HideInInspector]
    public TeamScript.Team colorTeam;

    public GameObject switchTeam;
    public Text NamePlayer;

	// Use this for initialization
	void Start () {
        view = GetComponent<PhotonView>();

        if (view.isMine)
        {
            switchTeam.SetActive(true);
            NamePlayer.text = view.owner.name;
        }

        view.RPC("InstantiateCharacter", PhotonTargets.OthersBuffered, colorTeam,view.owner.name);

    }
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.player.isMasterClient && CreationRoom.Instance.viewMaster == null)
            CreationRoom.Instance.viewMaster = view;
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
    private void InstantiateCharacter(TeamScript.Team team, string name)
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
        this.GetComponent<InfosCharacter>().NamePlayer.text = name;
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
    }

    [PunRPC]
    private void LaunchGame()
    {
        DontDestroyOnLoad(HUDManager.Instance.gameObject);
        DontDestroyOnLoad(GameObject.Find("ScriptTeam"));
        SceneManager.LoadScene("Main");
    }

}
