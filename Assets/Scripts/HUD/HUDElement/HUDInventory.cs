using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDInventory : HUDElement {

    public GameObject[] ListCaseItems = new GameObject[6];
    public Sprite emptyCase;

    public void AddItem(Item item)
    {
        for(int i =0; i < ListCaseItems.Length; i++)
        {
            if(ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().IsEmpty) // SHould do a bool IsEmpty Modify THAT 
            {
                ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().IsEmpty = false;
                ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().enabled = true;
                ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().item = item;
                ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().item.index = i;
                ListCaseItems[i].GetComponent<Button>().interactable = true;
                ListCaseItems[i].GetComponent<Image>().sprite = item.ImgItem;


                GameObject playerLocal = NetworkManager.Instance.FindLocalPlayer();
                playerLocal.GetComponent<PlayerScript>().currentAmountOfGold -= item.priceBuying;
                playerLocal.GetComponent<ManageInventory>().AddItem(ListCaseItems[i].GetComponent<SpriteButtonScriptInventory>().item);
                HUDManager.Instance.EditMoneyShop(playerLocal.GetComponent<PlayerScript>().currentAmountOfGold);

                if (NetworkManager.Instance.FindLocalPlayer().GetComponent<PlayerScript>().currentAmountOfGold < item.priceBuying)
                    HUDManager.Instance.DisplaySellBuyButton(false, false);

                break; // si ca marche pas changer valeur de i

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
            GameObject playerLocal = NetworkManager.Instance.FindLocalPlayer();
            playerLocal.GetComponent<PlayerScript>().currentAmountOfGold += ListCaseItems[index].GetComponent<SpriteButtonScriptInventory>().item.priceSelling;
            HUDManager.Instance.EditMoneyShop(playerLocal.GetComponent<PlayerScript>().currentAmountOfGold);
            playerLocal.GetComponent<ManageInventory>().RemoveItem(index);

            ListCaseItems[index].GetComponent<Button>().interactable = false;
            ListCaseItems[index].GetComponent<SpriteButtonScriptInventory>().IsEmpty = true;
            ListCaseItems[index].GetComponent<SpriteButtonScriptInventory>().enabled = false;
            ListCaseItems[index].GetComponent<SpriteButtonScriptInventory>().item = null;
            ListCaseItems[index].GetComponent<Image>().sprite = emptyCase;
        }
    }
    
}
