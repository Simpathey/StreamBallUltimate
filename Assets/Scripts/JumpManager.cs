using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using UnityEngine;

public class JumpManager : MonoBehaviour
{
    // I have syntactic sugar
    [SerializeField] GameObject MarbleObject;
    [SerializeField] Transform LongJumpLocation;
    MarbleList MarbleList;
    MarbleObject PlayerMarble;
    SpriteRenderer PlayerSpriteRenderer;
    GameData GameData;
    List<string> LongJumpedPlayers = new List<string>();
    List<string> HighJumpedPlayers = new List<string>();
    public int OrderInLayer;
    public int CostToReroll = 50;

    private void Start()
    {
        GameData = FindObjectOfType<GameData>();
        MarbleList = FindObjectOfType<MarbleList>();
        OrderInLayer = 0;
        Dictionary<string, float> kvp = new Dictionary<string, float>();
    }
    public void ResetLongJumpedPlayers()
    {
        LongJumpedPlayers = new List<string>();
    }

    public void ResetHighJumpedPlayers()
    {
        HighJumpedPlayers = new List<string>();
    }

    public void CreateMarbleAndJump(CommandEventArgs e)
    {
        string userID = e.UserID;
        string displayName = e.DisplayName;
        //if the player has not jumped yet
        if (!(LongJumpedPlayers.Contains(userID)))
        {
            LongJumpedPlayers.Add(userID);
            var mb = Instantiate(MarbleObject, LongJumpLocation.position, transform.rotation);
            mb.transform.SetParent(transform);
            mb.GetComponentInChildren<SpriteRenderer>().sortingOrder = OrderInLayer;
            OrderInLayer++;
            PlayerMarble = mb.GetComponentInChildren<MarbleObject>();
            PlayerMarble.PlayerName.text = displayName;
            int marbleIndex = GameData.GetPlayerEquipSkin(userID);
            GameObject marbleGameObject = MarbleList.GetMarbleFromMarbleCode(marbleIndex);
            PlayerMarble.GameMarbleSprite.sprite = marbleGameObject.GetComponent<Marble>().MarbleSprite;
            PlayerMarble.PlayerId = userID;
        }
        else //if player has already rolled
        {
            if (GameData.CheckPlayerMoney(userID) > CostToReroll)
            {
                MarbleObject[] allMarbles = GetComponentsInChildren<MarbleObject>();
                foreach (var marble in allMarbles)
                {
                    if (marble.PlayerId == userID && !(marble.IsRolling))
                    {
                        Destroy(marble.transform.parent.gameObject);
                        GameData.SubtractMoneyFromPlayerID(CostToReroll, userID);
                        LongJumpedPlayers.Remove(userID);
                        CreateMarbleAndJump(e);
                    }
                }
            }
            //if marble object isRolling do nothing
            //if marble object is not Rolling then reroll and charge player 50 monies
        }
    }

    public void CreateMarbleAndHighJump(CommandEventArgs e)
    {
        string userID = e.UserID;
        string displayName = e.DisplayName;
        if (!(HighJumpedPlayers.Contains(userID)))
        {
            HighJumpedPlayers.Add(userID);
            var mb = Instantiate(MarbleObject, LongJumpLocation.position, transform.rotation);
            mb.transform.SetParent(transform);
            mb.GetComponentInChildren<SpriteRenderer>().sortingOrder = OrderInLayer;
            OrderInLayer++;
            PlayerMarble = mb.GetComponentInChildren<MarbleObject>();
            PlayerMarble.PlayerName.text = displayName;
            int marbleIndex = GameData.GetPlayerEquipSkin(userID);
            GameObject marbleGameObject = MarbleList.GetMarbleFromMarbleCode(marbleIndex);
            PlayerMarble.GameMarbleSprite.sprite = marbleGameObject.GetComponent<Marble>().MarbleSprite;
            PlayerMarble.PlayerId = userID;
        }
    }

    public IEnumerator DestroyMarbles()
    {
        MarbleObject[] allMarbles = GetComponentsInChildren<MarbleObject>();

        foreach (var marble in allMarbles)
        {
            while (marble.IsRolling == true)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return new WaitForSeconds(5f);
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        LongJumpedPlayers = new List<string>();
        OrderInLayer = 0;
    }


    //playerMarble.marbleSprite = 
    /*playerMarble = mb.GetComponent<Marble>();
    playerSpriteRenderer = mb.GetComponent<SpriteRenderer>();
    playerMarble.playerName
    e.Command.ChatMessage.Username*/
}

