using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {
    
    public enum stateBall
    {
        Free,
        Send,
        Catch
    }

    public stateBall state;

    public Sprite ImgBall;

    private Rigidbody rigb;
    private int nbslowAction; // This is to reduce the dmg if it bounce some other gameObject;

    [HideInInspector]
    public TrailRenderer lineEffect;

    [HideInInspector]
    public int IDSender; //This is to know who send the Ball and so have acces to the sender caracterisitique 

    void Awake()
    {
        state = stateBall.Free;
        rigb = GetComponent<Rigidbody>();
        lineEffect = GetComponent<TrailRenderer>();
        lineEffect.enabled = false;
    }

    //Still need to manage the trail renderer to stop it zhen the magnitude is < to a number.
    void FixedUpdate()
    {
        if (!GetComponent<PhotonView>().isMine)
        {
            syncTime += Time.deltaTime;
            transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        }
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
        if (col.gameObject.GetComponent<PlayerScript>() && state == stateBall.Free)
        {
            state = stateBall.Catch;
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            if (lineEffect.enabled == true)
                lineEffect.enabled = false;
            PlayerScript ps = col.gameObject.GetComponent<PlayerScript>();
            ps.hasBall = true;
            GetComponent<PhotonView>().TransferOwnership(col.gameObject.GetComponent<PhotonView>().viewID);
            rigb.isKinematic = true;
        }
    }
}
