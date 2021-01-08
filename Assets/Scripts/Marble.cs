using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    //All the data about the ball I need to make dictionary entry
    public int marbleCode; //unique to each ball, not for player to see
    public string commonName; //name for player to see and call, not nessesarily unique
    public int cost;
    public int rarity; //use scale 1-4 inclusive, used to determine cost and chance to appear in the shop
    public Sprite marbleSprite;


    void Start()
    {
        marbleSprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {

    }
}
