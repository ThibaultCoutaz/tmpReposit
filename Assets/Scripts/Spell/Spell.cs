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
    public bool canCast = true;
    [HideInInspector]
    public bool reload = false;

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
            reload = false;
            displayFilled = false;
            HUDManager.Instance.ActivateFilledSpell(displayFilled, id);
        }
    }

    private bool displayInfos = false;

    public void ProjectionTarget(PlayerScript ps,Vector3 pos, Vector3 direction)
    {
        //HUDManager.Instance.DisplayTargeting(true);

        Debug.Log("Projection TArget");
        RaycastHit hit;

        //A REVOIR
        if (Physics.Raycast(pos, direction, out hit, range))
        {
            if (hit.collider.gameObject.GetComponent<PlayerScript>() && hit.collider.gameObject.GetComponent<PlayerScript>() != ps)
            {
                PlayerScript psTarget = hit.collider.gameObject.GetComponent<PlayerScript>();
                Debug.LogError("Found a Character - distance: " + hit.distance);
                if (!displayInfos)
                {
                    HUDManager.Instance.DisplayInfosTarget(true);
                    displayInfos = true;
                    canCast = true;
                }
                HUDManager.Instance.EditInfosTarget(psTarget.nameCharacter, psTarget.currentLife.ToString(), hit.distance.ToString());
            }
            else
            {
                if (displayInfos)
                {
                    displayInfos = false;
                    HUDManager.Instance.DisplayInfosTarget(false);
                    canCast = false;
                }
                //Debug.LogError("Personne ICI");
            }
        }
        else
        {
            if (displayInfos)
            {
                displayInfos = false;
                HUDManager.Instance.DisplayInfosTarget(false);
                canCast = false;
            }
        }
    }

    public void ProjectionAOE(PlayerScript ps,Vector3 posPlayer, Vector3 Direction)
    {
        Debug.Log("Projection AOE");
    }
}
