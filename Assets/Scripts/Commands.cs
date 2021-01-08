﻿using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using UnityEngine;

public class Commands : MonoBehaviour
{
    //ON THIS DAY WE GATHER TO PUT AN END TO THE WAR THAT HAS WAGED ON SIMPATHEYS BLOOD PRESSURE
    //RAVONUS, PHYRODARKMATTER, BEGINBOT AND PUNCHYPENGUIN WAGED WAR ALL THE WAY TO THE 200% HYPE TRAIN
    //BUT IT DIDNT END THERE, THE BATTLE SPILLED OUT AND EFFECTED THE LIVES OF ALL BUG CLUB MEMBERS FOLLOWERS AND
    //LURKERS ALIKE. ON THIS DAY AND HOUR THE TREATY IS SIGNED BY ALL PARTIES SO THAT SIMPATHEY DOESNT DIE
    //BEGINBOT HAS SIGNED 
    //RAVONUS UNDECIDED 
    //PHYRODARKMATTER UNDECIDED 
    //PUNCHYPENGUIN HAS SIGNED
    //CODING CUBER HAS SIGNED 
    //MOTTZYMAKES 
    TwitchClient TwitchClient;
    Client ChatClient;
    JoinedChannel ChatJoinedChannel;
    JoinedChannel BotJoinedChannel;
    [SerializeField] GameData GameDataScript;
    [SerializeField] MarbleList MarbleList;
    [SerializeField] GameController GameController;
    [SerializeField] JumpManager JumpManager;
    [SerializeField] Shop Shop;
    const string SecretMsg = " is hacking";
    const string Help = "!join-join the game | !play-play the game when it is GAMETIME | play to earn money" +
        " |  save money to buy and equip new marbles";
    const string PlayerAlreadyExists = " your user entry already exists, no need to join";
    const string NoPlayerEntryExists = ", Please type '!join' to play";
    const string PlayerEntryAdded = " you have joined the game";
    const string NoMarbleWithNameExists = ", there is no marble with that name. Please type a valid marble name.";
    const string UnlockedMarble1 = " has unlocked the ";
    const string UnlockedMarble2 = " marble. Use '!equip ";
    const string UnlockedMarble3 = "' to use your new marble.";
    const string UnlockedMarble4 = " Current Balance: ";
    const string NotEnoughMoney = ", you do not have enough money to unlock ";
    const string SkinAlreadyUnlocked1 = ", you already have the ";
    const string SkinAlreadyUnlocked2 = " marble unlocked.";
    const string DontOwnThatSkin = ", you dont own that skin. Type !skins to see the skins you own.";
    const string NotSubscribed1 = ", you can not use this command unless you give Simpagamebot permission to whisper your Twitch";
    const string NotSubscribed2 = "Please type !acceptwhispers you can type !stopwhispers at any time to revoke whisper permissions.";
    const string MarbleNotInShop = " marble is not in shop";
    const string CantGiveMoneyToPlayer1 = "Can not give money to ";
    const string CantGiveMoneyToPlayer2 = " because they are not entered in the game.";

    private void Start()
    {
        //chatClient.SendMessage(chatJoinedChannel, help);
        //Setup();
    }
    public void Setup()
    {
        TwitchClient = FindObjectOfType<TwitchClient>();
        ChatClient = TwitchClient.Client;
        ChatJoinedChannel = TwitchClient.JoinedChannel;
        BotJoinedChannel = TwitchClient.BotChannel;
    }

    //Help - provids a list of commands
    public void Help(Arrrgs e)
    {
        if (ChatClient == null)
        {
            Setup();
            //chatClient.SendMessage(chatJoinedChannel, help);
            AttemptToHelp(e);
        }
        else
        {
            AttemptToHelp(e);
        }
    }

    private void AttemptToHelp(Arrrgs e)
    {
        ChatClient.SendMessage(ChatJoinedChannel, help);
    }

