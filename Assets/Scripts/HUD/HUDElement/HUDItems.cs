using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDItems : HUDElement {

    public GameObject[] ListCaseItems = new GameObject[6];

    public void AddItems(Item item)
    {
        for(int i =0; i < ListCaseItems.Length; i++)
        {
            if(ListCaseItems[i].GetComponent<SpriteButtonScript>().item == null)
            {
                ListCaseItems[i].GetComponent<SpriteButtonScript>().item = item;
                ListCaseItems[i].GetComponent<Image>().sprite = item.ImgItem;
                break; // si ca marche pas changer valeur de i
            }
        }
    }
    
}
