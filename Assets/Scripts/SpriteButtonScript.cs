using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SpriteButtonScript : MonoBehaviour,
                                     IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    private Button button;

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
                Debug.LogError("LOUL");
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