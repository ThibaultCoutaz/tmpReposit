using UnityEngine;
using System.Collections;

public class HUDDebug : HUDElement {

    private PlayerScript ps;

    public void EditDebug(PlayerScript _ps)
    {
        ps = _ps;
    }

    public void KillPlayer()
    {
        Debug.LogError("Debuging -- Killing Player");
        ps.EditLife(-900000);
    }
}
