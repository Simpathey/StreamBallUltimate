﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerData Data;
    public string File = "GameData.txt";

    public void Save()
    {
        Debug.Log(Data);
        string json = JsonConvert.SerializeObject(Data);
        //WriteToFile(file, json);
        System.IO.File.WriteAllText(@"D:\SimpaGameBotData\GameData.txt", json);
    }

    public void NewSave(Dictionary<string, PlayerData> gameData)
    {
        //Code saves at this point to our text file 
        string json = JsonConvert.SerializeObject(gameData, Formatting.Indented);
        System.IO.File.WriteAllText(@"D:\SimpaGameBotData\GameData.txt", json);
    }
    public void Backup(Dictionary<string, PlayerData> gameData)
    {
        //Code saves at this point to our text file 
        Debug.Log(Data);
        string json = JsonConvert.SerializeObject(gameData, Formatting.Indented);
        System.IO.File.WriteAllText(@"D:\SimpaGameBotData\GameData.txt", json);
    }

    public string Load()
    {
        Data = new PlayerData();
        string json = ReadFromFile(File);
        return json;
        //JsonUtility.FromJsonOverwrite(json, data);
    }

    public void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }
    private string GetFilePath(string filename)
    {
        // Debug.Log(Application.persistentDataPath + "/" + filename);
        // return Application.persistentDataPath + "/" + filename;
        return (@"D:\SimpaGameBotData\GameData.txt");
    }

    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        if (System.IO.File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("File not Found");
            return "";
        }
    }

}
