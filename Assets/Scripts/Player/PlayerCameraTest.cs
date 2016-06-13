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

    private Vector3 offset;

    private InputManager inputs;
    private float zoom = 0;
    public Vector2 zoomMaxMin; 
    public float zoomSpeed = 1f;

    void Awake()
    {
        inputs = InputManager.Instance;
        offset = pivotView.transform.position - transform.position;
    }

    void Update()
    {
        if (!GameManager.Instance.pause) {
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

            transform.position = pivotView.transform.position - (rotation * (offset + offset * zoom));

            transform.LookAt(pivotView);
        }
    }
}
