using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    public enum type_Item
    {
        Coiffe,
        Dmg,
        Bottes,
        Ring
    }

    [System.Serializable]
    public struct BonusUItem
    {
        public float PVPlus;
        public float AttaquePlus;
        public float DefencePlus;
    }

    public int index = -1;
    public string Name;
    public string Description;
    public Sprite ImgItem;
    public type_Item type;
    public BonusUItem bonus;
    public float priceBuying;
    public float priceSelling;

    public Item CopyItem()
    {
        Item itemtmp = new Item();
        itemtmp.index = index;
        itemtmp.Name = Name;
        itemtmp.Description = Description;
        itemtmp.ImgItem = ImgItem;
        itemtmp.type = type;
        itemtmp.bonus = bonus;
        itemtmp.priceBuying = priceBuying;
        itemtmp.priceSelling = priceSelling;
        return itemtmp;
    }
}
