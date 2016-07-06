using UnityEngine;
using System.Collections;

public class ManageInventory : MonoBehaviour {

    public Item[] items = new Item[6];

	// Use this for initialization
	void Start () {
        HUDManager.Instance.DisplayInventory(true);
	}
	
    public void AddItem(Item item)
    {
        if (items[item.index] == null)
        {
            items[item.index] = item;
        }
    }

    /// <summary>
    /// TO remove a item from the inventory , index => start at 0
    /// </summary>
    /// <param name="index"></param>
    public void RemoveItem(int index)
    {
        if(items[index] != null)
            items[index] = null;
    }
}
