using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDInventory : HUDElement {

    public GameObject[] ListCaseItems = new GameObject[6];

    public void AddItem(Item item)
    {
        Debug.LogError("ACHAT de " + item.Name);
        for(int i =0; i < ListCaseItems.Length; i++)
        {
            if(ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().item.index == -1) // SHould do a bool IsEmpty Modify THAT 
            {
                Debug.LogError("Ajout a L'Index = " + i +" de litem "+item);
                ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().enabled = true;
                ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().item = item;
                ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().item.index = i;
                ListCaseItems[i].GetComponent<Button>().interactable = true;
                ListCaseItems[i].GetComponent<Image>().sprite = item.ImgItem;
                NetworkManager.Instance.FindLocalPlayer().GetComponent<ManageInventory>().AddItem(ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().item);
                break; // si ca marche pas changer valeur de i
            }
            else
            {
                Debug.LogError("Index pas null = " + i);
            }
        }
    }

    /// <summary>
    /// To remove a item from the inventory in the graphics way .. first Item is 0
    /// </summary>
    /// <param name="index"></param>
    public void RemoveItem(int index)
    {
        if (ListCaseItems[index].GetComponent<SpriteButtonScriptInventory>().item != null)
        {
            NetworkManager.Instance.FindLocalPlayer().GetComponent<ManageInventory>().RemoveItem(index);
            ListCaseItems[index].GetComponent<Button>().interactable = false;
            ListCaseItems[index].GetComponent<SpriteButtonScriptInventory>().enabled = false;
            ListCaseItems[index].GetComponent<SpriteButtonScriptInventory>().item = null;
            ListCaseItems[index].GetComponent<Image>().sprite = null;
        }
    }
    
}
