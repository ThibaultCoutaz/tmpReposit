using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Game;

public class PlayerScript : MonoBehaviour {

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

    
	void Start ()
    {
        nameCharacter = PhotonNetwork.player.name;
        name = nameCharacter;

        _currentLife = maxLife;

        Cursor.lockState = CursorLockMode.Locked;
        HUDManager.Instance.DisplayCharacterInfos(true);

        currentState = stateCharacter.Normal;
        HUDManager.Instance.EditState(currentState.ToString());

        currentAmoutOfGold = StartingGold;
        HUDManager.Instance.EditGold(currentAmoutOfGold);
        InvokeRepeating("MoneyInPocket", 1.0f, 1.0f);
        
        view = GetComponentInParent<PhotonView>();

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
