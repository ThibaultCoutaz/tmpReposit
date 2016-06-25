using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Game;

public class PlayerScript : MonoBehaviour
{

    public bool Debuging = true;
    [HideInInspector]
    public string nameCharacter;

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

    public stateCharacter currentState;
    public Camera cameraPlayer;
    public float ShootPower = 500;
    public float StartingGold = 250;
    [SerializeField]private Transform mainHand;

    [HideInInspector]
    public bool hasBall = false;

    private PhotonView view;
    private float currentAmoutOfGold = 0;

    private Transform posSpawn;
    
	void Start ()
    {

        if (Debuging)
        {
            HUDManager.Instance.InitDebug(this);
            HUDManager.Instance.DisplayDebuging(true);
        }


        view = GetComponentInParent<PhotonView>();

        nameCharacter = view.owner.name;
        name = nameCharacter;

      
        _currentLife = maxLife;

        Cursor.lockState = CursorLockMode.Locked;
        HUDManager.Instance.DisplayCharacterInfos(true);

        currentState = stateCharacter.Normal;
        HUDManager.Instance.EditState(currentState.ToString());

        currentAmoutOfGold = StartingGold;
        HUDManager.Instance.EditGold(currentAmoutOfGold);
        InvokeRepeating("MoneyInPocket", 1.0f, 1.0f);

        HUDManager.Instance.EditGetBall(GameManager.ballOfGame.GetComponent<BallBehaviour>().ImgBall);

        if(PhotonNetwork.player.GetPlayerTeam() == TeamScript.Team.red)
        {
            posSpawn = GameManager._redSpawn;
        }
        else
        {
            posSpawn = GameManager._blueSpawn;
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

    private bool displayBall = false;

    void Update () {
        if (currentState != stateCharacter.Dead)
        {

            if (_currentLife <= 0)
            {
                EditState(stateCharacter.Dead);
            }

            if (hasBall && view.isMine && GameManager.ballOfGame.GetComponent<PhotonView>().ownerId == GetComponent<PhotonView>().viewID)
            {
                if (!displayBall)
                {
                    HUDManager.Instance.DisplayGetBall(true);
                    displayBall = true;
                }

                view.RPC("CarryBall", PhotonTargets.AllBuffered);

                if (InputManager.Instance.IsPassing)
                {
                    if (PhotonNetwork.offlineMode) //ne va surment jamais étre utiliser car jeu est toujours Online .
                    {
                        Debug.LogError("Be carefull you are offLine");
                    }
                    else
                    {
                        HUDManager.Instance.DisplayGetBall(false);
                        displayBall = false;
                        view.RPC("ShootBall", PhotonTargets.AllBuffered, cameraPlayer.transform.forward);
                    }
                }
            }

            if (InputManager.Instance.IsCancelling && view.isMine)
            {
                GameManager.GamePause();
            }
        }
        else
        {
            transform.position = posSpawn.position;//He is Dead ! AU REVOIR 
            EditState(stateCharacter.Normal);
            _currentLife = maxLife;
        }

    }

    private void MoneyInPocket()
    {
        //currentAmoutOfGold += GameManager.Instance.moneyEarnPerSecond;
        HUDManager.Instance.EditGold(currentAmoutOfGold);
    }

    [PunRPC]
    private void ShootBall(Vector3 camF) // for the futur is better to sync the camera rotation i think in Y that send the vector ( for exemple for the head movement )
    {
        hasBall = false;
        GameManager.ballOfGame.GetComponent<Rigidbody>().isKinematic = false;
        GameManager.ballOfGame.GetComponent<Rigidbody>().AddForce(camF * ShootPower);
        GameManager.ballOfGame.GetComponent<BallBehaviour>().lineEffect.enabled = true;
        Invoke("ResetOwnerBall", 0.5f);
    }


    private void ResetOwnerBall()
    {
        if (GameManager.ballOfGame != null)
        {
            GameManager.ballOfGame.GetComponent<BallBehaviour>().IDPreviousOwner = -1;
            Physics.IgnoreCollision(GameManager.ballOfGame.GetComponent<Collider>(), GetComponent<Collider>(),false);
        }
    }

    [PunRPC]
    private void CarryBall()
    {
        if (GameManager.ballOfGame != null)
            GameManager.ballOfGame.transform.position = mainHand.position;
    }

    ////To see the direction of the shoot
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(cameraPlayer.transform.position, cameraPlayer.transform.forward * ShootPower);
    //}

    public void EditLife(float amount)
    {
        _currentLife += amount;
    }

    public void EditState(stateCharacter _state)
    {
        currentState = _state;
        HUDManager.Instance.EditState(currentState.ToString());
    }
}
