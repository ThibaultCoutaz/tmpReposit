using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Spell : MonoBehaviour {

    public int id = -1;

    public enum APU_Spell
    {
        Actif,
        Passif,
        Ultimate
    }

    public APU_Spell type;
    public List<Sprite> spriteSpell;

    public float couldown = 0;
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
            Debug.Log("WTF");
            currentCouldown = 0;
            canCast = true;
            displayFilled = false;
            HUDManager.Instance.ActivateFilledSpell(displayFilled, id);
        }
    }

}
