using UnityEngine;
using System.Collections;

public class Shield : Spell
{
    public override void OnCast(PlayerScript ps)
    {
        Debug.LogError("Shield");
        //throw new NotImplementedException();
        canCast = false;
        reload = true;
    }
}
