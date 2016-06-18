using UnityEngine;
using System.Collections;

public class Shield : Spell
{
    public override void OnCast(PlayerScript ps)
    {
        Debug.LogError("Shield");
        //throw new NotImplementedException();
        DetectPlayerRange(ps.transform.position);
        reload = true;
    }
}
