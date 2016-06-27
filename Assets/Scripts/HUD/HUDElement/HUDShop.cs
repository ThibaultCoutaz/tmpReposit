using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUDShop : HUDElement {

    public GameObject prefabItem_Shop;


    //All part
    public GameObject ListALL;
    public Transform parentALL;
    protected Dictionary<string, Sprite> _ALL_sprites;

    //Boots part
    public GameObject ListBottes;
    public Transform parentBottes;
    protected Dictionary<string, Sprite> _bottes_sprites;

    //DMg part
    public GameObject ListDmg;
    public Transform parentDmg;
    protected Dictionary<string, Sprite> _dmg_sprites;

    //Ring part
    public GameObject ListRing;
    public Transform parentRing;
    protected Dictionary<string, Sprite> _ring_sprites;

    //Coiffe part
    public GameObject ListCoiffe;
    public Transform parentCoiffe;
    protected Dictionary<string, Sprite> _coiffe_sprites;

    void Start()
    {
        _ALL_sprites = new Dictionary<string, Sprite>();

        //***************Boots Part********************//
        Sprite[] sprites = Resources.LoadAll<Sprite>("Asset2D/Shop/Bottes");

        _bottes_sprites = new Dictionary<string, Sprite>();

        foreach (Sprite s in sprites)
        {
            _bottes_sprites.Add(s.name, s);
            GameObject tmp = Instantiate(prefabItem_Shop);
            tmp.GetComponent<Image>().sprite = s;
            tmp.transform.parent = parentBottes;

            _ALL_sprites.Add(s.name, s);
            tmp = Instantiate(prefabItem_Shop);
            tmp.GetComponent<Image>().sprite = s;
            tmp.transform.parent = parentALL;
        }


        //***************DMG Part********************//
        sprites = Resources.LoadAll<Sprite>("Asset2D/Shop/Dmg");

        _dmg_sprites = new Dictionary<string, Sprite>();
        
        foreach (Sprite s in sprites)
        {
            _dmg_sprites.Add(s.name, s);
            GameObject tmp = Instantiate(prefabItem_Shop);
            tmp.GetComponent<Image>().sprite = s;
            tmp.transform.parent = parentDmg;

            _ALL_sprites.Add(s.name, s);
            tmp = Instantiate(prefabItem_Shop);
            tmp.GetComponent<Image>().sprite = s;
            tmp.transform.parent = parentALL;
        }


        ////***************Ring Part********************//
        sprites = Resources.LoadAll<Sprite>("Asset2D/Shop/Ring");

        _ring_sprites = new Dictionary<string, Sprite>();

        foreach (Sprite s in sprites)
        {
            _ring_sprites.Add(s.name, s);
            GameObject tmp = Instantiate(prefabItem_Shop);
            tmp.GetComponent<Image>().sprite = s;
            tmp.transform.parent = parentRing;

            _ALL_sprites.Add(s.name, s);
            tmp = Instantiate(prefabItem_Shop);
            tmp.GetComponent<Image>().sprite = s;
            tmp.transform.parent = parentALL;
        }


        ////***************Coiffe Part********************//
        sprites = Resources.LoadAll<Sprite>("Asset2D/Shop/Coiffe");

        _coiffe_sprites = new Dictionary<string, Sprite>();

        foreach (Sprite s in sprites)
        {
            _coiffe_sprites.Add(s.name, s);
            GameObject tmp = Instantiate(prefabItem_Shop);
            tmp.GetComponent<Image>().sprite = s;
            tmp.transform.parent = parentCoiffe;

            _ALL_sprites.Add(s.name, s);
            tmp = Instantiate(prefabItem_Shop);
            tmp.GetComponent<Image>().sprite = s;
            tmp.transform.parent = parentALL;
        }
    }


    //Function to get value in the different dictionnary
    public Sprite getSpriteItemShopByName(string name)
    {
        Sprite item_Sprite;
        if (_ALL_sprites.TryGetValue(name, out item_Sprite))
            return item_Sprite;
        else
            return null;
    }


    // Function to Open the different Part of the Shop
    private void CloseAll()
    {
        ListALL.SetActive(false);
        ListBottes.SetActive(false);
        ListDmg.SetActive(false);
        ListRing.SetActive(false);
        ListCoiffe.SetActive(false);
    }

    public void OpenAllShop()
    {
        CloseAll();
        ListALL.SetActive(true);
    }

    public void OpenBottesShop()
    {
        CloseAll();
        ListBottes.SetActive(true);
    }

    public void OpenDmgShop()
    {
        CloseAll();
        ListDmg.SetActive(true);
    }

    public void OpenRingShop()
    {
        CloseAll();
        ListRing.SetActive(true);
    }

    public void OpenCoiffeShop()
    {
        CloseAll();
        ListCoiffe.SetActive(true);
    }
}
