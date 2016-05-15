using UnityEngine;
using System.Collections;

public class PlayerNetworkSetup : MonoBehaviour {

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    GameObject Character;
    //[SerializeField]
    //PlayerScript playerScript;
    //[SerializeField]
    //PlayerUserControl playerController;

    //public Vector3 spawn_pos;
    //public float spawn_ray;
    public Vector3 movement { get; private set; }

    private PhotonView view;
    private Vector3 m_Forward; // The current forward direction of the camera
    private Rigidbody rigb;
    private PlayerCamera camScript;
    private CharacterControllerMoba characterControl;


    void Awake()
    {
        view = GetComponentInParent<PhotonView>();
        rigb = Character.GetComponent<Rigidbody>();
        characterControl = Character.GetComponent<CharacterControllerMoba>(); 

        if (playerCamera != null)
            camScript = playerCamera.GetComponent<PlayerCamera>();

        if (GetComponent<PhotonView>().isMine)
        {
            playerCamera.SetActive(true);
            //playerScript.Init();
            //playerController.Init();
            //transform.position = spawn_pos + new Vector3(Random.Range(0f, spawn_ray), 0, Random.Range(0f, spawn_ray));
        }
        else
        {
            //playerScript.enabled = false;
            //playerController.enabled = false;
        }
    }

    void Update()
    {
        if (view.isMine)
        {
            // Calculate relative direction to move
            m_Forward = Vector3.Scale(camScript.TargetDir, new Vector3(1, 0, 1)).normalized;

            rigb.velocity = (characterControl.movement.z * m_Forward + Quaternion.Euler(0f, 90f, 0f) * m_Forward * characterControl.movement.x) * speed;
        }
        else
        {
            SyncMovement();
        }
    }

    private void SyncMovement()
    {
        syncTime += Time.deltaTime;
        transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
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
        Vector3 syncVelocity = Vector3.zero;

        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(rigb.velocity);
        }
        else
        {
            syncPosition = (Vector3)stream.ReceiveNext();
            syncVelocity = (Vector3)stream.ReceiveNext();

            syncTime = 0;
            syncDelay = Time.time - lastSyncTime;
            lastSyncTime = Time.time;
            syncStartPosition = transform.position;

            syncEndPosition = syncPosition + syncVelocity * syncDelay;
            syncStartPosition = transform.position;
        }
    }
}
