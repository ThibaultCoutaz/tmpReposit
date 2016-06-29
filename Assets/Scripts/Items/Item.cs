using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

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

    public string Name;
    public string Description;
    public Sprite ImgItem;
    public type_Item type;
    public BonusUItem bonus;

}
