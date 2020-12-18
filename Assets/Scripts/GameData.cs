using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;

public class GameData : MonoBehaviour
{
    //game data is in the form keys (unique ID) and data in the form of Player Data (see Player Data Script)
    Dictionary<string, PlayerData> gameData = new Dictionary<string, PlayerData>();
    public DataManager dataManager;
    MarbleList marbleList;
    List<PlayerData> playerDataList;//GameData.values()
    List<string> gameDataKeyList;

    void Start()
    {
        //Load game data at start of game from JSON
        Dictionary<string, PlayerData> deserializedProduct = JsonConvert.DeserializeObject<Dictionary<string, PlayerData>>(dataManager.Load());
        gameData = deserializedProduct;
        marbleList = FindObjectOfType<MarbleList>();
        gameData = marbleList.AddNewMarblesToGameData(gameData);
        dataManager.NewSave(gameData);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayerData tempData = new PlayerData();
            tempData.money = 0;
            tempData.skins = FindObjectOfType<MarbleList>().getEmptyAllMarbleDictionary();
            gameData.Add("123456", tempData);
            //var convertedDictionary = FindObjectOfType<SpriteDictionary>().getEmptyAllSpritesDictionary().ToDictionary(item => item.Key.ToString(), item => item.Value.ToString());
            Debug.Log("pressing 7");
            //JsonConvert.SerializeObject(dataManager);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SaveGameDataToTXT();
            Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
        }
    }
    public bool CheckIfPlayerExists(string playerID)
    {

        if (gameData.ContainsKey(playerID) )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void CreateNewPlayerEntry(Arrrgs e)
    {
        PlayerData tempData = new PlayerData();
        tempData.money = 0;
        tempData.skins = FindObjectOfType<MarbleList>().getEmptyAllMarbleDictionary();
        tempData.equiptSkin = 0;
        tempData.playerName = e.displayName;
        tempData.isSubscribed = false;
        gameData.Add(e.userID, tempData);
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public void AddMoneyToPlayerID(int money, string playerID)
    {
        gameData[playerID].money += money;
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public void SubtractMoneyFromPlayerID(int money, string playerID)
    {
        gameData[playerID].money -= money;
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public int CheckPlayerMoney(string playerID)
    {
        return gameData[playerID].money;
    }
    public bool IsSkinUnlocked(string playerID, int skinIndex)
    {
        if (gameData[playerID].skins[skinIndex] == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UnlockSkinForPlayer(string playerID, int skinIndex)
    {
        gameData[playerID].skins[skinIndex] = true;
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public void SetPlayerEquipSkin(string playerID, int skinIndex)
    {
        gameData[playerID].equiptSkin = skinIndex;
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public int GetPlayerEquipSkin(string playerID)
    {
        return gameData[playerID].equiptSkin;
    }
    public void SaveGameDataToTXT()
    {
        dataManager.NewSave(gameData);
        Debug.Log("GAME DATA SUCCESSFULLY SAVED!!!!");
    }
    public bool CheckIfPlayerSubscribedToWhispers(string playerID)
    {
        return gameData[playerID].isSubscribed;
        //Returns true if player subscribed 
    }

    public string CheckSkins(Arrrgs e)
    {
        string playerID = e.userID;
        string playerName = e.displayName;
        string skinList = playerName+": ";
        for (int i = 0; i < gameData[playerID].skins.Count; i++)
        {
            if (gameData[playerID].skins[i] == true)
            {
                skinList += marbleList.marbleCodeToCommonName[i] + ", ";
            }
        }
        return skinList;
    }
    public string ConvertCommonNameToUserID(string commonName)
    {
        //var myKey = dictionary.FirstOrDefault(x => x.Value == "one").Key;
        playerDataList = new List<PlayerData>(gameData.Values);
        for (int i = 0; i < playerDataList.Count; i++)
        {
            if (playerDataList[i].playerName.Equals(commonName,System.StringComparison.CurrentCultureIgnoreCase))
            {
                gameDataKeyList = new List<string>(gameData.Keys);
                return gameDataKeyList[i];
            }
        }
        return "";
    }
    public bool CheckPlayerIDMatchesUserName(string userID, string name)
    {
        if (gameData[userID].playerName == name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
