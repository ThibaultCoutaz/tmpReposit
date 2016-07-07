using UnityEngine;
using System.Collections;

public class HUDHoverText : HUDElement {

    public bool activate = false;

	void Update () {
        if(activate)
            transform.position = Input.mousePosition;
    }
}
