using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDDashPower : HUDElement {

    public Text text;
    public RectTransform barTransform;
    //public Image insideBar;
    private float cachedY, minXValue, maxXValue, min, max;

    //Change the text
    public void SetText(string s, float value)
    {
        text.GetComponent<Text>().text = s + value;
    }

    //To Initialise the Bar with the position 
    public void InitialiseBar(float _min,float _max)
    {
        min = _min;
        max = _max;
        cachedY = barTransform.localPosition.y;
        maxXValue = barTransform.localPosition.x;
        minXValue = barTransform.localPosition.x - barTransform.rect.width;
    }

    //**********To calculate the value of the Bar*******//
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

    }

    //***To update the Bar********//
    public void HandleBar(float currentValue)
    {
        float currentXValue = MapValues(currentValue, 0f, max, minXValue, maxXValue);
        if (currentXValue < minXValue)
            currentXValue = minXValue;
        if (currentXValue > maxXValue)
            currentXValue = maxXValue;
        barTransform.localPosition = new Vector3(currentXValue, cachedY);
    }

    //internal void SetColor(Color color)
    //{
    //    insideBar.color = color;
    //}
}
