using System;
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
    TwitchClient twitchClient;
    Client chatClient;
    JoinedChannel chatJoinedChannel;
    JoinedChannel botJoinedChannel;
    [SerializeField] GameData gameDataScript;
    [SerializeField] MarbleList marbleList;
    [SerializeField] GameController gameController;
    [SerializeField] JumpManager jumpManager;
    [SerializeField] Shop shop;
    const string secretMsg = " is hacking";
    const string help = "!join-join the game | !play-play the game when it is GAMETIME | play to earn money" +
        " |  save money to buy and equip new marbles";
    const string playerAlreadyExists = " your user entry already exists, no need to join";
    const string noPlayerEntryExists = ", Please type '!join' to play";
    const string playerEntryAdded = " you have joined the game";
    const string noMarbleWithNameExists = ", there is no marble with that name. Please type a valid marble name.";
    const string unlockedMarble1 = " has unlocked the ";
    const string unlockedMarble2 = " marble. Use '!equip ";
    const string unlockedMarble3 = "' to use your new marble.";
    const string unlockedMarble4 = " Current Balance: ";
    const string notEnoughMoney = ", you do not have enough money to unlock ";
    const string skinAlreadyUnlocked1 = ", you already have the ";
    const string skinAlreadyUnlocked2 = " marble unlocked.";
    const string dontOwnThatSkin = ", you dont own that skin. Type !skins to see the skins you own.";
    const string notSubscribed1 = ", you can not use this command unless you give Simpagamebot permission to whisper your Twitch";
    const string notSubscribed2 = "Please type !acceptwhispers you can type !stopwhispers at any time to revoke whisper permissions.";
    const string marbleNotInShop = " marble is not in shop";
    const string cantGiveMoneyToPlayer1 = "Can not give money to ";
    const string cantGiveMoneyToPlayer2 = " because they are not entered in the game.";

    private void Start()
    {
        //chatClient.SendMessage(chatJoinedChannel, help);
        //Setup();
    }
    public void Setup()
    {
        twitchClient = FindObjectOfType<TwitchClient>();
        chatClient = twitchClient.client;
        chatJoinedChannel = twitchClient.joinedChannel;
        botJoinedChannel = twitchClient.botChannel;
    }

    //Help - provids a list of commands
    public void Help(Arrrgs e)
    {
        if (chatClient == null)
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
        chatClient.SendMessage(chatJoinedChannel, help);
    }

    //Join - check if player data exists - if not create empty player data entry
    public void Join(Arrrgs e)
    {
        if (chatClient == null)
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
        if (gameDataScript.CheckIfPlayerExists(e.userID))
        {
            chatClient.SendMessage(chatJoinedChannel, e.displayName + playerAlreadyExists);
        }
        else
        {
            gameDataScript.CreateNewPlayerEntry(e);
            chatClient.SendMessage(chatJoinedChannel, e.displayName + playerEntryAdded);
        }
    }


    //Money - checks if player data exists - if so returns how much money they have in chat
    public void money(Arrrgs e)
    {
        if (chatClient == null)
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
        string playerID = e.userID;
        string userName = e.displayName;

        if (gameDataScript.CheckIfPlayerExists(playerID))
        {
            chatClient.SendMessage(chatJoinedChannel, userName + " money: " + gameDataScript.CheckPlayerMoney(playerID));
        }
        else
        {
            chatClient.SendMessage(chatJoinedChannel, userName + noPlayerEntryExists);
        }
    }


    //Buy - checks if player data exists - if so checks if has enough money - if so then unlock skin
    public void Buy(Arrrgs e)
    {
        if (chatClient == null)
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
        string playerID = e.userID; //Command.ChatMessage.UserId;
        string playerUserName = e.displayName; //Command.ChatMessage.Username;
        if (gameDataScript.CheckIfPlayerExists(playerID))
        {
            commonName = e.commandArgs;
            /*
            for (int index = 0; index < e.Command.ArgumentsAsList.Count; index++)
            {
                commonName += e.Command.ArgumentsAsList[index].ToLower();
            }*/
            Debug.Log(commonName);
            if (marbleList.DoesMarbleCommonNameExist(commonName))
            {
                if (shop.MarbleNamesInShop(commonName))
                {
                    int playerMoney = gameDataScript.CheckPlayerMoney(playerID);
                    int marbleCost = marbleList.GetMarbleCostFromCommonName(commonName);
                    int marbleCode = marbleList.GetMarbleCodeFromCommonName(commonName);
                    if (gameDataScript.IsSkinUnlocked(playerID, marbleCode))
                    {
                        chatClient.SendMessage(chatJoinedChannel, playerUserName + skinAlreadyUnlocked1 + commonName + skinAlreadyUnlocked2);
                    }
                    else
                    {
                        if (playerMoney >= marbleCost)
                        {
                            gameDataScript.SubtractMoneyFromPlayerID(marbleCost, playerID);
                            gameDataScript.UnlockSkinForPlayer(playerID, marbleCode);
                            int currentMoney = gameDataScript.CheckPlayerMoney(playerID);
                            chatClient.SendMessage(chatJoinedChannel, playerUserName + unlockedMarble1 +
                                commonName + unlockedMarble2 + commonName + unlockedMarble3 + unlockedMarble4 + currentMoney);
                        }
                        else
                        {
                            chatClient.SendMessage(chatJoinedChannel, playerUserName + notEnoughMoney + commonName);
                        }
                    }
                }
                else
                {
                    chatClient.SendMessage(chatJoinedChannel, commonName + marbleNotInShop);
                }
            }
            else
            {
                chatClient.SendMessage(chatJoinedChannel, playerUserName + noMarbleWithNameExists);
            }
        }
        else
        {
            chatClient.SendMessage(chatJoinedChannel, playerUserName + noPlayerEntryExists);
        }
    }


    //Equip - checks if player data exists - checks if they own that skin - equips the skin
    public void Equip(Arrrgs e)
    {
        if (chatClient == null)
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
        string playerID = e.userID;
        string playerUserName = e.displayName;
        commonName = e.commandArgs;
        /*
        for (int index = 0; index < e.Command.ArgumentsAsList.Count; index++)
        {
            commonName += e.Command.ArgumentsAsList[index].ToLower();
        }*/

        if (gameDataScript.CheckIfPlayerExists(playerID))
        {
            if (marbleList.DoesMarbleCommonNameExist(commonName))
            {
                int marbleCode = marbleList.GetMarbleCodeFromCommonName(commonName);
                if (gameDataScript.IsSkinUnlocked(playerID, marbleCode))
                {
                    gameDataScript.SetPlayerEquipSkin(playerID, marbleCode);
                    chatClient.SendMessage(chatJoinedChannel, playerUserName + ", you now have the " + commonName + " skin in use.");
                    Debug.Log(playerUserName + " equipt " + commonName);
                }
                else
                {
                    chatClient.SendMessage(chatJoinedChannel, playerUserName + dontOwnThatSkin);
                }
            }
            else
            {
                chatClient.SendMessage(chatJoinedChannel, playerUserName + noMarbleWithNameExists);
            }
        }
        else
        {
            chatClient.SendMessage(chatJoinedChannel, playerUserName + noPlayerEntryExists);
        }
    }


    //Equipted - checks if player data exists - checks what skin they have equipped - tells them what skin that is
    public void InUse(Arrrgs e)
    {
        if (chatClient == null)
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
        string playerID = e.userID;
        string playerUserName = e.displayName;

        if (gameDataScript.CheckIfPlayerExists(playerID))
        {
            int marbleCode = gameDataScript.GetPlayerEquipSkin(playerID);
            string commonName = marbleList.GetCommonNameFromMarbleCode(marbleCode);
            chatClient.SendMessage(chatJoinedChannel, playerUserName + " is using the " + commonName + " skin!");
        }
        else
        {
            chatClient.SendMessage(chatJoinedChannel, playerUserName + noPlayerEntryExists);
        }

    }
    public void Play(Arrrgs e)
    {
        if (chatClient == null)
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
        string userID = e.userID;
        string displayName = e.displayName;

        if (gameDataScript.CheckIfPlayerExists(userID))
        {
            if (gameController.currentState == gameState.gametime)
            {
                if (gameController.currentGameMode == gameMode.longjump)
                {
                    jumpManager.CreateMarbleAndJump(e);
                }
                else if (gameController.currentGameMode == gameMode.highjump)
                {
                    jumpManager.CreateMarbleAndHighJump(e);
                }
                else if (gameController.currentGameMode == gameMode.race)
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
            chatClient.SendMessage(chatJoinedChannel, displayName + noPlayerEntryExists);
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
        if (chatClient == null)
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
        string userID = e.userID;
        string displayName = e.displayName;

        if (gameDataScript.CheckIfPlayerExists(userID))
        {
            string skinsPlayerOwns = gameDataScript.CheckSkins(e);
            StartCoroutine(SkinsMessege(skinsPlayerOwns));
            chatClient.SendMessage(chatJoinedChannel, "https://www.twitch.tv/simpagamebot");
        }
        else
        {
            chatClient.SendMessage(chatJoinedChannel, displayName + noPlayerEntryExists);
        }
    }
    IEnumerator SkinsMessege(string skinsList)
    {
        yield return new WaitForSeconds(7);
        chatClient.SendMessage(botJoinedChannel, skinsList);
    }
    public void AkaiEasterEgg(string name)
    {
        chatClient.SendMessage(chatJoinedChannel, name + secretMsg);
    }

    public void Rotate(Arrrgs e) //Temporary command!!! TODO REMOVE
    {
        if (e.userID == "73184979")
        {
            FindObjectOfType<Shop>().ResetShop();
        }
    }
    public void Give(Arrrgs e)
    {
        if (chatClient == null)
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
        if (e.multiCommand != null)
        {
            string userID = e.userID;
            string displayName = e.displayName;
            if (gameDataScript.CheckIfPlayerExists(userID))
            {
                if (e.multiCommand.Count >= 2)
                {
                    string otherPlayerDisplayName = e.multiCommand[0].TrimStart('@');
                    string PersonGettingMoney = gameDataScript.ConvertCommonNameToUserID(otherPlayerDisplayName);
                    if (String.IsNullOrEmpty(PersonGettingMoney))
                    {
                        chatClient.SendMessage(chatJoinedChannel, cantGiveMoneyToPlayer1 + otherPlayerDisplayName + cantGiveMoneyToPlayer2);
                    }
                    else
                    {
                        if (gameDataScript.CheckIfPlayerExists(PersonGettingMoney) &&
                       gameDataScript.CheckPlayerIDMatchesUserName(PersonGettingMoney, otherPlayerDisplayName))
                        {
                            int cost;
                            //money = int.TryParse((e.multiCommand[1]), money);
                            if (int.TryParse((e.multiCommand[1]), out cost))
                            {
                                int currentMoney = gameDataScript.CheckPlayerMoney(userID);
                                if (currentMoney >= cost)
                                {
                                    if (cost > 0 && cost <= 10000)
                                    {
                                        gameDataScript.SubtractMoneyFromPlayerID(cost, userID);
                                        gameDataScript.AddMoneyToPlayerID(cost, PersonGettingMoney);
                                        chatClient.SendMessage(chatJoinedChannel, displayName + " gave " + e.multiCommand[0] + " " + cost);
                                    }
                                    else
                                    {
                                        chatClient.SendMessage(chatJoinedChannel, displayName + " you can only give give between 1 and 10000");
                                    }
                                }
                                else
                                {
                                    chatClient.SendMessage(chatJoinedChannel, displayName + " you can't give money you dont have.");
                                }
                            }
                        }
                        else
                        {
                            chatClient.SendMessage(chatJoinedChannel, cantGiveMoneyToPlayer1 + e.multiCommand[0] + cantGiveMoneyToPlayer2);
                        }
                    }
                }
                else
                {
                    chatClient.SendMessage(chatJoinedChannel, displayName + " to give use !give [PlayerName] [Amount]");
                }
            }
            else
            {
                chatClient.SendMessage(chatJoinedChannel, displayName + noPlayerEntryExists);
            }
        }
    }
}
