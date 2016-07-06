using UnityEngine;
using System.Collections;

public class ItemPrefab : MonoBehaviour {

    public Item item = new Item();

    public Item CopyItem()
    {
        Item itemtmp = new Item();
        itemtmp.index = item.index;
        itemtmp.Name = item.Name;
        itemtmp.Description = item.Description;
        itemtmp.ImgItem = item.ImgItem;
        itemtmp.type = item.type;
        itemtmp.bonus = item.bonus;
        Debug.Log(itemtmp);
        return item;
    }

}
