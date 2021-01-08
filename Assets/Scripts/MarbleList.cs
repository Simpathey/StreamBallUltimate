using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleList : MonoBehaviour
{
    [SerializeField] int MinimumCommon = 0;
    [SerializeField] int MaximumCommon = 100;
    [SerializeField] int MaximumRare = 400;
    [SerializeField] int MaximumEpic = 1000;
    [SerializeField] int MaxLegendary = 2000;

    public List<bool> MarbleArraySetToFalse = new List<bool>();
    //Every string needs to be the unique code for the marble
    //The bool will be true or false depending on if the player has unlocked that marble

    public Dictionary<string, int> MarbleCommonNameToMarbleCode = new Dictionary<string, int>();
    //This is a way for the game to quickly look up what the unique marble code is for a common name

    public Dictionary<int, int> MarbleCodeToCost = new Dictionary<int, int>();
    //This lets us look up the cost of a marble if we have the keycode

    public Dictionary<int, int> MarbleCodeToRarity = new Dictionary<int, int>();
    //This links a marble code to its cost

    public Dictionary<int, string> MarbleCodeToCommonName = new Dictionary<int, string>();

    [SerializeField] GameObject[] Marbles;
    //This game objects stores the balls uniquecode,skin(sprite),cost and rarity 

    private void Start()
    {
        int code = 0;
        //the goal here is to make a dictionary of every ball's unique code and 
        //have it initialized to be false (not unlocked)
        foreach (var item in Marbles)
        {
            //We grab the ball script at the start so we can build our dictionaries at start
            var marble = item.GetComponent<Marble>();
            marble.MarbleCode = code;
            //We first need to set the code before we build the dictionaries 

            MarbleArraySetToFalse.Add(false);
            MarbleCommonNameToMarbleCode.Add((marble.CommonName.ToLower()), marble.MarbleCode);
            MarbleCodeToCommonName.Add(marble.MarbleCode, (marble.CommonName.ToLower()));
            int cost = setMarbleCostsBasedOnRarity(marble.Rarity);
            MarbleCodeToCost.Add(marble.MarbleCode, cost);
            marble.Cost = cost;
            code = code + 1;
        }
        //The player always has marble 0 unlocked
        MarbleArraySetToFalse[0] = true;
    }

    public List<bool> getEmptyAllMarbleDictionary()
    {
        return MarbleArraySetToFalse;
    }
    //At the start of the game there may be new marbles that need to be added to the game data
    //We need to count the difference between the current # and the existing number in any game data entry
    //Add the difference 
    public Dictionary<string, PlayerData> AddNewMarblesToGameData(Dictionary<string, PlayerData> gameData)
    {
        Debug.Log("ADD NEW MARBLES CALLED");
        int currentNumberOfMarbles = Marbles.Length;
        foreach (var kvp in gameData)
        {
            while (kvp.Value.Skins.Count < currentNumberOfMarbles)
            {
                Debug.Log("new skin added");
                kvp.Value.Skins.Add(false);
            }
        }
        return gameData;
    }

    public bool DoesMarbleCommonNameExist(string commonName)
    {
        if (MarbleCommonNameToMarbleCode.ContainsKey(commonName))
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
        int code = MarbleCommonNameToMarbleCode[commonName];
        int cost = MarbleCodeToCost[code];
        return cost;
    }

    public int GetMarbleCodeFromCommonName(string commonName)
    {
        int code = MarbleCommonNameToMarbleCode[commonName];
        return code;
    }

    public string GetCommonNameFromMarbleCode(int ballCode)
    {
        return MarbleCodeToCommonName[ballCode];
    }
    public GameObject GetMarbleFromMarbleCode(int ballCode)
    {
        return Marbles[ballCode];
    }

    public int setMarbleCostsBasedOnRarity(int rarity)
    {
        //At the start of the game session all the marble costs will be randomized 
        //They will fall into a price range depending on rarity
        switch (rarity)
        {
            case 1:
                return Random.Range(MinimumCommon, MaximumCommon);
            case 2:
                return Random.Range(MaximumCommon, MaximumRare);
            case 3:
                return Random.Range(MaximumRare, MaximumEpic);
            case 4:
                return Random.Range(MaximumEpic, MaxLegendary);
            default:
                return -1;
        }

    }

    public HashSet<GameObject> GetMarblesForShop(int howManyMarbles)
    {
        int marbleLength = Marbles.Length;
        HashSet<GameObject> returnedMarbles = new HashSet<GameObject>();
        do
        {
            int marbleCode = Random.Range(0, marbleLength);
            returnedMarbles.Add(Marbles[marbleCode]);
        } while (returnedMarbles.Count < howManyMarbles);
        foreach (var item in returnedMarbles)
        {
            Debug.Log(item.GetComponent<Marble>().CommonName);
        }
        return returnedMarbles;

    }
}
