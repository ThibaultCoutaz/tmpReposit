using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Game;

public class PlayerScript : MonoBehaviour {

    public enum stateCharacter
    {
        Normal,
        Stunt,
        slow,
        Dead,
        Dash
    }
   
    public Spell[] spells = new Spell[3];

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
        Cursor.lockState = CursorLockMode.Locked;
        HUDManager.Instance.DisplayCharacterInfos(true);

        currentState = stateCharacter.Normal;
        HUDManager.Instance.EditState(currentState.ToString());

        currentAmoutOfGold = StartingGold;
        HUDManager.Instance.EditGold(currentAmoutOfGold);
        InvokeRepeating("MoneyInPocket", 1.0f, 1.0f);
        
        view = GetComponentInParent<PhotonView>();
        spells[0].id = 0;
        spells[1].id = 1;
        spells[2].id = 2;
        HUDManager.Instance.InitSpell(spells[0].spriteSpell, spells[1].spriteSpell, spells[2].spriteSpell,null);
        HUDManager.Instance.DisplaySpell(true);

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
        
        for (int i = 0; i < 3; i++)
        {
            if (!spells[i].canCast)
                spells[i].ManageFilledSpell();
        }

        if (InputManager.Instance.IsSpellA)
        {
            if(spells[0].canCast)
                spells[0].OnCast(this);
        }
        if (InputManager.Instance.IsSpellE)
        {
            if (spells[1].canCast)
                spells[1].OnCast(this);
        }
        if (InputManager.Instance.IsSpellR)
        {
            if (spells[2].canCast)
                spells[2].OnCast(this);
        }
    }

    void MoneyInPocket()
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

    //To see the direction of the shoot
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(cameraPlayer.transform.position, cameraPlayer.transform.forward * ShootPower);
    }

    public void EditState(stateCharacter _state)
    {
        currentState = _state;
        HUDManager.Instance.EditState(currentState.ToString());
    }
}
