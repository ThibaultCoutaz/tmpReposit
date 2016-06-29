using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SpriteButtonScriptInventory : MonoBehaviour,
                                     IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    private Button button;
    [HideInInspector]
    public Item item = null;
    [HideInInspector]
    public GameObject PictureOfGameObject;
    [HideInInspector]
    public Button BuyItem;

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
                Debug.LogError("Item Name and bonus = " + item.name);
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
