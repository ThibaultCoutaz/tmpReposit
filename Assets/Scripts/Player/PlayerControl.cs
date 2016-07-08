using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float speed = 10;
    public float gravity = 10.0f;
    public float jumpHeight = 2.0f;
    public float maxVelocityChange = 10.0f;
    
    public float distanceGround = 0.4f;
    public bool canJump = true;

    public bool grounded = true;
    private Rigidbody rigb;
    private Animator anim;
    private PlayerScript ps;
	
    void Awake()
    {
        rigb = GetComponent<Rigidbody>();
        rigb.useGravity = false; // to control by my self the gravity 
        anim = GetComponent<Animator>();
        ps = GetComponent<PlayerScript>();

    }



	// Update is called once per frame
	void Update () {
        if (GetComponent<PhotonView>().isMine)
        {
            Vector3 targetVelocity = new Vector3(0, 0, 0);
            CheckGroundStatus();
            if (grounded)
            {
                float x = InputManager.Instance.GetHorizontalAxis();
                float z = InputManager.Instance.GetVerticalAxis();

                //calculate the speed
                targetVelocity = new Vector3(x, 0, z);
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= speed;

                //Apply a force
                Vector3 velocity = rigb.velocity;
                Vector3 velocityChange = targetVelocity - velocity;
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                rigb.AddForce(velocityChange, ForceMode.VelocityChange);


                if (InputManager.Instance.IsJumping && canJump && grounded)
                {
                    rigb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                }
                
            }
            // We apply gravity manually for more tuning control
            rigb.AddForce(new Vector3(0, -gravity * rigb.mass, 0));

            UpdateAnimation(targetVelocity);
        }
    }

    void UpdateAnimation(Vector3 move)
    {
        anim.SetFloat("Magnitude", move.magnitude);// a changer
    }

    float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2*jumpHeight * gravity);
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * distanceGround));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, distanceGround))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
