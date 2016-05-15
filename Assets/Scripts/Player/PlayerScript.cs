using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    
    public Camera cameraPlayer;

    [SerializeField]
    private Transform mainHand;

    private PhotonView view;

    [HideInInspector]
    public bool hasBall;

    [HideInInspector]
    public GameObject ball;
    
	void Start ()
    {
        PhotonNetwork.isMessageQueueRunning = true;
        hasBall = false;
        view = GetComponentInParent<PhotonView>();
    }

	void Update () {
        if (hasBall)
        {
            ball.transform.position = mainHand.position;
        }

        if (hasBall && view.isMine)
        {
            if (InputManager.Instance.IsPassing)
            {
                hasBall = false;
                if (PhotonNetwork.offlineMode) //ne va surment jamais étre utiliser car jeu est toujours Online .
                {
                    Debug.LogError("Be carefull you are offLine");
                    ShootBall(10.0f);
                }
                else
                {
                    view.RPC("ShootBall",
                            PhotonTargets.All,
                            new object[] {10.0f}
                            );
                }
            }
        }
	}

    [PunRPC]
    private void ShootBall(float power)
    {
        ball.GetComponent<Rigidbody>().velocity = transform.forward * power;
    }
}