    //Join - check if player data exists - if not create empty player data entry
    public void Join(Arrrgs e)
    {
        if (ChatClient == null)
        {
            Setup();
            AttemptToJoin(e);
        }
        else
        {
            AttemptToJoin(e);
        }
    }
    private void AttemptToJoin(Arrrgs e)
    {
        if (GameDataScript.CheckIfPlayerExists(e.UserID))
        {
            ChatClient.SendMessage(ChatJoinedChannel, e.DisplayName + playerAlreadyExists);
        }
        else
        {
            GameDataScript.CreateNewPlayerEntry(e);
            ChatClient.SendMessage(ChatJoinedChannel, e.DisplayName + playerEntryAdded);
        }
    }


    //Money - checks if player data exists - if so returns how much money they have in chat
    public void money(Arrrgs e)
    {
        if (ChatClient == null)
        {
            Setup();
            AttemptToCheckMoney(e);
        }
        else
        {
            AttemptToCheckMoney(e);
        }
    }
    private void AttemptToCheckMoney(Arrrgs e)
    {
        string playerID = e.UserID;
        string userName = e.DisplayName;

        if (GameDataScript.CheckIfPlayerExists(playerID))
        {
            ChatClient.SendMessage(ChatJoinedChannel, userName + " money: " + GameDataScript.CheckPlayerMoney(playerID));
        }
        else
        {
            ChatClient.SendMessage(ChatJoinedChannel, userName + noPlayerEntryExists);
        }
    }


    //Buy - checks if player data exists - if so checks if has enough money - if so then unlock skin
    public void Buy(Arrrgs e)
    {
        if (ChatClient == null)
        {
            Setup();
            AttemptToBuy(e);
        }
        else
        {
            AttemptToBuy(e);
        }
    }
    public void AttemptToBuy(Arrrgs e)
    {
        string commonName = "";
        string playerID = e.UserID; //Command.ChatMessage.UserId;
        string playerUserName = e.DisplayName; //Command.ChatMessage.Username;
        if (GameDataScript.CheckIfPlayerExists(playerID))
        {
            commonName = e.CommandArgs;
            /*
            for (int index = 0; index < e.Command.ArgumentsAsList.Count; index++)
            {
                commonName += e.Command.ArgumentsAsList[index].ToLower();
            }*/
            Debug.Log(commonName);
            if (MarbleList.DoesMarbleCommonNameExist(commonName))
            {
                if (Shop.MarbleNamesInShop(commonName))
                {
                    int playerMoney = GameDataScript.CheckPlayerMoney(playerID);
                    int marbleCost = MarbleList.GetMarbleCostFromCommonName(commonName);
                    int marbleCode = MarbleList.GetMarbleCodeFromCommonName(commonName);
                    if (GameDataScript.IsSkinUnlocked(playerID, marbleCode))
                    {
                        ChatClient.SendMessage(ChatJoinedChannel, playerUserName + skinAlreadyUnlocked1 + commonName + skinAlreadyUnlocked2);
                    }
                    else
                    {
                        if (playerMoney >= marbleCost)
                        {
                            GameDataScript.SubtractMoneyFromPlayerID(marbleCost, playerID);
                            GameDataScript.UnlockSkinForPlayer(playerID, marbleCode);
                            int currentMoney = GameDataScript.CheckPlayerMoney(playerID);
                            ChatClient.SendMessage(ChatJoinedChannel, playerUserName + unlockedMarble1 +
                                commonName + unlockedMarble2 + commonName + unlockedMarble3 + unlockedMarble4 + currentMoney);
                        }
                        else
                        {
                            ChatClient.SendMessage(ChatJoinedChannel, playerUserName + notEnoughMoney + commonName);
                        }
                    }
                }
                else
                {
                    ChatClient.SendMessage(ChatJoinedChannel, commonName + marbleNotInShop);
                }
            }
            else
            {
                ChatClient.SendMessage(ChatJoinedChannel, playerUserName + noMarbleWithNameExists);
            }
        }
        else
        {
            ChatClient.SendMessage(ChatJoinedChannel, playerUserName + noPlayerEntryExists);
        }
    }


