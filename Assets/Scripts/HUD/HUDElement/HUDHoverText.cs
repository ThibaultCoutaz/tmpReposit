using UnityEngine;
using System.Collections;

public class HUDHoverText : HUDElement {

    public bool activate = false;

	void Update () {
        if(activate)
            transform.position = Input.mousePosition+ new Vector3(0,0,-10);
    }
}
