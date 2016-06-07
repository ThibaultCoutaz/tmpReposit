using UnityEngine;
using System.Collections;

public class PlayerNetworkSetup : MonoBehaviour {

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    GameObject playerCamera;
    

    public Vector3 movement { get; private set; }

    private PhotonView view;
    private Vector3 m_Forward; // The current forward direction of the camera
    private Rigidbody rigb;
    private PlayerCamera camScript;
    private Animator m_animator;

    //For the RolePlay
    private float money = 0f;

    void Awake()
    {
        view = GetComponentInParent<PhotonView>();
        rigb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();

        if (playerCamera != null)
            camScript = playerCamera.GetComponent<PlayerCamera>();

        if (view.isMine)
        {
            playerCamera.SetActive(true);
        }
    }

    void Update()
    {
        if (m_animator == null)
            m_animator = GetComponent<Animator>();

        if (!view.isMine)
        {
            SyncMovement();
        }
    }

    private void SyncMovement()
    {
        syncTime += Time.deltaTime;
        transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        transform.rotation = Quaternion.Lerp(transform.rotation, syncEndRotation, syncTime / syncDelay);
    }

    private float lastSyncTime = 0;
    private float syncDelay = 0;
    private float syncTime = 0;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;
    private Quaternion syncEndRotation = Quaternion.identity;

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo message)
    {
        Vector3 syncPosition = Vector3.zero;
        Quaternion syncRotation = Quaternion.identity;
        Vector3 syncVelocity = Vector3.zero;

        if (stream.isWriting)
        {
            //Pensez a faire la rotation
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(rigb.velocity);
            stream.SendNext(m_animator.GetFloat("Magnitude"));
        }
        else
        {
            syncPosition = (Vector3)stream.ReceiveNext();
            syncRotation = (Quaternion)stream.ReceiveNext();
            syncVelocity = (Vector3)stream.ReceiveNext();
            m_animator.SetFloat("Magnitude", (float)stream.ReceiveNext());

            syncTime = 0;
            syncDelay = Time.time - lastSyncTime;
            lastSyncTime = Time.time;
            syncStartPosition = transform.position;
            syncEndRotation = syncRotation;

            syncEndPosition = syncPosition + syncVelocity * syncDelay;
            syncStartPosition = transform.position;
        }
    }
}
