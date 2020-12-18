using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataManager: MonoBehaviour
{
    public PlayerData data;
    public string file = "GameData.txt";

    public void Save()
    {
        Debug.Log(data);
        string json = JsonConvert.SerializeObject(data);
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
        Debug.Log(data);
        string json = JsonConvert.SerializeObject(gameData, Formatting.Indented);
        System.IO.File.WriteAllText(@"D:\SimpaGameBotData\GameData.txt", json);
    }

    public string Load()
    {
        data = new PlayerData();
        string json = ReadFromFile(file);
        return json;
        //JsonUtility.FromJsonOverwrite(json, data);
    }

    public void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using(StreamWriter writer = new StreamWriter(fileStream))
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
        if (File.Exists(path))
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
