using UnityEngine;
using System.Collections;

public class ManageSpell : MonoBehaviour {

    private enum KeyPress
    {
        A,
        E,
        R,
        NULL
    }

    public Spell[] spells = new Spell[3];
    public Transform pivot;

    private PlayerScript ps;
    private Camera cameraPlayer;
    private KeyPress keyPress = KeyPress.NULL;
    private bool displayViseur = false;

    void Start () {
        ps = GetComponent<PlayerScript>();
        cameraPlayer = ps.cameraPlayer;

        spells[0].id = 0; if (spells[0].targeting != Spell.Type_Spell.Target) spells[0].canCast = true; else spells[0].canCast = false;
        spells[1].id = 1; if (spells[1].targeting != Spell.Type_Spell.Target) spells[1].canCast = true; else spells[1].canCast = false;
        spells[2].id = 2; if (spells[2].targeting != Spell.Type_Spell.Target) spells[2].canCast = true; else spells[2].canCast = false;
        HUDManager.Instance.InitSpell(spells[0].spriteSpell, spells[1].spriteSpell, spells[2].spriteSpell, null);
        HUDManager.Instance.DisplaySpell(true);
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 3; i++)
        {
            if (spells[i].reload)
                spells[i].ManageFilledSpell();
        }

        //Imput manage
        if (!GetComponent<PlayerScript>().hasBall)
        {
            if (InputManager.Instance.IsSpellA && !spells[0].reload && (keyPress == KeyPress.NULL || keyPress == KeyPress.A))
            {
                keyPress = KeyPress.A;
                if (spells[0].targeting != Spell.Type_Spell.HimSelf)
                {
                    if (spells[0].projectionSpell == null)
                    {
                        InitFunctionProjection(spells[0]);
                    }

                    if (!displayViseur && spells[0].targeting == Spell.Type_Spell.Target)
                    {
                        HUDManager.Instance.DisplayTargeting(true);
                        displayViseur = true;
                    }

                    spells[0].projectionSpell(ps, pivot.position, cameraPlayer.transform.forward);

                    if (InputManager.Instance.IsUsingSpell)
                        if (spells[0].targeting != Spell.Type_Spell.Target)
                        {
                            spells[0].OnCast(ps);
                        }
                        else
                        {
                            if (spells[0].canCast)
                                spells[0].OnCast(ps);
                        }

                }
                else
                {
                    spells[0].OnCast(ps);
                }
            }
            else if (InputManager.Instance.IsSpellE && !spells[1].reload && (keyPress == KeyPress.NULL || keyPress == KeyPress.E))
            {
                keyPress = KeyPress.E;
                if (spells[1].targeting != Spell.Type_Spell.HimSelf)
                {
                    if (spells[1].projectionSpell == null)
                    {
                        InitFunctionProjection(spells[1]);
                    }

                    spells[1].projectionSpell(ps, pivot.position, cameraPlayer.transform.forward);

                    if (InputManager.Instance.IsUsingSpell)
                        if (spells[1].targeting != Spell.Type_Spell.Target)
                        {
                            spells[1].OnCast(ps);
                        }
                        else
                        {
                            if (spells[1].canCast)
                                spells[1].OnCast(ps);
                        }
                }
                else
                {
                    spells[1].OnCast(ps);
                }
            }
            else if (InputManager.Instance.IsSpellR && !spells[2].reload && (keyPress == KeyPress.NULL || keyPress == KeyPress.E))
            {
                keyPress = KeyPress.R;
                if (spells[2].targeting != Spell.Type_Spell.HimSelf)
                {
                    if (spells[2].projectionSpell == null)
                    {
                        InitFunctionProjection(spells[2]);
                    }

                    spells[2].projectionSpell(ps, pivot.position, cameraPlayer.transform.forward);

                    if (InputManager.Instance.IsUsingSpell)
                        if (spells[2].targeting != Spell.Type_Spell.Target)
                        {
                            spells[2].OnCast(ps);
                        }
                        else
                        {
                            if (spells[2].canCast)
                                spells[2].OnCast(ps);
                        }
                }
                else
                {
                    spells[2].OnCast(ps);
                }
            }
            else
            {
                if (keyPress != KeyPress.NULL)
                {
                    switch (keyPress)
                    {
                        case KeyPress.A:
                            if (spells[0].displayInfosTarget)
                            {
                                HUDManager.Instance.DisplayInfosTarget(false);
                                spells[0].displayInfosTarget = false;
                            }
                            break;
                        case KeyPress.E:
                            if (spells[1].displayInfosTarget)
                            {
                                HUDManager.Instance.DisplayInfosTarget(false);
                                spells[1].displayInfosTarget = false;
                            }
                            break;
                        case KeyPress.R:
                            if (spells[2].displayInfosTarget)
                            {
                                HUDManager.Instance.DisplayInfosTarget(false);
                                spells[2].displayInfosTarget = false;
                            }
                            break;
                    }

                    keyPress = KeyPress.NULL;
                    //Set all the Display to false
                    //HUDManager.Instance.DisplayInfosTarget(false);
                    if (displayViseur)
                    {
                        HUDManager.Instance.DisplayTargeting(false);
                        displayViseur = false;
                    }
                }
            }
        }
    }

    private void InitFunctionProjection(Spell spell)
    {
        switch (spell.targeting)
        {
            case Spell.Type_Spell.Target:
                spell.projectionSpell = spell.ProjectionTarget;
                break;
            case Spell.Type_Spell.AOE:
                spell.projectionSpell = spell.ProjectionAOE;
                break;

        }
    }
    
}
