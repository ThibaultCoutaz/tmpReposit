using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SpriteButtonScriptShop : MonoBehaviour,
                                     IPointerClickHandler
{
    public Text price;
    public Image picItem;
    private Button button;
    public Item item;

    void Start()
    {
        button = GetComponent<Button>();
        //button.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.enabled)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if(NetworkManager.Instance.FindLocalPlayer().GetComponent<PlayerScript>().currentAmountOfGold > item.priceBuying)
                    HUDManager.Instance.DisplaySellBuyButton(false, true);
                else
                    HUDManager.Instance.DisplaySellBuyButton(false, false);

                HUDManager.Instance.DisplayAmountSellBuy(-item.priceBuying);

                HUDManager.Instance.SetCurrentSelectItemShop(item);
            }
        }
    }
}