    //Equip - checks if player data exists - checks if they own that skin - equips the skin
    public void Equip(Arrrgs e)
    {
        if (ChatClient == null)
        {
            Setup();
            AttemptToEquip(e);
        }
        else
        {
            AttemptToEquip(e);
        }
    }
    private void AttemptToEquip(Arrrgs e)
    {
        string commonName = "";
        string playerID = e.UserID;
        string playerUserName = e.DisplayName;
        commonName = e.CommandArgs;
        /*
        for (int index = 0; index < e.Command.ArgumentsAsList.Count; index++)
        {
            commonName += e.Command.ArgumentsAsList[index].ToLower();
        }*/

        if (GameDataScript.CheckIfPlayerExists(playerID))
        {
            if (MarbleList.DoesMarbleCommonNameExist(commonName))
            {
                int marbleCode = MarbleList.GetMarbleCodeFromCommonName(commonName);
                if (GameDataScript.IsSkinUnlocked(playerID, marbleCode))
                {
                    GameDataScript.SetPlayerEquipSkin(playerID, marbleCode);
                    ChatClient.SendMessage(ChatJoinedChannel, playerUserName + ", you now have the " + commonName + " skin in use.");
                    Debug.Log(playerUserName + " equipt " + commonName);
                }
                else
                {
                    ChatClient.SendMessage(ChatJoinedChannel, playerUserName + dontOwnThatSkin);
                }
            }
            else
            {
                ChatClient.SendMessage(ChatJoinedChannel, playerUserName + noMarbleWithNameExists);
            }
        }
        else
        {
            ChatClient.SendMessage(ChatJoinedChannel, playerUserName + noPlayerEntryExists);
        }
    }


    //Equipted - checks if player data exists - checks what skin they have equipped - tells them what skin that is
    public void InUse(Arrrgs e)
    {
        if (ChatClient == null)
        {
            Setup();
            AttemptToInUse(e);
        }
        else
        {
            AttemptToInUse(e);
        }
    }
    private void AttemptToInUse(Arrrgs e)
    {
        string playerID = e.UserID;
        string playerUserName = e.DisplayName;

        if (GameDataScript.CheckIfPlayerExists(playerID))
        {
            int marbleCode = GameDataScript.GetPlayerEquipSkin(playerID);
            string commonName = MarbleList.GetCommonNameFromMarbleCode(marbleCode);
            ChatClient.SendMessage(ChatJoinedChannel, playerUserName + " is using the " + commonName + " skin!");
        }
        else
        {
            ChatClient.SendMessage(ChatJoinedChannel, playerUserName + noPlayerEntryExists);
        }

    }
    public void Play(Arrrgs e)
    {
        if (ChatClient == null)
        {
            Setup();
            AttemptToPlay(e);
        }
        else
        {
            AttemptToPlay(e);
        }
    }

    private void AttemptToPlay(Arrrgs e)
    {
        string userID = e.UserID;
        string displayName = e.DisplayName;

        if (GameDataScript.CheckIfPlayerExists(userID))
        {
            if (GameController.CurrentState == gameState.gametime)
            {
                if (GameController.CurrentGameMode == gameMode.longjump)
                {
                    JumpManager.CreateMarbleAndJump(e);
                }
                else if (GameController.CurrentGameMode == gameMode.highjump)
                {
                    JumpManager.CreateMarbleAndHighJump(e);
                }
                else if (GameController.CurrentGameMode == gameMode.race)
                {

                }
            }
            else
            {
                return;
            }
        }
        else
        {
            ChatClient.SendMessage(ChatJoinedChannel, displayName + noPlayerEntryExists);
        }
    }
    //AcceptWhispers
    /*
    public void AcceptWhispers(OnChatCommandReceivedArgs e)
    {
        if (chatClient == null)
        {
            Setup();
            AttemptToAcceptWhispers(e);
        }
        else
        {
            AttemptToAcceptWhispers(e);
        }
    }
    */
    /*public void AttemptToAcceptWhispers(OnChatCommandReceivedArgs e)
    {
        Debug.Log("WHISPER,PLAYER ACTIVATED!");
        string userID = e.Command.ChatMessage.UserId;
        string playerName = e.Command.ChatMessage.Username;
        if (gameDataScript.CheckIfPlayerExists(userID))
        {
            Debug.Log("WHISPER,PLAYER DONE!");
            gameDataScript.SubscribePlayerToWhispers(userID);
            chatClient.SendWhisper(playerName, "THIS IS A TEST MESSEGE FROM SIMPA GAME BOT.");
        }
        else
        {
            chatClient.SendMessage(chatJoinedChannel, e.Command.ChatMessage.Username + noPlayerEntryExists);
        }
    }
    */
    public void Skins(Arrrgs e)
    {
        if (ChatClient == null)
        {
            Setup();
            AttemptToSkins(e);
        }
        else
        {
            AttemptToSkins(e);
        }
    }
    public void AttemptToSkins(Arrrgs e)
    {
        string userID = e.UserID;
        string displayName = e.DisplayName;

        if (GameDataScript.CheckIfPlayerExists(userID))
        {
            string skinsPlayerOwns = GameDataScript.CheckSkins(e);
            StartCoroutine(SkinsMessege(skinsPlayerOwns));
            ChatClient.SendMessage(ChatJoinedChannel, "https://www.twitch.tv/simpagamebot");
        }
        else
        {
            ChatClient.SendMessage(ChatJoinedChannel, displayName + noPlayerEntryExists);
        }
    }
    IEnumerator SkinsMessege(string skinsList)
    {
        yield return new WaitForSeconds(7);
        ChatClient.SendMessage(BotJoinedChannel, skinsList);
    }
    public void AkaiEasterEgg(string name)
    {
        ChatClient.SendMessage(ChatJoinedChannel, name + secretMsg);
    }

