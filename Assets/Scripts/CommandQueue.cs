using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue : MonoBehaviour
{
    //Queue's store and handle messege sending so the BOT does not break Twitch Dev Guidlines
    //100 chat messeges per min
    //200 whispers per min
    Queue<Arrrgs> CommandQueueChat = new Queue<Arrrgs>();
    Queue<Arrrgs> CommandQueueWhisper = new Queue<Arrrgs>();
    [SerializeField] Commands Commands;
    private void Start()
    {
        //commands = FindObjectOfType<Commands>();
        StartCoroutine(RemoveFromChatQueue());
    }
    public void AddToChatQueue(Arrrgs arg)
    {
        CommandQueueChat.Enqueue(arg);
        Debug.Log("Command in queue");
        Debug.Log("There are " + CommandQueueChat.Count + " in the queue");
    }

    private void AddToWhisperQueue(Arrrgs arg)
    {
        CommandQueueWhisper.Enqueue(arg);
    }

    //This Dequeue's from the commandChatQueue
    IEnumerator RemoveFromChatQueue()
    {
        while (true)
        {
            if (CommandQueueChat.Count != 0)
            {
                var e = CommandQueueChat.Dequeue();
                string firstCommand = e.CommandText; //Command.CommandText.ToLower();
                if (firstCommand == "buy") { Commands.Buy(e); }
                else if (firstCommand == "join") { Commands.Join(e); }
                else if (firstCommand == "equip") { Commands.Equip(e); }
                else if (firstCommand == "money") { Commands.money(e); }
                else if (firstCommand == "inuse") { Commands.InUse(e); }
                else if (firstCommand == "help") { Commands.Help(e); }
                else if (firstCommand == "skins") { Commands.Skins(e); }
                else if (firstCommand == "give") { Commands.Give(e); }

                //else if (firstCommand == "stopwhispers") { commands.StopWhispers(e); }
                else { Debug.LogWarning("COMMAND NOT FOUND"); }
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
            }

        }
    }


    //This Seperates the different commands into buckets
    public void FirstCommandBuckets(Arrrgs e)
    {
        string firstCommand = e.CommandText; //Command.CommandText.ToLower();
        //These commands will provide player confirmation/Response in CHAT
        if (firstCommand == "buy") { AddToChatQueue(e); }
        else if (firstCommand == "join") { AddToChatQueue(e); }
        else if (firstCommand == "equip") { AddToChatQueue(e); }
        else if (firstCommand == "money") { AddToChatQueue(e); }
        else if (firstCommand == "inuse") { AddToChatQueue(e); }
        else if (firstCommand == "skins") { AddToChatQueue(e); }
        else if (firstCommand == "give") { AddToChatQueue(e); }

        //These commands will provide player with visial confirmation in overlay
        else if (firstCommand == "play") { Commands.Play(e); }
        else if (firstCommand == "rotate") { Commands.Rotate(e); } //TEMPORARY CHAT COMMAND TO ROTATE SHOP
        //These commands will provide player with whisper confirmation 
        else if (firstCommand == "help") { AddToChatQueue(e); }
        else if (firstCommand == "vfx") { }
        else if (firstCommand == "sfx") { }

        else { return; }
    }

}
