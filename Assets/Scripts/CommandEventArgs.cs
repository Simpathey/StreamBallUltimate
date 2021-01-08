using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEventArgs
{
    //These are the chat arguments that we care about 
    //Stores data for both whispers and chat messeges 

    public string Message = "";
    public string UserID = "";
    public string DisplayName = "";
    public string CommandText = "";
    public string CommandArgs = "";
    public List<string> MultiCommand = null; //new List<string>();

}
