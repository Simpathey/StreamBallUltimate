using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
//DATA that is stored in a dictionary and can be accessed by looking at the key which will be a player chat ID 
{
    public int Money = 0;
    public List<bool> Skins = new List<bool>();
    public int ActiveSkin = 0;
    public string PlayerName;
    public bool IsSubscribed;
    //Things I may want to add later bellow
    //public Dictionary<int, bool> sfx = new Dictionary<int, bool>();
    //public Dictionary<int, bool> vfx = new Dictionary<int, bool>();

}
