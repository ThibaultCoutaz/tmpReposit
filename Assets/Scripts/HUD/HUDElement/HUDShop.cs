using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUDShop : HUDElement {

    public GameObject prefabItem_Shop;
    public GameObject ImageInfosItem;
    public Button buyButton;

    //All part
    public GameObject ListALL;
    public Transform parentALL;
    protected Dictionary<string, Item> _ALL_sprites;

    //Boots part
    public GameObject ListBottes;
    public Transform parentBottes;
    protected Dictionary<string, Item> _bottes_sprites;

    //DMg part
    public GameObject ListDmg;
    public Transform parentDmg;
    protected Dictionary<string, Item> _dmg_sprites;

    //Ring part
    public GameObject ListRing;
    public Transform parentRing;
    protected Dictionary<string, Item> _ring_sprites;

    //Coiffe part
    public GameObject ListCoiffe;
    public Transform parentCoiffe;
    protected Dictionary<string, Item> _coiffe_sprites;

    [HideInInspector]
    public Item CurrentItemSelect;

    void Start()
    {
        _ALL_sprites = new Dictionary<string, Item>();

        //***************Boots Part********************//
        GameObject[] gameObject = Resources.LoadAll<GameObject>("Prefabs/Items/Bottes");

        _bottes_sprites = new Dictionary<string, Item>();

        foreach (GameObject go in gameObject)
        {
            _bottes_sprites.Add(go.GetComponent<Item>().Name, go.GetComponent<Item>());
            CreateItemShop(go.GetComponent<Item>(),parentBottes);

            _ALL_sprites.Add(go.GetComponent<Item>().Name, go.GetComponent<Item>());
            CreateItemShop(go.GetComponent<Item>(), parentALL);
        }


        ////***************DMG Part********************//
        gameObject = Resources.LoadAll<GameObject>("Prefabs/Items/Dmg");

        _dmg_sprites = new Dictionary<string, Item>();

        foreach (GameObject go in gameObject)
        {
            _dmg_sprites.Add(go.GetComponent<Item>().Name, go.GetComponent<Item>());
            CreateItemShop(go.GetComponent<Item>(), parentDmg);

            _ALL_sprites.Add(go.GetComponent<Item>().Name, go.GetComponent<Item>());
            CreateItemShop(go.GetComponent<Item>(), parentALL);
        }


        //////***************Ring Part********************//
        gameObject = Resources.LoadAll<GameObject>("Prefabs/Items/Ring");

        _ring_sprites = new Dictionary<string, Item>();

        foreach (GameObject go in gameObject)
        {
            _ring_sprites.Add(go.GetComponent<Item>().Name, go.GetComponent<Item>());
            CreateItemShop(go.GetComponent<Item>(), parentRing);

            _ALL_sprites.Add(go.GetComponent<Item>().Name, go.GetComponent<Item>());
            CreateItemShop(go.GetComponent<Item>(), parentALL);
        }


        //////***************Coiffe Part********************//
        gameObject = Resources.LoadAll<GameObject>("Prefabs/Items/Coiffe");

        _coiffe_sprites = new Dictionary<string, Item>();

        foreach (GameObject go in gameObject)
        {
            _coiffe_sprites.Add(go.GetComponent<Item>().Name, go.GetComponent<Item>());
            CreateItemShop(go.GetComponent<Item>(), parentCoiffe);

            _ALL_sprites.Add(go.GetComponent<Item>().Name, go.GetComponent<Item>());
            CreateItemShop(go.GetComponent<Item>(), parentALL);
        }
    }

    private void CreateItemShop(Item item,Transform parent)
    {
        GameObject tmp = Instantiate(prefabItem_Shop);
        tmp.GetComponent<SpriteButtonScript>().item = item;
        tmp.GetComponent<SpriteButtonScript>().PictureOfGameObject = ImageInfosItem;
        tmp.GetComponent<SpriteButtonScript>().BuyItem = buyButton;
        tmp.GetComponent<Image>().sprite = item.ImgItem;
        tmp.transform.parent = parent;
    }


    //Function to get value in the different dictionnary
    public Item getSpriteItemShopByName(string name)
    {
        Item item_Sprite;
        if (_ALL_sprites.TryGetValue(name, out item_Sprite))
            return item_Sprite;
        else
            return null;
    }

    public void SetCurrentSelectItem(Item item)
    {
        CurrentItemSelect = item;
    }

    public void AddItems()
    {
        HUDManager.Instance.AddItem(CurrentItemSelect);
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
