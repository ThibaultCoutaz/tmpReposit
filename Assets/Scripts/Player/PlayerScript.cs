using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    
    public Camera cameraPlayer;

    [SerializeField]
    private Transform mainHand;

    private PhotonView view;
    
    [HideInInspector]
    public bool hasBall = false;
    
	void Start ()
    {
        //PhotonNetwork.isMessageQueueRunning = true;
        view = GetComponentInParent<PhotonView>();
    }

	void Update () {
        //Debug.LogError(hasBall +"--"+ view.isMine +"--"+ GameManager.Instance.ballOfGame.GetComponent<PhotonView>().ownerId +"/"+ GetComponent<PhotonView>().viewID);
        if (hasBall && view.isMine && GameManager.Instance.ballOfGame.GetComponent<PhotonView>().ownerId == GetComponent<PhotonView>().viewID)
        {
            Debug.LogError("Gotit");
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
	}

    [PunRPC]
    private void ShootBall()
    {
        hasBall = false;
        GameManager.Instance.ballOfGame.GetComponent<Rigidbody>().isKinematic = false;
        GameManager.Instance.ballOfGame.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }

    [PunRPC]
    private void CarryBall()
    {
        GameManager.Instance.ballOfGame.transform.position = mainHand.position;
    }
}
