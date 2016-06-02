using UnityEngine;
using System.Collections;

public class PlayerCameraTest : MonoBehaviour {

    public Transform player;
    public Transform pivotPlayer;
    public float rotateSpeed = 5;

    private Vector3 offset;
    //private Vector3 target;

    private InputManager inputs;

    void Awake()
    {
        inputs = InputManager.Instance;
        offset = player.transform.position - transform.position;
        //target = pivotPlayer.transform.position + pivotPlayer.transform.forward /** TargetPlayerRadius*/;
    }

    void Update()
    {
        float horizontal = GameObject.Find("InputManager").GetComponent<InputManager>().GetHorizontalMouse() * rotateSpeed; // DEEEEEEEEEEGEUX !
        player.transform.Rotate(0, horizontal, 0);

        float desiredAngle = player.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = player.transform.position - (rotation * offset);

        transform.LookAt(player.transform.position + player.transform.forward*2);
    }
}
