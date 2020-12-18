using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;
using System;
using TMPro;

public class JumpManager : MonoBehaviour
{
    // I have syntactic sugar
    [SerializeField] GameObject marbleObject;
    [SerializeField] Transform longJumpLocation;
    MarbleList marbleList;
    MarbleObject playerMarble;
    SpriteRenderer playerSpriteRenderer;
    GameData gameData;
    List<string> longJumpedPlayers = new List<string>();
    List<string> highJumpedPlayers = new List<string>();
    public int orderInLayer;
    public int costToReroll = 50;

    private void Start()
    {
        gameData = FindObjectOfType<GameData>();
        marbleList = FindObjectOfType<MarbleList>();
        orderInLayer = 0;
        Dictionary<string, float> kvp = new Dictionary<string, float>();
    }
    public void ResetLongJumpedPlayers()
    {
        longJumpedPlayers = new List<string>();
    }
    
    public void ResetHighJumpedPlayers()
    {
        highJumpedPlayers = new List<string>();
    }

    public void CreateMarbleAndJump(Arrrgs e)
    {
        string userID = e.userID;
        string displayName = e.displayName;
        //if the player has not jumped yet
        if (!(longJumpedPlayers.Contains(userID)))
        {
            longJumpedPlayers.Add(userID);
            var mb = Instantiate(marbleObject, longJumpLocation.position, transform.rotation);
            mb.transform.SetParent(transform);
            mb.GetComponentInChildren<SpriteRenderer>().sortingOrder = orderInLayer;
            orderInLayer++;
            playerMarble = mb.GetComponentInChildren<MarbleObject>();
            playerMarble.playerName.text = displayName;
            int marbleIndex = gameData.GetPlayerEquipSkin(userID);
            GameObject marbleGameObject = marbleList.GetMarbleFromMarbleCode(marbleIndex);
            playerMarble.gameMarbleSprite.sprite = marbleGameObject.GetComponent<Marble>().marbleSprite;
            playerMarble.playerID = userID;
        }
        else //if player has already rolled
        {
            if (gameData.CheckPlayerMoney(userID) > costToReroll)
            {
                MarbleObject[] allMarbles = GetComponentsInChildren<MarbleObject>();
                foreach (var marble in allMarbles)
                {
                    if (marble.playerID == userID && !(marble.isrolling))
                    {
                        Destroy(marble.transform.parent.gameObject);
                        gameData.SubtractMoneyFromPlayerID(costToReroll, userID);
                        longJumpedPlayers.Remove(userID);
                        CreateMarbleAndJump(e);
                    }
                }
            }
            //if marble object isRolling do nothing
            //if marble object is not Rolling then reroll and charge player 50 monies
        }
    }

    public void CreateMarbleAndHighJump(Arrrgs e)
    {
        string userID = e.userID;
        string displayName = e.displayName;
        if (!(highJumpedPlayers.Contains(userID)))
        {
            highJumpedPlayers.Add(userID);
            var mb = Instantiate(marbleObject, longJumpLocation.position, transform.rotation);
            mb.transform.SetParent(transform);
            mb.GetComponentInChildren<SpriteRenderer>().sortingOrder = orderInLayer;
            orderInLayer++;
            playerMarble = mb.GetComponentInChildren<MarbleObject>();
            playerMarble.playerName.text = displayName;
            int marbleIndex = gameData.GetPlayerEquipSkin(userID);
            GameObject marbleGameObject = marbleList.GetMarbleFromMarbleCode(marbleIndex);
            playerMarble.gameMarbleSprite.sprite = marbleGameObject.GetComponent<Marble>().marbleSprite;
            playerMarble.playerID = userID;
        }
    }

    public IEnumerator DestroyMarbles()
    {
        MarbleObject[] allMarbles = GetComponentsInChildren<MarbleObject>();

            foreach (var marble in allMarbles)
            {
                while (marble.isrolling == true)
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        yield return new WaitForSeconds(5f);
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        longJumpedPlayers = new List<string>();
        orderInLayer = 0;
    }


        //playerMarble.marbleSprite = 
        /*playerMarble = mb.GetComponent<Marble>();
        playerSpriteRenderer = mb.GetComponent<SpriteRenderer>();
        playerMarble.playerName
        e.Command.ChatMessage.Username*/
    }

