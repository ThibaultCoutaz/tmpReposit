﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField]
    private GameObject LocalWorldCanvas;

    protected HUDManager() { }

    Dictionary<Game.UI_Types, HUDElement> elements;
    
    //protected Dictionary<string, Sprite> _items_sprites;

    void Start()
    {
        DontDestroyOnLoad(LocalWorldCanvas);

        //Sprite[] sprites = Resources.LoadAll<Sprite>("Inventory");

        //_items_sprites = new Dictionary<string, Sprite>();

        //foreach (Sprite s in sprites)
        //    _items_sprites.Add(s.name, s);
    }


    //public Sprite getSpriteByName(string name)
    //{
    //    Sprite item_sprite;
    //    if (_items_sprites.TryGetValue(name, out item_sprite))
    //        return item_sprite;
    //    else
    //        return null;
    //}

    public void registerElement(Game.UI_Types key, HUDElement element)
    {
        if (key == Game.UI_Types.NULL)
            return;

        if (elements == null)
            elements = new Dictionary<Game.UI_Types, HUDElement>();

        if (!elements.ContainsKey(key))
            elements.Add(key, element);
        else
            Debug.LogError("HUDManager already contains key " + key);

        disableElement(key, element);

    }

    void disableElement(Game.UI_Types key, HUDElement element)
    {
        if (key == Game.UI_Types.TimerInGame ||
            key == Game.UI_Types.CharacterInfos ||
            key == Game.UI_Types.Spell)
            element.displayGroup(false, .0f, false, false);
    }

    //to get the gameObject we want from the dictionnary
    public GameObject getElement(Game.UI_Types key)
    {
        HUDElement obj;
        if (elements.TryGetValue(key, out obj))
        {
            return obj.gameObject;
        }
        Debug.LogError("No Element with the Type :" + key);
        return null;
    }

    public void DisableAll()
    {
        foreach (KeyValuePair<Game.UI_Types, HUDElement> element in elements)
        {

            disableElement(element.Key, element.Value);
        }
    }

    public void EditTextStatus(string text)
    {
        HUDElement connectInfo;
        if (elements.TryGetValue(Game.UI_Types.ConnectionInfos, out connectInfo))
        {
            ((HUDConnectInfos)connectInfo).EditTextStatus(text);

        }
    }

    public void EditTextIsMaster(string text)
    {
        HUDElement connectInfo;
        if (elements.TryGetValue(Game.UI_Types.ConnectionInfos, out connectInfo))
        {
            ((HUDConnectInfos)connectInfo).EditTextIsMaster(text);

        }
    }

    public void EditTextPing(string text)
    {
        HUDElement connectInfo;
        if (elements.TryGetValue(Game.UI_Types.ConnectionInfos, out connectInfo))
        {
            ((HUDConnectInfos)connectInfo).EditTextPing(text);

        }
    }

    public void DisplayTimerInGame(bool display)
    {
        HUDElement timer;
        if (elements.TryGetValue(Game.UI_Types.TimerInGame, out timer))
        {
            timer.displayGroup(display);
        }
    }

    public void EditTimerInGame(float time)
    {
        HUDElement timer;
        if (elements.TryGetValue(Game.UI_Types.TimerInGame, out timer))
        {
            timer.setChrono(time);
        }
    }

    public void DisplayCharacterInfos(bool display)
    {
        HUDElement CInfos;
        if (elements.TryGetValue(Game.UI_Types.CharacterInfos, out CInfos))
        {
            CInfos.displayGroup(display);
        }
    }

    public void DisplayMoney(bool display)
    {
        HUDElement money;
        if (elements.TryGetValue(Game.UI_Types.AmountGold, out money))
        {
            money.displayGroup(display);
        }
    }

    public void EditGold(float amount)
    {
        HUDElement gold;
        if (elements.TryGetValue(Game.UI_Types.AmountGold, out gold))
        {
            gold.setText("Gold : "+ amount);
        }
    }

    public void DisplayStateCharacter(bool display)
    {
        HUDElement StateC;
        if (elements.TryGetValue(Game.UI_Types.StateCharacter, out StateC))
        {
            StateC.displayGroup(display);
        }
    }

    public void EditState(string text)
    {
        HUDElement StateC;
        if (elements.TryGetValue(Game.UI_Types.StateCharacter, out StateC))
        {
            StateC.setText("State : "+text);
        }
    }

    public void DisplaySpell(bool display)
    {
        HUDElement spell;
        if (elements.TryGetValue(Game.UI_Types.Spell, out spell))
        {
            spell.displayGroup(display);
        }
    }

    public void InitSpell(List<Sprite> spell1, List<Sprite> spell2, List<Sprite> spell3, List<Sprite> spell4)
    {
        HUDElement spell;
        if (elements.TryGetValue(Game.UI_Types.Spell, out spell))
        {
            ((HUDSpell)spell).InitSprites(spell1,spell2,spell3,spell4);
        }
    }
}

