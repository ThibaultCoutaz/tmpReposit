using UnityEngine;
using System.Collections;

public class PlayerCameraTest : MonoBehaviour {

    public Transform player;
    public Transform pivotView;
    public float sensibilityX = 5;
    public float sensibilityY = 5;

    float verticale = 0f;
    public float minimumY = -30F;
    public float maximumY = 30F;
    public float SmoothTime = 0.2f;
    public Vector2 zoomMaxMin;
    public float zoomSpeed = 1f;

    private float zoom = 0;
    private Vector3 offset;
    private InputManager inputs;
    private Vector3 velocity = Vector3.zero;


    void Awake()
    {
        inputs = InputManager.Instance;
        offset = pivotView.transform.position - transform.position;
    }

    void Update()
    {
        if (!GameManager.pause) {
            float horizontal = InputManager.Instance.GetHorizontalMouse() * sensibilityY; 
            verticale += InputManager.Instance.GetVerticalMouse() * sensibilityX;
            verticale = Mathf.Clamp(verticale, minimumY, maximumY);

            //For the rotation in Y of the Player
            player.transform.Rotate(0, horizontal, 0);
            float desiredAngleY = player.transform.eulerAngles.y;

            Quaternion rotation = Quaternion.Euler(-verticale, desiredAngleY, 0);

            //For the Zoom
            zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            zoom = Mathf.Clamp(zoom, zoomMaxMin.x, zoomMaxMin.y);

            Vector3 tmpPosition = pivotView.transform.position - (rotation * (offset + offset * zoom));

            transform.position = Vector3.SmoothDamp(transform.position, tmpPosition,ref velocity, SmoothTime);

            transform.LookAt(pivotView);
        }
    }
}
