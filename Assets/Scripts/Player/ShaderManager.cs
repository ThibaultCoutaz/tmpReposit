using UnityEngine;
using System.Collections;

public class ShaderManager : MonoBehaviour {

    private Shader highLight;
    private Shader highLightTarget;
    private Shader normal;

    public GameObject MeshCharacter;

    private TeamScript.Team teamCharacter;

    void Start()
    {
        teamCharacter = PhotonNetwork.player.GetPlayerTeam(); // Initialiser la team puis faire le highLight

        //***********Shado Managing Team***************//
        highLight = Shader.Find("Outlined/Silhouetted Diffuse");
        highLightTarget = Shader.Find("Outlined/Silhouette Only");
        normal = MeshCharacter.GetComponent<Renderer>().material.shader;

        HighLightYourTeam();
    }

    public void ChangeHigleLight(Shader s, TeamScript.Team team = TeamScript.Team.none)
    {
        MeshCharacter.GetComponent<Renderer>().material.shader = s;
        if (s == highLight && team != TeamScript.Team.none)
        {
            MeshCharacter.GetComponent<Renderer>().material.SetFloat("_Outline", 0.003f);
            switch (team)
            {
                case TeamScript.Team.blue:
                    MeshCharacter.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.blue);
                    break;
                case TeamScript.Team.red:
                    MeshCharacter.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.red);
                    break;
            }
        }

    }

    private void HighLightYourTeam()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in players)
        {
            if (go.GetComponent<PhotonView>().owner.GetPlayerTeam() == teamCharacter && !go.GetComponent<PhotonView>().isMine)
            {
                go.GetComponent<ShaderManager>().ChangeHigleLight(highLight, go.GetComponent<ShaderManager>().teamCharacter);
            }
        }
    }

    public void GetTarget(bool target)
    {
        if (target)
        {
            MeshCharacter.GetComponent<Renderer>().material.shader = highLight;
            switch (GetComponent<PhotonView>().owner.GetPlayerTeam())
            {
                case TeamScript.Team.blue:
                    MeshCharacter.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.blue);
                    break;
                case TeamScript.Team.red:
                    MeshCharacter.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.red);
                    break;
            }
        }
        else
        {
            MeshCharacter.GetComponent<Renderer>().material.shader = normal;
        }
    }
}
