using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //It was the salmonmoose
    [SerializeField] GameObject ShopObject;
    [SerializeField] Transform[] ShopObjectLocations;
    MarbleList MarbleList;
    HashSet<GameObject> ShopMarbles;

    void Start()
    {
        MarbleList = FindObjectOfType<MarbleList>();
        DisplayShopItems();
    }

    //Creates a shop Object -> sets its name cost and sprite from the Marble List
    public void DisplayShopItems()
    {
        Generate3ShopItems();
        int counter = 0;
        foreach (var item in ShopMarbles)
        {
            GameObject shop = Instantiate(ShopObject);
            shop.transform.SetParent(transform);
            Marble marble = item.GetComponent<Marble>();
            string name = marble.CommonName;
            string cost = marble.Cost.ToString();
            Sprite sprite = marble.MarbleSprite;

            ShopObject newShopObject = shop.GetComponent<ShopObject>();
            newShopObject.Name.text = name;
            newShopObject.Cost.text = "$" + cost;
            newShopObject.SpriteRender.sprite = sprite;

            if (counter < ShopObjectLocations.Length)
            {
                shop.transform.position = ShopObjectLocations[counter].position;
            }
            counter++;
        }
    }

    private void Generate3ShopItems()
    {
        ShopMarbles = MarbleList.GetMarblesForShop(3);
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
        foreach (var marble in ShopMarbles)
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
