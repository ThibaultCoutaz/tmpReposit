using UnityEngine;
using System.Collections;

public class CharacterControllerMoba : MonoBehaviour {

    public Vector3 movement;

    // Use this for initialization
    void Awake () {
    }
    

    void FixedUpdate()
    {
        //Define the movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        movement = new Vector3(h, 0.0f, v);
        
    }


}
