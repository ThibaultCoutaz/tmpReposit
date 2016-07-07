using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Spell : MonoBehaviour {

    public int id = -1;

    public delegate void ProjectionSpell(PlayerScript ps,Vector3 posPlayer, Vector3 direction);
    public ProjectionSpell projectionSpell;

    public enum APU_Spell
    {
        Actif,
        Passif,
        Ultimate
    }

    public enum Type_Spell
    {
        HimSelf,
        Target,
        AOE
    }

    public APU_Spell type;
    public Type_Spell targeting;
    public List<Sprite> spriteSpell;

    public float couldown = 10;
    public float damage = 0;
    public float heal = 0;
    public bool stunt = false;
    public float range = 10;

    [HideInInspector]
    public bool CanGetReadyCast = true;
    [HideInInspector]
    public bool canCast = false;
    [HideInInspector]
    public bool reload = false;
    [HideInInspector]
    public List<PlayerScript> playersTarget;

    protected Vector3 CenterAOE;

    public abstract void OnCast(PlayerScript ps);

    private float currentCouldown = 0;
    private bool displayFilled = false;

    public void ManageFilledSpell()
    {
        if (!displayFilled)
        {
            displayFilled = true;
            HUDManager.Instance.ActivateFilledSpell(displayFilled, id);
        }

        HUDManager.Instance.EditFilledSpell(1 - currentCouldown / couldown, id);
        currentCouldown += Time.deltaTime;

        if (currentCouldown >= couldown)
        {
            currentCouldown = 0;
            canCast = true;
            if (targeting == Type_Spell.Target)
                canCast = false;
            reload = false;
            displayFilled = false;
            HUDManager.Instance.ActivateFilledSpell(displayFilled, id);
        }
    }

    [HideInInspector]
    public bool displayInfosTarget = false;

    private PlayerScript psTarget;

    public void ProjectionTarget(PlayerScript ps,Vector3 pos, Vector3 direction)
    {
        Debug.Log("Projection TArget");
        RaycastHit hit;

        //A REVOIR
        if (Physics.Raycast(pos, direction, out hit, range))
        {
            if (hit.collider.gameObject.GetComponent<PlayerScript>() && hit.collider.gameObject.GetComponent<PlayerScript>() != ps)
            {
                psTarget = hit.collider.gameObject.GetComponent<PlayerScript>();
                if (!displayInfosTarget)
                {
                    HUDManager.Instance.DisplayInfosTarget(true);
                    displayInfosTarget = true;
                    //psTarget.GetComponent<ShaderManager>().GetTarget(true);
                    canCast = true;
                }
                HUDManager.Instance.EditInfosTarget(psTarget.nameCharacter, psTarget.caracterisiticCurrent.PV.ToString(), hit.distance.ToString());
            }
            else
            {
                if (displayInfosTarget)
                {
                    displayInfosTarget = false;
                    HUDManager.Instance.DisplayInfosTarget(false);
                    //psTarget.GetComponent<ShaderManager>().GetTarget(false);
                    canCast = false;
                }
                //Debug.LogError("Personne ICI");
            }
        }
        else
        {
            if (displayInfosTarget)
            {
                displayInfosTarget = false;
                HUDManager.Instance.DisplayInfosTarget(false);
                //psTarget.GetComponent<ShaderManager>().GetTarget(false);
                canCast = false;
            }
        }
    }

    public void ProjectionAOE(PlayerScript ps,Vector3 pos, Vector3 direction)
    {
        Debug.Log("Projection AOE");
        RaycastHit hit;

        //A REVOIR
        if (Physics.Raycast(pos, direction, out hit))
        {
            //Remove the Y axis to have the point on the floor
            CenterAOE = hit.point - new Vector3(0,hit.point.y,0);
            Debug.LogError("CentreAOE = " + CenterAOE);
        }
        else if (Physics.Raycast(pos, direction, out hit))
        {
            Vector3 tmpPos = hit.point - new Vector3(0, hit.point.y, 0);
            Debug.LogError("CentreAOE = " + CenterAOE);
        }
    }

    public void DetectPlayerRange(Vector3 center)
    {
        playersTarget = new List<PlayerScript>();
        Collider[] hitColliders = Physics.OverlapSphere(center, range);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].GetComponent<PlayerScript>())
            {
                playersTarget.Add(hitColliders[i].GetComponent<PlayerScript>());
            }
            i++;
        }

        Debug.LogError("Il y a " + playersTarget.Count + " Players in the area");
    }

}
