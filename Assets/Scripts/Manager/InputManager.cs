using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager>
{
    protected InputManager() { }

    public bool IsRunning { get; private set; }
    public bool IsWalking { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsCrouching { get; private set; }
    public bool IsPassing { get; private set; }
    public bool IsZooming { get; private set; }
    public bool IsCancelling { get; private set; }
    public bool IsInGodMode { get; private set; }
    

    void Update()
    {

        //IsRunning = Input.GetButton("Run") || Input.GetAxis("Run") > 0f;
        IsWalking = (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") < 0);
        IsJumping = Input.GetButtonDown("Jump");
        //IsCrouching = Input.GetButton("Crouch");
        IsPassing = Input.GetButtonDown("Pass");
        
        IsCancelling = Input.GetButtonDown("Cancel");

        IsInGodMode = Input.GetKeyDown(KeyCode.G);
        
    }

    public float GetHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    public float GetHorizontalMouse()
    {
        return Input.GetAxis("Mouse X");
    }

    public float GetVerticalMouse()
    {
        return Input.GetAxis("Mouse Y");
    }
}
