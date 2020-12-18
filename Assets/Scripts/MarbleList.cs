using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleList : MonoBehaviour
{
    [SerializeField] int minCommon = 0;
    [SerializeField] int maxCommon = 100;
    [SerializeField] int maxRare = 400;
    [SerializeField] int maxEpic = 1000;
    [SerializeField] int maxLegendary = 2000; 

    public List<bool> marbleArraySetToFalse = new List<bool>();
    //Every string needs to be the unique code for the marble
    //The bool will be true or false depending on if the player has unlocked that marble

    public Dictionary<string, int> marbleCommonNameToMarbleCode = new Dictionary<string, int>();
    //This is a way for the game to quickly look up what the unique marble code is for a common name

    public Dictionary<int, int> marbleCodeToCost = new Dictionary<int, int>();
    //This lets us look up the cost of a marble if we have the keycode

    public Dictionary<int, int> marbleCodeToRarity = new Dictionary<int, int>();
    //This links a marble code to its cost

    public Dictionary<int, string> marbleCodeToCommonName = new Dictionary<int, string>();

    [SerializeField] GameObject[] marbles;
    //This game objects stores the balls uniquecode,skin(sprite),cost and rarity 

    private void Start()
    {
        int code = 0;
        //the goal here is to make a dictionary of every ball's unique code and 
        //have it initialized to be false (not unlocked)
        foreach (var item in marbles)
        {
            //We grab the ball script at the start so we can build our dictionaries at start
            var marble = item.GetComponent<Marble>();
            marble.marbleCode = code;
            //We first need to set the code before we build the dictionaries 

            marbleArraySetToFalse.Add(false);
            marbleCommonNameToMarbleCode.Add((marble.commonName.ToLower()), marble.marbleCode);
            marbleCodeToCommonName.Add(marble.marbleCode, (marble.commonName.ToLower()));
            int cost = setMarbleCostsBasedOnRarity(marble.rarity);
            marbleCodeToCost.Add(marble.marbleCode, cost);
            marble.cost = cost;
            code = code + 1;
        }
        //The player always has marble 0 unlocked
        marbleArraySetToFalse[0] = true;
    }

    public List<bool> getEmptyAllMarbleDictionary() 
    {
        return marbleArraySetToFalse;
    }
    //At the start of the game there may be new marbles that need to be added to the game data
    //We need to count the difference between the current # and the existing number in any game data entry
    //Add the difference 
    public Dictionary<string, PlayerData> AddNewMarblesToGameData(Dictionary<string, PlayerData> gameData)
    {
        Debug.Log("ADD NEW MARBLES CALLED");
        int currentNumberOfMarbles = marbles.Length;
        foreach (var kvp in gameData)
        {
            while (kvp.Value.skins.Count < currentNumberOfMarbles)
            {
                Debug.Log("new skin added");
                kvp.Value.skins.Add(false);
            }
        }
        return gameData;
    }

    public bool DoesMarbleCommonNameExist(string commonName)
    {
        if (marbleCommonNameToMarbleCode.ContainsKey(commonName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetMarbleCostFromCommonName(string commonName)
    {
            int code = marbleCommonNameToMarbleCode[commonName];
            int cost = marbleCodeToCost[code];
            return cost;
    }
    
    public int GetMarbleCodeFromCommonName(string commonName)
    {
        int code = marbleCommonNameToMarbleCode[commonName];
        return code;
    }

    public string GetCommonNameFromMarbleCode(int ballCode)
    {
        return marbleCodeToCommonName[ballCode];
    }
    public GameObject GetMarbleFromMarbleCode(int ballCode)
    {
        return marbles[ballCode];
    }
    
    public int setMarbleCostsBasedOnRarity(int rarity)
    {
        //At the start of the game session all the marble costs will be randomized 
        //They will fall into a price range depending on rarity
        switch (rarity)
        {
            case 1:
                return Random.Range(minCommon, maxCommon);
            case 2:
                return Random.Range(maxCommon, maxRare);
            case 3:
                return Random.Range(maxRare, maxEpic);
            case 4:
                return Random.Range(maxEpic, maxLegendary);
            default:
                return -1;
        }

    }

    public HashSet<GameObject> GetMarblesForShop(int howManyMarbles)
    {
        int marbleLength = marbles.Length;
        HashSet<GameObject> returnedMarbles = new HashSet<GameObject>();
        do { 
            int marbleCode = Random.Range(0, marbleLength);
            returnedMarbles.Add(marbles[marbleCode]);
        } while (returnedMarbles.Count<howManyMarbles);
        foreach (var item in returnedMarbles)
        {
            Debug.Log(item.GetComponent<Marble>().commonName);
        }
        return returnedMarbles;

    }
}
