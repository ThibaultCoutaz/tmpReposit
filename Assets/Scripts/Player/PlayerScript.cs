using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    
    public enum stateCharacter
    {
        Normal,
        Stunt,
        slow,
        Dead,
        Dash
    }

    public GameObject[] spellObject;
    private Spell[] spell;

    public stateCharacter currentState;
    public Camera cameraPlayer;
    public float ShootPower = 500;
    public float StartingGold = 250;

    //Spell Part
    public List<Sprite> spell1;
    public List<Sprite> spell2;
    public List<Sprite> spell3;
    public List<Sprite> passif;


    [SerializeField]
    private Transform mainHand;

    private PhotonView view;
    private float currentAmoutOfGold = 0;

    [HideInInspector]
    public bool hasBall = false;
    
	void Start ()
    {
        HUDManager.Instance.DisplayCharacterInfos(true);

        currentState = stateCharacter.Normal;
        HUDManager.Instance.EditState(currentState.ToString());

        currentAmoutOfGold = StartingGold;
        HUDManager.Instance.EditGold(currentAmoutOfGold);
        InvokeRepeating("MoneyInPocket", 1.0f, 1.0f);
        
        view = GetComponentInParent<PhotonView>();

        HUDManager.Instance.InitSpell(spell1, spell2, spell3, passif);
        HUDManager.Instance.DisplaySpell(true);

        spell = new Spell[3];
        spell[0] = new FireBall();

        Debug.LogError("TAMERE"+spell[0]);
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
                    view.RPC("ShootBall", PhotonTargets.AllBuffered);
                }
            }
        }

        if (InputManager.Instance.IsSpellA)
        {
            Debug.LogError("Spell A");
        }
        if (InputManager.Instance.IsSpellE)
        {
            Debug.LogError("Spell E");
        }
        if (InputManager.Instance.IsSpellR)
        {
            Debug.LogError("Spell R");
        }
    }

    void MoneyInPocket()
    {
        currentAmoutOfGold += GameManager.Instance.moneyEarnPerSecond;
        HUDManager.Instance.EditGold(currentAmoutOfGold);
    }

    [PunRPC]
    private void ShootBall()
    {
        hasBall = false;
        GameManager.Instance.ballOfGame.GetComponent<Rigidbody>().isKinematic = false;
        GameManager.Instance.ballOfGame.GetComponent<Rigidbody>().AddForce(cameraPlayer.transform.forward * ShootPower);
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
