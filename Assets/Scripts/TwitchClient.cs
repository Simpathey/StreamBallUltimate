using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.PubSub.Events;
using TwitchLib.Unity;
using UnityEngine;
using UnityEngine.UI;

public class TwitchClient : MonoBehaviour
{
    // Client Object is defined in Twitch Lib
    public Client Client;
    public JoinedChannel JoinedChannel;
    public JoinedChannel BotChannel;
    CommandQueue CommandQueue;
    private PubSub PubSub;
    [SerializeField] Text Debug;
    private string ChannelName = "simpathey";
    private string BotName = "simpagamebot";

    void Start()
    {
        CommandQueue = FindObjectOfType<CommandQueue>();
        //This script should always run in background if game application is running
        Application.runInBackground = true;

        //set up bot and tell what channel to join
        ConnectionCredentials credentials = new ConnectionCredentials(BotName, Secrets.BotAccessToken);
        Client = new Client();
        Client.Initialize(credentials, ChannelName);
        //pubSub = new PubSub();

        //connect bot to channel
        Client.Connect();
        Client.OnJoinedChannel += ClientOnJoinedChannel;
        Client.OnMessageReceived += MyMessageReceivedFunction;
        Client.OnChatCommandReceived += MyCommandReceivedFunction;
        Client.OnWhisperSent += Client_OnWhisperSent;
        Client.OnWhisperReceived += Client_OnWhisperReceived;

        //pubSub.OnChannelCommerceReceived += Pubsub_OnCommerceReceived;

        //client.On will fill in with telesence
    }


    private void Client_OnWhisperSent(object sender, OnWhisperSentArgs e)
    {
        UnityEngine.Debug.Log(sender.ToString());
        UnityEngine.Debug.Log(e.Receiver);
    }

    private void ClientOnJoinedChannel(object sender, OnJoinedChannelArgs e)
    {
        JoinedChannel = new JoinedChannel(ChannelName);
        BotChannel = new JoinedChannel(BotName);
        //client.SendMessage(joinedChannel, "SimpaGameBotConnected");
    }

    private void MyCommandReceivedFunction(object sender, OnChatCommandReceivedArgs e)
    {
        Arrrgs chatArgs = new Arrrgs();
        chatArgs.Message = e.Command.ChatMessage.Message;
        chatArgs.UserID = e.Command.ChatMessage.UserId;
        chatArgs.DisplayName = e.Command.ChatMessage.DisplayName;
        chatArgs.CommandText = e.Command.CommandText.ToLower();
        chatArgs.MultiCommand = e.Command.ArgumentsAsList;

        for (int index = 0; index < e.Command.ArgumentsAsList.Count; index++)
        {
            chatArgs.CommandArgs += e.Command.ArgumentsAsList[index].ToLower();
        }

        CommandQueue.FirstCommandBuckets(chatArgs); //e
        Debug.text = (e.Command.ChatMessage.Username);
    }

    private void MyMessageReceivedFunction(object sender,
        TwitchLib.Client.Events.OnMessageReceivedArgs e)
    {
        UnityEngine.Debug.Log(e.ChatMessage.UserId);
    }
    private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
    {
        Arrrgs chatArgs = new Arrrgs();
        chatArgs.Message = e.WhisperMessage.Message;
        chatArgs.UserID = e.WhisperMessage.UserId;
        chatArgs.DisplayName = e.WhisperMessage.DisplayName;
        chatArgs.CommandText = ConvertWhisperToCommand(e.WhisperMessage.Message);
        chatArgs.CommandArgs = ConvertWhisperToArguments(e.WhisperMessage.Message);
        CommandQueue.FirstCommandBuckets(chatArgs);
    }
    private string ConvertWhisperToCommand(string whisper)
    {
        if (string.IsNullOrEmpty(whisper))
        {
            return whisper;
        }
        else
        {

            whisper = whisper.Substring(1);
            string[] commandArray = whisper.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return (commandArray[0].ToLower());

        }
    }

    private string ConvertWhisperToArguments(string whisper)
    {
        string commands = "";
        if (string.IsNullOrEmpty(whisper))
        {
            return whisper;
        }
        else
        {

            whisper = whisper.Substring(1);
            string[] commandArray = whisper.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            UnityEngine.Debug.Log(commandArray[0]);
            if (commandArray.Length > 1)
            {
                for (int i = 1; i < commandArray.Length; i++)
                {
                    commands += commandArray[i];
                }
            }
            commands = commands.ToLower();
            return (commands);

        }
    }
    private List<string> ParseCommand(string command)
    {
        List<string> list = new List<string>();
        string[] commandArray = command.Split(' ');
        if (string.IsNullOrEmpty(command))
        {
            return null;
        }
        for (int i = 0; i < commandArray.Length; i++)
        {
            UnityEngine.Debug.Log(commandArray.Length + " : this is command Array Length");
            UnityEngine.Debug.Log(commandArray[i] + " : this is command Array");
        }
        return null;
    }
}
