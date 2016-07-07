using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SpriteButtonScriptInventory : MonoBehaviour,
                                     IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    public bool IsEmpty = true;
    private Button button;
    public Item item;

    void Start()
    {
        button = GetComponent<Button>();
        //button.enabled = true;
    }

    void Update()
    {
        //if(item != null)
        //    Debug.LogError("squalala cest la fete a lindex = " + item.index);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.interactable)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                HUDManager.Instance.DisplaySellBuyButton(true, false);
                HUDManager.Instance.SetCurrentSelectItemShop(item);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HUDManager.Instance.displayHoverText(true, item.Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HUDManager.Instance.displayHoverText(false);
    }
}
