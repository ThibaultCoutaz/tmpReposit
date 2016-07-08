using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUDShop : HUDElement {

    public GameObject prefabItem_Shop;
    public Image ImageInfosItem;
    public Text descriptionInfosItem;
    public Button buyButton;
    public Button sellButton;
    public Text moneyText;
    public Text sellBuyAmount;
    public Image backgroundSellBuyAmount;

    //All part
    public GameObject ListALL;
    public Transform parentALL;
    protected Dictionary<string, ItemPrefab> _ALL_sprites;

    //Boots part
    public GameObject ListBottes;
    public Transform parentBottes;
    protected Dictionary<string, ItemPrefab> _bottes_sprites;

    //DMg part
    public GameObject ListDmg;
    public Transform parentDmg;
    protected Dictionary<string, ItemPrefab> _dmg_sprites;

    //Ring part
    public GameObject ListRing;
    public Transform parentRing;
    protected Dictionary<string, ItemPrefab> _ring_sprites;

    //Coiffe part
    public GameObject ListCoiffe;
    public Transform parentCoiffe;
    protected Dictionary<string, ItemPrefab> _coiffe_sprites;

    [HideInInspector]
    public Item CurrentItemSelect;

    void Start()
    {
        _ALL_sprites = new Dictionary<string, ItemPrefab>();

        //***************Boots Part********************//
        GameObject[] gameObject = Resources.LoadAll<GameObject>("Prefabs/Items/Bottes");

        _bottes_sprites = new Dictionary<string, ItemPrefab>();

        foreach (GameObject go in gameObject)
        {
            _bottes_sprites.Add(go.GetComponent<ItemPrefab>().item.Name, go.GetComponent < ItemPrefab>());
            CreateItemShop(go.GetComponent<ItemPrefab>().item,parentBottes);

            _ALL_sprites.Add(go.GetComponent<ItemPrefab>().item.Name, go.GetComponent<ItemPrefab>());
            CreateItemShop(go.GetComponent<ItemPrefab>().item, parentALL);
        }


        ////***************DMG Part********************//
        gameObject = Resources.LoadAll<GameObject>("Prefabs/Items/Dmg");

        _dmg_sprites = new Dictionary<string, ItemPrefab>();

        foreach (GameObject go in gameObject)
        {
            _dmg_sprites.Add(go.GetComponent<ItemPrefab>().item.Name, go.GetComponent<ItemPrefab>());
            CreateItemShop(go.GetComponent<ItemPrefab>().item, parentDmg);

            _ALL_sprites.Add(go.GetComponent<ItemPrefab>().item.Name, go.GetComponent<ItemPrefab>());
            CreateItemShop(go.GetComponent<ItemPrefab>().item, parentALL);
        }


        //////***************Ring Part********************//
        gameObject = Resources.LoadAll<GameObject>("Prefabs/Items/Ring");

        _ring_sprites = new Dictionary<string, ItemPrefab>();

        foreach (GameObject go in gameObject)
        {
            _ring_sprites.Add(go.GetComponent<ItemPrefab>().item.Name, go.GetComponent<ItemPrefab>());
            CreateItemShop(go.GetComponent<ItemPrefab>().item, parentRing);

            _ALL_sprites.Add(go.GetComponent<ItemPrefab>().item.Name, go.GetComponent<ItemPrefab>());
            CreateItemShop(go.GetComponent<ItemPrefab>().item, parentALL);
        }


        //////***************Coiffe Part********************//
        gameObject = Resources.LoadAll<GameObject>("Prefabs/Items/Coiffe");

        _coiffe_sprites = new Dictionary<string, ItemPrefab>();

        foreach (GameObject go in gameObject)
        {
            _coiffe_sprites.Add(go.GetComponent<ItemPrefab>().item.Name, go.GetComponent<ItemPrefab>());
            CreateItemShop(go.GetComponent<ItemPrefab>().item, parentCoiffe);

            _ALL_sprites.Add(go.GetComponent<ItemPrefab>().item.Name, go.GetComponent<ItemPrefab>());
            CreateItemShop(go.GetComponent<ItemPrefab>().item, parentALL);
        }
    }

    private void CreateItemShop(Item item,Transform parent)
    {
        GameObject tmp = Instantiate(prefabItem_Shop);
        tmp.GetComponent<SpriteButtonScriptShop>().item = item;
        tmp.GetComponent<SpriteButtonScriptShop>().picItem.sprite = item.ImgItem;
        tmp.GetComponent<SpriteButtonScriptShop>().price.text = item.priceBuying.ToString();

        tmp.GetComponent<Image>().sprite = item.ImgItem;
        tmp.transform.parent = parent;
    }

    //Function to get value in the different dictionnary
    public Item getSpriteItemShopByName(string name)
    {
        ItemPrefab item_Sprite;
        if (_ALL_sprites.TryGetValue(name, out item_Sprite))
            return item_Sprite.item;
        else
            return null;
    }

    public void SetCurrentSelectItem(Item item)
    {
        CurrentItemSelect = item;
        ImageInfosItem.sprite = item.ImgItem;
        descriptionInfosItem.text = item.Description;
    }

    public void BuyItem()
    {
        Item item = CurrentItemSelect.CopyItem();
        HUDManager.Instance.AddItem(item);
    }

    public void SellItem()
    {
        sellButton.interactable = false;
        HUDManager.Instance.RemoveItem(CurrentItemSelect.index);
        HUDManager.Instance.DisplayAmountSellBuy(0);
    }

    public void SetSellBuyButton(bool interactSell , bool interactBuy)
    {
        buyButton.interactable = interactBuy;
        sellButton.interactable = interactSell;
    }

    public void SetAmountBuySell(float _amount)
    {
        if (_amount > 0)
        {
            sellBuyAmount.text = "+ " + _amount.ToString();
            backgroundSellBuyAmount.color = Color.green;
        }
        else if(_amount < 0)
        {
            sellBuyAmount.text =  _amount.ToString();
            backgroundSellBuyAmount.color = Color.red;
        }
        else //Case = 0
        {
            sellBuyAmount.text = "+ " + _amount.ToString();
            backgroundSellBuyAmount.color = Color.white;
        }
    }

    public void InitMoney(float amount)
    {
        if (amount > 0)
        {
            moneyText.text = amount + "Golds";
        }
        else
        {
            moneyText.text = amount + "Gold";
        }
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
