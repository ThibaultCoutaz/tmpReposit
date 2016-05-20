using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {
    
    private Rigidbody rigb;

    void Start()
    {
        rigb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!GetComponent<PhotonView>().isMine)
        {
            syncTime += Time.deltaTime;
            transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        }
        Debug.LogError(GetComponent<PhotonView>().ownerId+"---"+ GameManager.Instance.ballOfGame.GetComponent<PhotonView>().ownerId);
    }

    private float lastSyncTime = 0;
    private float syncDelay = 0;
    private float syncTime = 0;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo message)
    {
        Vector3 syncPositionBall = Vector3.zero;
        Vector3 syncVelocityBall = Vector3.zero;

        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(rigb.velocity);
            stream.SendNext(rigb.useGravity);
            //stream.SendNext(GetComponent<PhotonView>().ownerId);
        }
        else
        {
            syncPositionBall = (Vector3)stream.ReceiveNext();
            syncVelocityBall = (Vector3)stream.ReceiveNext();
            rigb.useGravity = (bool)stream.ReceiveNext();
            //GetComponent<PhotonView>().ownerId = (int)stream.ReceiveNext();

            syncTime = 0;
            syncDelay = Time.time - lastSyncTime;
            lastSyncTime = Time.time;

            syncEndPosition = syncPositionBall + syncVelocityBall * syncDelay;
            syncStartPosition = transform.position;
        }
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.GetComponent<PlayerScript>())
        {
            PlayerScript ps = col.gameObject.GetComponent<PlayerScript>();
            ps.hasBall = true;
            GetComponent<PhotonView>().TransferOwnership(col.gameObject.GetComponent<PhotonView>().viewID);
            rigb.useGravity = false;
        }
    }
}
