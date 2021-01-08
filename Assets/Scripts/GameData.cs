using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //game data is in the form keys (unique ID) and data in the form of Player Data (see Player Data Script)
    Dictionary<string, PlayerData> Data = new Dictionary<string, PlayerData>();
    public DataManager Manager;
    MarbleList MarbleList;
    List<PlayerData> PlayerDataList;//GameData.values()
    List<string> GameDataKeyList;

    void Start()
    {
        //Load game data at start of game from JSON
        Dictionary<string, PlayerData> deserializedProduct = JsonConvert.DeserializeObject<Dictionary<string, PlayerData>>(Manager.Load());
        Data = deserializedProduct;
        MarbleList = FindObjectOfType<MarbleList>();
        Data = MarbleList.AddNewMarblesToGameData(Data);
        Manager.NewSave(Data);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayerData tempData = new PlayerData();
            tempData.Money = 0;
            tempData.Skins = FindObjectOfType<MarbleList>().getEmptyAllMarbleDictionary();
            Data.Add("123456", tempData);
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

        if (Data.ContainsKey(playerID))
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
        tempData.Money = 0;
        tempData.Skins = FindObjectOfType<MarbleList>().getEmptyAllMarbleDictionary();
        tempData.ActiveSkin = 0;
        tempData.PlayerName = e.DisplayName;
        tempData.IsSubscribed = false;
        Data.Add(e.UserID, tempData);
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public void AddMoneyToPlayerID(int money, string playerID)
    {
        Data[playerID].Money += money;
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public void SubtractMoneyFromPlayerID(int money, string playerID)
    {
        Data[playerID].Money -= money;
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public int CheckPlayerMoney(string playerID)
    {
        return Data[playerID].Money;
    }
    public bool IsSkinUnlocked(string playerID, int skinIndex)
    {
        if (Data[playerID].Skins[skinIndex] == true)
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
        Data[playerID].Skins[skinIndex] = true;
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public void SetPlayerEquipSkin(string playerID, int skinIndex)
    {
        Data[playerID].ActiveSkin = skinIndex;
        SaveGameDataToTXT();
        Debug.Log("GAME DATA SUCCESFULLY SAVED!!!!");
    }
    public int GetPlayerEquipSkin(string playerID)
    {
        return Data[playerID].ActiveSkin;
    }
    public void SaveGameDataToTXT()
    {
        Manager.NewSave(Data);
        Debug.Log("GAME DATA SUCCESSFULLY SAVED!!!!");
    }
    public bool CheckIfPlayerSubscribedToWhispers(string playerID)
    {
        return Data[playerID].IsSubscribed;
        //Returns true if player subscribed 
    }

    public string CheckSkins(Arrrgs e)
    {
        string playerID = e.UserID;
        string playerName = e.DisplayName;
        string skinList = playerName + ": ";
        for (int i = 0; i < Data[playerID].Skins.Count; i++)
        {
            if (Data[playerID].Skins[i] == true)
            {
                skinList += MarbleList.MarbleCodeToCommonName[i] + ", ";
            }
        }
        return skinList;
    }
    public string ConvertCommonNameToUserID(string commonName)
    {
        //var myKey = dictionary.FirstOrDefault(x => x.Value == "one").Key;
        PlayerDataList = new List<PlayerData>(Data.Values);
        for (int i = 0; i < PlayerDataList.Count; i++)
        {
            if (PlayerDataList[i].PlayerName.Equals(commonName, System.StringComparison.CurrentCultureIgnoreCase))
            {
                GameDataKeyList = new List<string>(Data.Keys);
                return GameDataKeyList[i];
            }
        }
        return "";
    }
    public bool CheckPlayerIDMatchesUserName(string userID, string name)
    {
        if (Data[userID].PlayerName == name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