    public void Rotate(Arrrgs e) //Temporary command!!! TODO REMOVE
    {
        if (e.UserID == "73184979")
        {
            FindObjectOfType<Shop>().ResetShop();
        }
    }
    public void Give(Arrrgs e)
    {
        if (ChatClient == null)
        {
            Setup();
            AttemptToGive(e);
        }
        else
        {
            AttemptToGive(e);
        }
    }
    public void AttemptToGive(Arrrgs e)
    {
        if (e.MultiCommand != null)
        {
            string userID = e.UserID;
            string displayName = e.DisplayName;
            if (GameDataScript.CheckIfPlayerExists(userID))
            {
                if (e.MultiCommand.Count >= 2)
                {
                    string otherPlayerDisplayName = e.MultiCommand[0].TrimStart('@');
                    string PersonGettingMoney = GameDataScript.ConvertCommonNameToUserID(otherPlayerDisplayName);
                    if (String.IsNullOrEmpty(PersonGettingMoney))
                    {
                        ChatClient.SendMessage(ChatJoinedChannel, cantGiveMoneyToPlayer1 + otherPlayerDisplayName + cantGiveMoneyToPlayer2);
                    }
                    else
                    {
                        if (GameDataScript.CheckIfPlayerExists(PersonGettingMoney) &&
                       GameDataScript.CheckPlayerIDMatchesUserName(PersonGettingMoney, otherPlayerDisplayName))
                        {
                            int cost;
                            //money = int.TryParse((e.multiCommand[1]), money);
                            if (int.TryParse((e.MultiCommand[1]), out cost))
                            {
                                int currentMoney = GameDataScript.CheckPlayerMoney(userID);
                                if (currentMoney >= cost)
                                {
                                    if (cost > 0 && cost <= 10000)
                                    {
                                        GameDataScript.SubtractMoneyFromPlayerID(cost, userID);
                                        GameDataScript.AddMoneyToPlayerID(cost, PersonGettingMoney);
                                        ChatClient.SendMessage(ChatJoinedChannel, displayName + " gave " + e.MultiCommand[0] + " " + cost);
                                    }
                                    else
                                    {
                                        ChatClient.SendMessage(ChatJoinedChannel, displayName + " you can only give give between 1 and 10000");
                                    }
                                }
                                else
                                {
                                    ChatClient.SendMessage(ChatJoinedChannel, displayName + " you can't give money you dont have.");
                                }
                            }
                        }
                        else
                        {
                            ChatClient.SendMessage(ChatJoinedChannel, cantGiveMoneyToPlayer1 + e.MultiCommand[0] + cantGiveMoneyToPlayer2);
                        }
                    }
                }
                else
                {
                    ChatClient.SendMessage(ChatJoinedChannel, displayName + " to give use !give [PlayerName] [Amount]");
                }
            }
            else
            {
                ChatClient.SendMessage(ChatJoinedChannel, displayName + noPlayerEntryExists);
            }
        }
    }
}
