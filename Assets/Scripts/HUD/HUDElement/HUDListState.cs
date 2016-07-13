using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HUDListState : HUDElement {

    public enum typeState
    {
        NULL,
        DashFoward,
        DashRight,
        DashLeft
    }

    public Transform parentList;
    public GameObject statePic;
    public int nbEmplacement=8;
    private int currentnbEmplacement = 0;
    private int IDIncrement = 0;

    Dictionary<string, Sprite> spriteState;

	// Use this for initialization
	void Start () {

        //***************Boots Part********************//
        Sprite[] sprites = Resources.LoadAll<Sprite>("Asset2D/UI/State");

        spriteState = new Dictionary<string, Sprite>();

        foreach (Sprite s in sprites)
        {
            spriteState.Add(s.name, s);
        }
    }
	
    public int AddActiveStateCD(float cooldown,typeState dState = typeState.NULL,Sprite s = null)
    {
        if (dState == typeState.NULL && s == null)
        {
            Debug.LogError("THis State can't be initialise because no type and no sprite pass in parameter");
            return -1;
        }
        else
        {
            if (currentnbEmplacement < nbEmplacement)
            {
                if (dState != typeState.NULL)
                {
                    return CreateSprite(getSpriteState(dState.ToString()), cooldown);
                }
                else if (s != null)
                {
                    return CreateSprite(s, cooldown);
                }
            }
        }
        return -1;
    }

    //Function to get value in the different dictionnary
    public Sprite getSpriteState(string name)
    {
        Sprite sprite_State;
        if (spriteState.TryGetValue(name, out sprite_State))
            return sprite_State;
        else
            return null;
    }

    /// <summary>
    /// Without a return
    /// </summary>
    /// <param name="s"></param>
    /// <param name="cooldown"></param>
    private int CreateSprite(Sprite s,float cooldown)
    {
        GameObject tmp = Instantiate(statePic);
        tmp.transform.SetParent(parentList);
        tmp.GetComponent<Image>().sprite = s;
        tmp.GetComponent<BehaviourStateIcone>().CoolDown = cooldown;
        tmp.GetComponent<BehaviourStateIcone>().ID = IDIncrement;
        currentnbEmplacement++;
        return IDIncrement++;
    }

    public void RemoveEmplacement(int _ID = -1)
    {
        if(_ID != -1)
        {
            foreach(Transform child in parentList)
            {
                if(child.gameObject.GetComponent<BehaviourStateIcone>().ID == _ID)
                {
                    child.gameObject.GetComponent<BehaviourStateIcone>().End = true;
                    if (currentnbEmplacement > 0)
                        currentnbEmplacement--;
                }
            }
        }
        else
        {
            if (currentnbEmplacement > 0)
                currentnbEmplacement--;
        }
    }
}
