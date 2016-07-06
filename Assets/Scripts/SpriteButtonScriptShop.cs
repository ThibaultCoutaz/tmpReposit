using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SpriteButtonScriptShop : MonoBehaviour,
                                     IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
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
                HUDManager.Instance.DisplaySellBuyButton(false, true);
                HUDManager.Instance.SetCurrentSelectItemShop(item);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}