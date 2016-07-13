using UnityEngine;
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
            key == Game.UI_Types.Spell ||
            key == Game.UI_Types.Targeting ||
            key == Game.UI_Types.InfosTarget ||
            key == Game.UI_Types.Debuging ||
            key == Game.UI_Types.Shop ||
            key == Game.UI_Types.Inventory ||
            key == Game.UI_Types.HoverText)
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

    public void EditTimerInGame(double time)
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

    public void InitSpell(List<Sprite> spell1, List<Sprite> spell2, List<Sprite> spell3,List<Sprite> passif)
    {
        HUDElement spell;
        if (elements.TryGetValue(Game.UI_Types.Spell, out spell))
        {
            ((HUDSpell)spell).InitSprites(spell1,spell2,spell3, passif);
        }
    }

    public void ActivateFilledSpell(bool display, int whichOne)
    {
        HUDElement spell;
        if (elements.TryGetValue(Game.UI_Types.Spell, out spell))
        {
            if (whichOne == 0)
                ((HUDSpell)spell).ActivateFilled1(display);
            else if (whichOne == 1)
                ((HUDSpell)spell).ActivateFilled2(display);
            else if (whichOne == 2)
                ((HUDSpell)spell).ActivateFilled3(display);
        }
    }

    public void EditFilledSpell(float amount, int whichOne)
    {
        HUDElement spell;
        if (elements.TryGetValue(Game.UI_Types.Spell, out spell))
        {
            if(whichOne == 0)
                ((HUDSpell)spell).EditFilled1(amount);
            else if(whichOne == 1)
                ((HUDSpell)spell).EditFilled2(amount);
            else if(whichOne == 2)
                ((HUDSpell)spell).EditFilled3(amount);
        }
    }

    public void DisplayTargeting(bool display)
    {
        HUDElement targeting;
        if (elements.TryGetValue(Game.UI_Types.Targeting, out targeting))
        {
            targeting.displayGroup(display, 1);
        }
    }

    public void DisplayInfosTarget(bool display)
    {
        HUDElement infosTarget;
        if (elements.TryGetValue(Game.UI_Types.InfosTarget, out infosTarget))
        {
            infosTarget.displayGroup(display,1);
        }
    }

    public void EditInfosTarget(string textName = "", string textLife = "", string textDistance = "")
    {
        HUDElement infosTarget;
        if (elements.TryGetValue(Game.UI_Types.InfosTarget, out infosTarget))
        {
            ((HUDInfosTarget)infosTarget).EditName(textName);
            ((HUDInfosTarget)infosTarget).EditLife(textLife);
            ((HUDInfosTarget)infosTarget).EditDistance(textDistance);
        }
    }

    public void InitDebug(PlayerScript _ps)
    {
        HUDElement debug;
        if (elements.TryGetValue(Game.UI_Types.Debuging, out debug))
        {
            ((HUDDebug)debug).EditDebug(_ps);
        }
    }

    public void DisplayDebuging(bool display)
    {
        HUDElement debug;
        if (elements.TryGetValue(Game.UI_Types.Debuging, out debug))
        {
            debug.displayGroup(display, 1);
        }
    }

    public void DisplayShop(bool display,float money = 0) //If you forget the money paf = 0;
    {
        HUDElement shop;
        if (elements.TryGetValue(Game.UI_Types.Shop, out shop))
        {
            if (display)
                EditMoneyShop(money);
            shop.displayGroup(display, 1);
        }
    }

    public void EditMoneyShop(float money)
    {
        HUDElement shop;
        if (elements.TryGetValue(Game.UI_Types.Shop, out shop))
        {
            ((HUDShop)shop).InitMoney(money);
        }
    }

    public void SetCurrentSelectItemShop(Item item)
    {
        HUDElement shop;
        if (elements.TryGetValue(Game.UI_Types.Shop, out shop))
        {
            ((HUDShop)shop).SetCurrentSelectItem(item);
        }
    }

    public void DisplaySellBuyButton(bool sell,bool buy)
    {
        HUDElement shop;
        if (elements.TryGetValue(Game.UI_Types.Shop, out shop))
        {
            ((HUDShop)shop).SetSellBuyButton(sell,buy);
        }
    }

    public void DisplayAmountSellBuy(float _amount)
    {
        HUDElement shop;
        if (elements.TryGetValue(Game.UI_Types.Shop, out shop))
        {
            ((HUDShop)shop).SetAmountBuySell(_amount);
        }
    }

    public void DisplayInventory(bool display)
    {
        HUDElement inventory;
        if (elements.TryGetValue(Game.UI_Types.Inventory, out inventory))
        {
            inventory.displayGroup(display,1);
        }
    }

    public void AddItem(Item item)
    {
        HUDElement inventory;
        if (elements.TryGetValue(Game.UI_Types.Inventory, out inventory))
        {
            ((HUDInventory)inventory).AddItem(item);
        }
    }

    public void RemoveItem(int index)
    {
        HUDElement inventory;
        if (elements.TryGetValue(Game.UI_Types.Inventory, out inventory))
        {
            ((HUDInventory)inventory).RemoveItem(index);
        }
    }

    public void displayHoverText(bool value, string msg = "")
    {
        HUDElement text;
        if (elements.TryGetValue(Game.UI_Types.HoverText, out text))
        {
            text.setText(msg);
            ((HUDHoverText)text).activate = value;
            text.displayGroup(value,1,false,false);
        }
    }

    public void InitialiseDashBar(float max)
    {
        HUDElement dash;
        if (elements.TryGetValue(Game.UI_Types.DashPower, out dash))
        {
            ((HUDDashPower)dash).InitialiseBar(55, max);
        }
    }

    public void HandleBarDashPower(float value)
    {
        HUDElement dash;
        if (elements.TryGetValue(Game.UI_Types.DashPower, out dash))
        {
            ((HUDDashPower)dash).SetText("Power : ", value);
            ((HUDDashPower)dash).HandleBar(value);
        }
    }

    public int AddStateDisplayCD(float cooldown,HUDListState.typeState dState = HUDListState.typeState.NULL, Sprite s = null)
    {
        HUDElement stateList;
        if (elements.TryGetValue(Game.UI_Types.ListState, out stateList))
        {
            return ((HUDListState)stateList).AddActiveStateCD(cooldown,dState, s);
        }
        return -1;
    }

    public void RemoveEmplacement(int _ID = 0)
    {
        HUDElement stateList;
        if (elements.TryGetValue(Game.UI_Types.ListState, out stateList))
        {
            ((HUDListState)stateList).RemoveEmplacement(_ID);
        }
    }
}

