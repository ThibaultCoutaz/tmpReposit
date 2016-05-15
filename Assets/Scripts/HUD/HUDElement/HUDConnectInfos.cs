using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDConnectInfos : HUDElement {

    [SerializeField]
    private Text textStatus;


    [SerializeField]
    private Text textIsMaster;

    [SerializeField]
    private Text textPing;

    public void EditTextStatus(string text)
    {
        textStatus.text = text;
    }

    public void EditTextIsMaster(string text)
    {
        textIsMaster.text = text;
    }


    public void EditTextPing(string text)
    {
        textPing.text = text;
    }
}
