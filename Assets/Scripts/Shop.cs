using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //It was the salmonmoose
    [SerializeField] GameObject shopObject;
    [SerializeField] Transform[] shopObjectLocations;
    MarbleList marbleList;
    HashSet<GameObject> shopMarbles;

    void Start()
    {
        marbleList = FindObjectOfType<MarbleList>();
        DisplayShopItems();
    }

    //Creates a shop Object -> sets its name cost and sprite from the Marble List
    public void DisplayShopItems()
    {
        Generate3ShopItems();
        int counter = 0;
        foreach (var item in shopMarbles)
        {
            GameObject shop = Instantiate(shopObject);
            shop.transform.SetParent(transform);
            Marble marble = item.GetComponent<Marble>();
            string name = marble.commonName;
            string cost = marble.cost.ToString();
            Sprite sprite = marble.marbleSprite;

            ShopObject newShopObject = shop.GetComponent<ShopObject>();
            newShopObject.marbleName.text = name;
            newShopObject.marbleCost.text = "$" + cost;
            newShopObject.marbleSpriteRenderer.sprite = sprite;

            if (counter < shopObjectLocations.Length)
            {
                shop.transform.position = shopObjectLocations[counter].position;
            }
            counter++;
        }
    }

    private void Generate3ShopItems()
    {
        shopMarbles = marbleList.GetMarblesForShop(3);
    }

    public void ResetShop()
    {
        DestroyShopObjectChildren();
        DisplayShopItems();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetShop();
        }
    }
    public void DestroyShopObjectChildren()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public bool MarbleNamesInShop(string checkedMarble)
    {
        bool marbleInShop = false;
        foreach (var marble in shopMarbles)
        {
            Debug.Log(marble.name);
            Debug.Log(checkedMarble);
            if (marble.name.ToLower() == checkedMarble)
            {
                marbleInShop = true;
            }
        }
        return marbleInShop;
    }
}
