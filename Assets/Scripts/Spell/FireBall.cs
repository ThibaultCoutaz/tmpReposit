using UnityEngine;
using System.Collections;
using System;

public class FireBall : Spell
{
    public override void OnCast(PlayerScript ps)
    {
        //GameObject tmp = PhotonNetwork.Instantiate("Prefabs/Spells/Projectil/FireBall", ps.transform.position, Quaternion.identity, 0);
        //tmp.GetComponent<Rigidbody>().AddForce(ps.transform.forward * 1000);
        Debug.LogError("FIRE");
        //throw new NotImplementedException();
        canCast = false;
        reload = true;
    }
}
