using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrrgs
{
    //These are the chat arguments that we care about 
    //Stores data for both whispers and chat messeges 

    public string message = "";
    public string userID = "";
    public string displayName = "";
    public string commandText = "";
    public string commandArgs = "";
    public List<string> multiCommand = null; //new List<string>();

}
