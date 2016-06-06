using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {

    private Rigidbody rigb;

    [HideInInspector]
    public int IDPreviousOwner; //could do it with some state like , catch , free , and owned

    void Start()
    {
        IDPreviousOwner = -1;
        rigb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!GetComponent<PhotonView>().isMine)
        {
            syncTime += Time.deltaTime;
            transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        }
    }

    //[PunRPC]
    //void BallMove()
    //{
    //    syncTime += Time.deltaTime;
    //    transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    //}

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
        }
        else
        {
            syncPositionBall = (Vector3)stream.ReceiveNext();
            syncVelocityBall = (Vector3)stream.ReceiveNext();

            syncTime = 0;
            syncDelay = Time.time - lastSyncTime;
            lastSyncTime = Time.time;

            syncEndPosition = syncPositionBall + syncVelocityBall * syncDelay;
            syncStartPosition = transform.position;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("IDOwner = " + IDPreviousOwner);
        if (col.gameObject.GetComponent<PlayerScript>() && IDPreviousOwner != col.gameObject.GetComponent<PhotonView>().viewID)
        {
            //Debug.LogError("Gotit");
            IDPreviousOwner = col.gameObject.GetComponent<PhotonView>().viewID;
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            //GetComponent<Collider>().enabled = false;
            PlayerScript ps = col.gameObject.GetComponent<PlayerScript>();
            ps.hasBall = true;
            GetComponent<PhotonView>().TransferOwnership(col.gameObject.GetComponent<PhotonView>().viewID);
            rigb.isKinematic = true;
        }
    }
}
