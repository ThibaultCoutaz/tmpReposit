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
        PhotonNetwork.isMessageQueueRunning = true;
        view = GetComponentInParent<PhotonView>();
    }

	void Update () {
        if (hasBall)
        {
            Debug.LogError(PhotonNetwork.isMasterClient+" Has ball");
            GameManager.Instance.ballOfGame.GetComponent<Rigidbody>().useGravity = true;
            GameManager.Instance.ballOfGame.transform.position = mainHand.position;
        }

        if (hasBall && view.isMine)
        {
            if (InputManager.Instance.IsPassing)
            {
                if (PhotonNetwork.offlineMode) //ne va surment jamais étre utiliser car jeu est toujours Online .
                {
                    Debug.LogError("Be carefull you are offLine");
                }
                else
                {
                    ShootBall();
                    //NetworkManager.Instance.TransalteBall(ball,10);
                }
                Debug.LogError(PhotonNetwork.isMasterClient + " thow ball");
            }
        }
	}

    private void ShootBall()
    {
        Debug.Log("Shoot");
        hasBall = false;
        GameManager.Instance.ballOfGame.GetComponent<Rigidbody>().useGravity = false;
        GameManager.Instance.ballOfGame.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }
}
