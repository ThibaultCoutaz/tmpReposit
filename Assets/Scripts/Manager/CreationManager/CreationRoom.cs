using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreationRoom : MonoBehaviour {
    
    public GameObject infosPlayer;
    public Transform redTeam;
    public Transform blueTeam;

	// Use this for initialization
	void Start () {
        if (PhotonNetwork.isMasterClient)
        {
            GameObject tmp = PhotonNetwork.InstantiateSceneObject("Prefabs/UI/" + infosPlayer.name, new Vector3(0, 0, 0), Quaternion.identity, 0, null);
            tmp.transform.SetParent(redTeam);
            tmp.GetComponent<Image>().color = Color.red;
            PhotonNetwork.player.SetTeam(PunTeams.Team.red);
        }
        else
        {
            ChooseTeam();
        }
    }

    private int nbRed = 0;
    private int nbBlue = 0;

    private void ChooseTeam()
    {
        //We start with Red
        List<PhotonPlayer> listPlayer;
        if(PunTeams.PlayersPerTeam.TryGetValue(PunTeams.Team.red,out listPlayer))
        {
            Debug.Log("someOne in the red team");
            foreach(PhotonPlayer p in listPlayer)
            {
                nbRed++;
            }
        }
        else
        {
            Debug.Log("NoOne in the red team");
        }


        //Then Blue Team
        if (PunTeams.PlayersPerTeam.TryGetValue(PunTeams.Team.blue, out listPlayer))
        {
            Debug.Log("someOne in the blue team");
            foreach (PhotonPlayer p in listPlayer)
            {
                nbBlue++;
            }
        }
        else
        {
            Debug.Log("NoOne in the blue team");
        }

        if(nbRed > nbBlue)
        {
            Debug.Log("lets go in the blue team");
            PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
        }else
        {
            Debug.Log("lets go in the red team");
            PhotonNetwork.player.SetTeam(PunTeams.Team.red);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
