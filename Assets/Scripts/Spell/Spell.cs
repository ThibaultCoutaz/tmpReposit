using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Spell : MonoBehaviour {

    public int id = -1;

    public delegate void ProjectionSpell(Vector3 posPlayer, Vector3 direction);
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
            displayFilled = false;
            HUDManager.Instance.ActivateFilledSpell(displayFilled, id);
        }
    }

    public void ProjectionTarget(Vector3 pos, Vector3 direction)
    {
        Debug.Log("Projection TArget");
        RaycastHit hit;

        //A REVOIR
        if (Physics.Raycast(pos, direction, out hit))
            if (hit.collider.gameObject.GetComponent<PlayerScript>())
            {
                Debug.LogError("Found a Character - distance: " + hit.distance);
            }
            else
            {
                Debug.LogError("Personne ICI");
            }
            
    }

    public void ProjectionAOE(Vector3 posPlayer, Vector3 Direction)
    {
        Debug.Log("Projection AOE");
    }
}
