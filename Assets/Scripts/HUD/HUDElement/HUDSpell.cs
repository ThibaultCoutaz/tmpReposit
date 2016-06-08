﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class HUDSpell : HUDElement {

    public Image imageSpell1;
    public Image imageSpell2;
    public Image imageSpell3;
    public Image imagePassif;

    public Sprite NoIcone;

    private List<Sprite> niveauSpell1;
    private List<Sprite> niveauSpell2;
    private List<Sprite> niveauSpell3;
    private List<Sprite> niveauPassif;

    public void InitSprites(List<Sprite> _spell1, List<Sprite> _spell2, List<Sprite> _spell3, List<Sprite> _passif)
    {
        niveauSpell1 = _spell1;
        niveauSpell2 = _spell2;
        niveauSpell3 = _spell3;
        niveauPassif = _passif;
        

        if (_spell1.Capacity>0)
            imageSpell1.sprite = niveauSpell1[0];
        else
            imageSpell1.sprite = NoIcone;

        if (_spell2.Capacity > 0)
            imageSpell2.sprite = niveauSpell2[0];
        else
            imageSpell2.sprite = NoIcone;

        if (_spell3.Capacity > 0)
            imageSpell3.sprite = niveauSpell3[0];
        else
            imageSpell2.sprite = NoIcone;

        if (_passif.Capacity > 0)
            imagePassif.sprite = _passif[0];
        else
            imagePassif.sprite = NoIcone;
    }

    public void EditSpell1(Sprite s)
    {
        imageSpell1.sprite = s;
    }

    public void EditSpell2(Sprite s)
    {
        imageSpell2.sprite = s;
    }

    public void EditSpell3(Sprite s)
    {
        imageSpell3.sprite = s;
    }

    public void EditPassif(Sprite s)
    {
        imagePassif.sprite = s;
    }
}
