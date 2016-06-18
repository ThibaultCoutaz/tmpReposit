using UnityEngine;
using System.Collections;

public class Explosion : Spell
{ 
    public override void OnCast(PlayerScript ps)
    {
        Debug.LogError("ALLAH WAKBA");
        //throw new NotImplementedException();
        reload = true;
    }
}
