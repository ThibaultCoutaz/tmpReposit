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

    GameObject[] listState = new GameObject[8]; // A REVOIR CA !

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
	
    public void AddActiveState(float cooldown,typeState dState = typeState.NULL,Sprite s = null)
    {
        if (dState == typeState.NULL && s == null)
        {
            Debug.LogError("THis State can't be initialise because no type and no sprite pass in parameter");
        }
        else
        {
            for(int i = 0; i < listState.Length; i++)
            {
                if(listState[i] == null)
                {
                    if(dState!= typeState.NULL)
                    {
                        CreateSprite(listState[i], getSpriteState(dState.ToString()),cooldown);
                    }
                    else if (s != null)
                    {
                        CreateSprite(listState[i], s,cooldown);
                    }
                    break;
                }
            }
        }
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

    private void CreateSprite(GameObject emplacement,Sprite s,float cooldown)
    {
        emplacement = Instantiate(statePic);
        emplacement.transform.SetParent(parentList);
        emplacement.GetComponent<Image>().sprite = s;
        emplacement.GetComponent<BehaviourStateIcone>().CoolDown = cooldown;
    }

    public void RemoveState()
    {
    }
}
