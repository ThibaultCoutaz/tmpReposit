using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Game;

public class PlayerScript : MonoBehaviour {

    [HideInInspector]
    public string nameCharacter;
    [HideInInspector]
    public TeamScript.Team teamCharacter;

    public enum stateCharacter
    {
        Normal,
        Stunt,
        slow,
        Dead,
        Dash
    }

    //Variable about Life
    public float maxLife = 500f;
    private float _currentLife;
    public float currentLife
    {
        get { return _currentLife; }
    }
    //**************************//

    private Shader highLight;
    private Shader normal;
    public GameObject MeshCharacter;

    public stateCharacter currentState;
    public Camera cameraPlayer;
    public float ShootPower = 500;
    public float StartingGold = 250;
    [SerializeField]private Transform mainHand;

    [HideInInspector]
    public bool hasBall = false;

    private PhotonView view;
    private float currentAmoutOfGold = 0;
    
	void Start ()
    {
        view = GetComponentInParent<PhotonView>();

        nameCharacter = view.owner.name;
        name = nameCharacter;

        teamCharacter = PhotonNetwork.player.GetPlayerTeam(); // Initialiser la team puis faire le highLight

        _currentLife = maxLife;

        Cursor.lockState = CursorLockMode.Locked;
        HUDManager.Instance.DisplayCharacterInfos(true);

        currentState = stateCharacter.Normal;
        HUDManager.Instance.EditState(currentState.ToString());

        currentAmoutOfGold = StartingGold;
        HUDManager.Instance.EditGold(currentAmoutOfGold);
        InvokeRepeating("MoneyInPocket", 1.0f, 1.0f);
        

        //***********Shado Managing Team***************//
        highLight = Shader.Find("Outlined/Silhouetted Diffuse");
        normal = MeshCharacter.GetComponent<Renderer>().material.shader;

        HighLightTeam();
        Debug.LogError("Passage dans le Start");
    }

    public void ChangeHigleLight(Shader s, TeamScript.Team team = TeamScript.Team.none)
    {
        MeshCharacter.GetComponent<Renderer>().material.shader = s;
        if(s == highLight && team != TeamScript.Team.none)
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

    private void HighLightTeam()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject go in players)
        {
            if (go.GetComponent<PhotonView>().owner.GetPlayerTeam() == teamCharacter && !go.GetComponent<PlayerScript>().view.isMine)
            {
                Debug.LogError("player = " + go.GetComponent<PlayerScript>().nameCharacter + "--- Team = " + go.GetComponent<PlayerScript>().teamCharacter);
                go.GetComponent<PlayerScript>().ChangeHigleLight(highLight, go.GetComponent<PlayerScript>().teamCharacter);
            }
        }
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

    void Update () {
        if (hasBall && view.isMine && GameManager.Instance.ballOfGame.GetComponent<PhotonView>().ownerId == GetComponent<PhotonView>().viewID)
        {
            view.RPC("CarryBall", PhotonTargets.AllBuffered);

            if (InputManager.Instance.IsPassing)
            {
                if (PhotonNetwork.offlineMode) //ne va surment jamais étre utiliser car jeu est toujours Online .
                {
                    Debug.LogError("Be carefull you are offLine");
                }
                else
                {
                    view.RPC("ShootBall", PhotonTargets.AllBuffered, cameraPlayer.transform.forward);
                }
            }
        }

        if (InputManager.Instance.IsCancelling && view.isMine)
        {
            GameManager.Instance.GamePause();
        }

    }

    private void MoneyInPocket()
    {
        currentAmoutOfGold += GameManager.Instance.moneyEarnPerSecond;
        HUDManager.Instance.EditGold(currentAmoutOfGold);
    }

    [PunRPC]
    private void ShootBall(Vector3 camF) // for the futur is better to sync the camera rotation i think in Y that send the vector ( for exemple for the head movement )
    {
        hasBall = false;
        GameManager.Instance.ballOfGame.GetComponent<Rigidbody>().isKinematic = false;
        GameManager.Instance.ballOfGame.GetComponent<Rigidbody>().AddForce(camF * ShootPower);
        GameManager.Instance.ballOfGame.GetComponent<BallBehaviour>().lineEffect.enabled = true;
        Invoke("ResetOwnerBall", 0.5f);
    }


    private void ResetOwnerBall()
    {
        if (GameManager.Instance.ballOfGame != null)
        {
            GameManager.Instance.ballOfGame.GetComponent<BallBehaviour>().IDPreviousOwner = -1;
            Physics.IgnoreCollision(GameManager.Instance.ballOfGame.GetComponent<Collider>(), GetComponent<Collider>(),false);
        }
    }

    [PunRPC]
    private void CarryBall()
    {
        if (GameManager.Instance.ballOfGame != null)
            GameManager.Instance.ballOfGame.transform.position = mainHand.position;
    }

    ////To see the direction of the shoot
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(cameraPlayer.transform.position, cameraPlayer.transform.forward * ShootPower);
    //}

    public void EditState(stateCharacter _state)
    {
        currentState = _state;
        HUDManager.Instance.EditState(currentState.ToString());
    }
}
