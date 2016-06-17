using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDInfosTarget : HUDElement {

    [SerializeField]
    private Text name_character;

    [SerializeField]
    private Text life;

    [SerializeField]
    private Text distance;

    public void EditName(string text)
    {
        name_character.text = "Name : "+text;
    }

    public void EditLife(string text)
    {
        life.text = "Life : "+text;
    }

    public void EditDistance(string text)
    {
        distance.text = "Distance : "+text;
    }
}
